using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Insurance_agency.Models.Entities;

public partial class InsuranceContext : DbContext
{
    public InsuranceContext()
    {
    }

    public InsuranceContext(DbContextOptions<InsuranceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<TblActivityLog> TblActivityLogs { get; set; }

    public virtual DbSet<TblAssignHistory> TblAssignHistories { get; set; }

    public virtual DbSet<TblAuthentication> TblAuthentications { get; set; }

    public virtual DbSet<TblAuthorization> TblAuthorizations { get; set; }

    public virtual DbSet<TblContract> TblContracts { get; set; }

    public virtual DbSet<TblInsuranceType> TblInsuranceTypes { get; set; }

    public virtual DbSet<TblLoan> TblLoans { get; set; }

    public virtual DbSet<TblNotification> TblNotifications { get; set; }

    public virtual DbSet<TblPaymentHistory> TblPaymentHistories { get; set; }

    public virtual DbSet<TblPolicy> TblPolicies { get; set; }

    public virtual DbSet<TblSubsidiary> TblSubsidiaries { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=Insurance3.mssql.somee.com;packet size=4096;user id=Thoai123_SQLLogin_1;pwd=5748x63qmi;data source=Insurance3.mssql.somee.com;persist security info=False;initial catalog=Insurance3;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.ToTable("insurance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("description");
            entity.Property(e => e.ExImage)
                .HasMaxLength(50)
                .HasColumnName("ex_image");
            entity.Property(e => e.InsuranceTypeId).HasColumnName("insurance_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.Value).HasColumnName("value");
            entity.Property(e => e.YearMax).HasColumnName("year_max");

            entity.HasOne(d => d.InsuranceType).WithMany(p => p.Insurances)
                .HasForeignKey(d => d.InsuranceTypeId)
                .HasConstraintName("FK_insurance_tbl_insurance_type");
        });

        modelBuilder.Entity<TblActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ActivityLog");

            entity.ToTable("tbl_activity_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Detail)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("detail");
            entity.Property(e => e.TargetId).HasColumnName("target_id");
            entity.Property(e => e.TargetTable)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("target_table");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<TblAssignHistory>(entity =>
        {
            entity.ToTable("tbl_assign_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<TblAuthentication>(entity =>
        {
            entity.ToTable("tbl_authentication");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TblAuthorization>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_authorization");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AuthenId).HasColumnName("authen_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("url");

            entity.HasOne(d => d.Authen).WithMany()
                .HasForeignKey(d => d.AuthenId)
                .HasConstraintName("FK_tbl_authorization_tbl_authentication");
        });

        modelBuilder.Entity<TblContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Contract");

            entity.ToTable("tbl_contract");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeId)
                .HasDefaultValue(0)
                .HasColumnName("employee_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");
            entity.Property(e => e.NumberYearPaid).HasColumnName("number_year_paid");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalPaid)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total_paid");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ValueContract)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("value_contract");
            entity.Property(e => e.YearPaid)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("year_paid");

            entity.HasOne(d => d.Insurance).WithMany(p => p.TblContracts)
                .HasForeignKey(d => d.InsuranceId)
                .HasConstraintName("FK_tbl_contract_insurance");

            entity.HasOne(d => d.User).WithMany(p => p.TblContracts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tbl_contract_tbl_user");
        });

        modelBuilder.Entity<TblInsuranceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InsuranceType");

            entity.ToTable("tbl_insurance_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(1)
                .HasColumnName("active");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ImageClassCss)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("image_class_css");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
        });

        modelBuilder.Entity<TblLoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Loan");

            entity.ToTable("tbl_loan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.LoanAmount).HasColumnName("loan_amount");
            entity.Property(e => e.RequestDate)
                .HasColumnType("datetime")
                .HasColumnName("request_date");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Contract).WithMany(p => p.TblLoans)
                .HasForeignKey(d => d.ContractId)
                .HasConstraintName("FK_Loan_Contract");
        });

        modelBuilder.Entity<TblNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Notification");

            entity.ToTable("tbl_notification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.Detail)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("detail");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Contract).WithMany(p => p.TblNotifications)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_notification_tbl_contract");
        });

        modelBuilder.Entity<TblPaymentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PaymentHistory");

            entity.ToTable("tbl_payment_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.PaymentDay)
                .HasColumnType("datetime")
                .HasColumnName("payment_day");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Contract).WithMany(p => p.TblPaymentHistories)
                .HasForeignKey(d => d.ContractId)
                .HasConstraintName("FK_PaymentHistory_Contract");
        });

        modelBuilder.Entity<TblPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Policy");

            entity.ToTable("tbl_policy");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(1)
                .HasColumnName("active");
            entity.Property(e => e.AgeMax).HasColumnName("age_max");
            entity.Property(e => e.AgeMin).HasColumnName("age_min");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Insurance).WithMany(p => p.TblPolicies)
                .HasForeignKey(d => d.InsuranceId)
                .HasConstraintName("FK_tbl_policy_insurance");
        });

        modelBuilder.Entity<TblSubsidiary>(entity =>
        {
            entity.ToTable("tbl_subsidiary");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("description");
            entity.Property(e => e.InsuranceId).HasColumnName("insurance_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PromotionPercentage)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("promotion_percentage");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.ToTable("tbl_user");

            entity.HasIndex(e => e.Email, "UX_tbl_user_email").IsUnique();

            entity.HasIndex(e => e.Phone, "UX_tbl_user_phone").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.AuthId).HasColumnName("auth_id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("user.jpg")
                .HasColumnName("avatar");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone");

            entity.HasOne(d => d.Auth).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.AuthId)
                .HasConstraintName("FK_tbl_user_tbl_authentication1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
