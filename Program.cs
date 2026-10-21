using Insurance_agency;
using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Insurance_agency.Services.VnPay;
using Microsoft.EntityFrameworkCore;

var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (File.Exists(envFilePath))
{
    DotNetEnv.Env.Load(envFilePath);
}

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Missing required configuration 'ConnectionStrings:DefaultConnection'.");
builder.Services.AddDbContext<InsuranceContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddScoped<IRepository<InsuranceTypeView>, InsuranceTypeRepository>();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();


builder.Services.AddHttpContextAccessor();

// Add payment gateway service
builder.Services.AddScoped<IVnPayService, VnPayService>();
var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); 
app.UseMiddleware<DisplayAndAuthorizationMiddleware>();


app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
