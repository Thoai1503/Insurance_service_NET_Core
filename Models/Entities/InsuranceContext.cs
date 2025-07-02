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

    public virtual DbSet<TblActivityLog> TblActivityLogs { get; set; }

    public virtual DbSet<TblContract> TblContracts { get; set; }

    public virtual DbSet<TblContractStatus> TblContractStatuses { get; set; }

    public virtual DbSet<TblInsuranceType> TblInsuranceTypes { get; set; }

    public virtual DbSet<TblLoan> TblLoans { get; set; }

    public virtual DbSet<TblNotification> TblNotifications { get; set; }

    public virtual DbSet<TblNotificationType> TblNotificationTypes { get; set; }

    public virtual DbSet<TblPaymentHistory> TblPaymentHistories { get; set; }

    public virtual DbSet<TblPolicy> TblPolicies { get; set; }

    public virtual DbSet<TblPolicyBenifit> TblPolicyBenifits { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=insurance;User ID=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ActivityLog");

            entity.ToTable("tbl_ActivityLog");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
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

        modelBuilder.Entity<TblContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Contract");

            entity.ToTable("tbl_Contract");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Policy).WithMany(p => p.TblContracts)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK_Contract_Policy");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.TblContracts)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK_Contract_ContractStatus");

            entity.HasOne(d => d.User).WithMany(p => p.TblContracts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Contract_User");
        });

        modelBuilder.Entity<TblContractStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ContractStatus");

            entity.ToTable("tbl_ContractStatus");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TblInsuranceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InsuranceType");

            entity.ToTable("tbl_InsuranceType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(1)
                .HasColumnName("active");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
        });

        modelBuilder.Entity<TblLoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Loan");

            entity.ToTable("tbl_Loan");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
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

            entity.ToTable("tbl_Notification");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Detail)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("detail");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.TblNotifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .HasConstraintName("FK_Notification_NotificationType");

            entity.HasOne(d => d.User).WithMany(p => p.TblNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_User");
        });

        modelBuilder.Entity<TblNotificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NotificationType");

            entity.ToTable("tbl_NotificationType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblPaymentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PaymentHistory");

            entity.ToTable("tbl_PaymentHistory");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
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

            entity.ToTable("tbl_Policy");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(1)
                .HasColumnName("active");
            entity.Property(e => e.InsuranceTypeId).HasColumnName("insurance_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SumAssured).HasColumnName("sum_assured");
            entity.Property(e => e.TermMax).HasColumnName("term_max");
            entity.Property(e => e.TermMin).HasColumnName("term_min");

            entity.HasOne(d => d.InsuranceType).WithMany(p => p.TblPolicies)
                .HasForeignKey(d => d.InsuranceTypeId)
                .HasConstraintName("FK_Policy_InsuranceType");
        });

        modelBuilder.Entity<TblPolicyBenifit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PolicyBenifit");

            entity.ToTable("tbl_PolicyBenifit");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(1)
                .HasColumnName("active");
            entity.Property(e => e.BenifitAmount).HasColumnName("benifit_amount");
            entity.Property(e => e.Detail)
                .HasMaxLength(500)
                .HasColumnName("detail");
            entity.Property(e => e.PolicyId).HasColumnName("policy_id");

            entity.HasOne(d => d.Policy).WithMany(p => p.TblPolicyBenifits)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK_PolicyBenifit_Policy");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.ToTable("tbl_User");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AuthId).HasColumnName("auth_id");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
