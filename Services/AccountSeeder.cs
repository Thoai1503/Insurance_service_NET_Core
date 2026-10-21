using Insurance_agency.Helper;
using Insurance_agency.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insurance_agency.Services;

internal static class AccountSeeder
{
    private const int AdminRoleId = 1;
    private const int UserRoleId = 4;
    private const string AdminEmail = "admin@gmail.com";
    private const string UserEmail = "user1@gmail.com";

    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
    {
        await using var scope = services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<InsuranceContext>();

        await EnsureRoleAsync(context, AdminRoleId, "Admin");
        await EnsureRoleAsync(context, UserRoleId, "User");

        await EnsureUserAsync(
            context,
            AdminEmail,
            "Admin Vo",
            AdminRoleId,
            configuration["SeedAccounts:AdminPassword"]);
        await EnsureUserAsync(
            context,
            UserEmail,
            "User 1",
            UserRoleId,
            configuration["SeedAccounts:UserPassword"]);

        await context.SaveChangesAsync();

        await EnsureAuthorizationAsync(context, AdminRoleId, "Admin", "/adminarea");
        await EnsureAuthorizationAsync(context, UserRoleId, "User", "/home");
    }

    private static async Task EnsureRoleAsync(InsuranceContext context, int id, string title)
    {
        if (await context.TblAuthentications.FindAsync(id) is null)
        {
            context.TblAuthentications.Add(new TblAuthentication
            {
                Id = id,
                Title = title,
                Active = 1
            });
        }
    }

    private static async Task EnsureUserAsync(
        InsuranceContext context,
        string email,
        string fullName,
        int roleId,
        string? password)
    {
        var existingUser = await context.TblUsers.SingleOrDefaultAsync(user => user.Email == email);
        if (existingUser is not null)
        {
            if (existingUser.AuthId != roleId)
            {
                throw new InvalidOperationException(
                    $"Seed account '{email}' already exists with a different role.");
            }

            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                $"Missing required configuration 'SeedAccounts:{(roleId == AdminRoleId ? "Admin" : "User")}Password'.");
        }

        context.TblUsers.Add(new TblUser
        {
            FullName = fullName,
            Email = email,
            Password = Function.MD5Hash(password),
            AuthId = roleId,
            Active = 1,
            Avatar = "user.jpg",
            CreatedDate = DateTime.Now
        });
    }

    private static async Task EnsureAuthorizationAsync(
        InsuranceContext context,
        int roleId,
        string title,
        string url)
    {
        await context.Database.ExecuteSqlInterpolatedAsync($"""
            IF NOT EXISTS (
                SELECT 1
                FROM tbl_authorization
                WHERE authen_id = {roleId} AND url = {url}
            )
            BEGIN
                INSERT INTO tbl_authorization (id, title, url, authen_id, active)
                VALUES ({roleId}, {title}, {url}, {roleId}, 1)
            END
            """);
    }
}
