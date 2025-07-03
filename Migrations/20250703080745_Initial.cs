using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance_agency.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ActivityLog",
                columns: table => new
                {
                    id = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    admin_id = table.Column<int>(type: "int", nullable: true),
                    action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    target_table = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    target_id = table.Column<int>(type: "int", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    detail = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ContractStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_InsuranceType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NotificationType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    full_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    auth_id = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Policy",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    insurance_type_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    term_min = table.Column<int>(type: "int", nullable: true),
                    term_max = table.Column<int>(type: "int", nullable: true),
                    sum_assured = table.Column<long>(type: "bigint", nullable: true),
                    active = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.id);
                    table.ForeignKey(
                        name: "FK_Policy_InsuranceType",
                        column: x => x.insurance_type_id,
                        principalTable: "tbl_InsuranceType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Notification",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    detail = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    notification_type_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    is_read = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notification_NotificationType",
                        column: x => x.notification_type_id,
                        principalTable: "tbl_NotificationType",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Notification_User",
                        column: x => x.user_id,
                        principalTable: "tbl_User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Contract",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    policy_id = table.Column<int>(type: "int", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    TotalPaid = table.Column<long>(type: "bigint", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.id);
                    table.ForeignKey(
                        name: "FK_Contract_ContractStatus",
                        column: x => x.status,
                        principalTable: "tbl_ContractStatus",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Contract_Policy",
                        column: x => x.policy_id,
                        principalTable: "tbl_Policy",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Contract_User",
                        column: x => x.user_id,
                        principalTable: "tbl_User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_PolicyBenifit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    policy_id = table.Column<int>(type: "int", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    benifit_amount = table.Column<long>(type: "bigint", nullable: true),
                    active = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyBenifit", x => x.id);
                    table.ForeignKey(
                        name: "FK_PolicyBenifit_Policy",
                        column: x => x.policy_id,
                        principalTable: "tbl_Policy",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Loan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    contract_id = table.Column<int>(type: "int", nullable: true),
                    loan_amount = table.Column<long>(type: "bigint", nullable: true),
                    request_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.id);
                    table.ForeignKey(
                        name: "FK_Loan_Contract",
                        column: x => x.contract_id,
                        principalTable: "tbl_Contract",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_PaymentHistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    contract_id = table.Column<int>(type: "int", nullable: true),
                    payment_day = table.Column<DateTime>(type: "datetime", nullable: true),
                    amount = table.Column<long>(type: "bigint", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_Contract",
                        column: x => x.contract_id,
                        principalTable: "tbl_Contract",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Contract_policy_id",
                table: "tbl_Contract",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Contract_status",
                table: "tbl_Contract",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Contract_user_id",
                table: "tbl_Contract",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Loan_contract_id",
                table: "tbl_Loan",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Notification_notification_type_id",
                table: "tbl_Notification",
                column: "notification_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Notification_user_id",
                table: "tbl_Notification",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PaymentHistory_contract_id",
                table: "tbl_PaymentHistory",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Policy_insurance_type_id",
                table: "tbl_Policy",
                column: "insurance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PolicyBenifit_policy_id",
                table: "tbl_PolicyBenifit",
                column: "policy_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ActivityLog");

            migrationBuilder.DropTable(
                name: "tbl_Loan");

            migrationBuilder.DropTable(
                name: "tbl_Notification");

            migrationBuilder.DropTable(
                name: "tbl_PaymentHistory");

            migrationBuilder.DropTable(
                name: "tbl_PolicyBenifit");

            migrationBuilder.DropTable(
                name: "tbl_NotificationType");

            migrationBuilder.DropTable(
                name: "tbl_Contract");

            migrationBuilder.DropTable(
                name: "tbl_ContractStatus");

            migrationBuilder.DropTable(
                name: "tbl_Policy");

            migrationBuilder.DropTable(
                name: "tbl_User");

            migrationBuilder.DropTable(
                name: "tbl_InsuranceType");
        }
    }
}
