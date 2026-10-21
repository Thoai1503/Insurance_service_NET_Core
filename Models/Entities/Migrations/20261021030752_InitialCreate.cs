using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance_agency.Models.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_activity_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "tbl_assign_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contract_id = table.Column<int>(type: "int", nullable: false),
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_assign_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_authentication",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    active = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_authentication", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_insurance_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    image_class_css = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_policy",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    insurance_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    age_min = table.Column<int>(type: "int", nullable: true),
                    age_max = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_subsidiary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "ntext", nullable: true),
                    insurance_id = table.Column<int>(type: "int", nullable: true),
                    promotion_percentage = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_subsidiary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_authorization",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    url = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    authen_id = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_tbl_authorization_tbl_authentication",
                        column: x => x.authen_id,
                        principalTable: "tbl_authentication",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    auth_id = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<int>(type: "int", nullable: true),
                    phone = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    avatar = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "user.jpg"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_user_tbl_authentication1",
                        column: x => x.auth_id,
                        principalTable: "tbl_authentication",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "insurance",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "ntext", nullable: true),
                    year_max = table.Column<int>(type: "int", nullable: true),
                    insurance_type_id = table.Column<int>(type: "int", nullable: true),
                    value = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    ex_image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance", x => x.id);
                    table.ForeignKey(
                        name: "FK_insurance_tbl_insurance_type",
                        column: x => x.insurance_type_id,
                        principalTable: "tbl_insurance_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_notification",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    from = table.Column<int>(type: "int", nullable: false),
                    notification_type_id = table.Column<int>(type: "int", nullable: true),
                    to = table.Column<int>(type: "int", nullable: false),
                    is_read = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_notification_tbl_user1",
                        column: x => x.to,
                        principalTable: "tbl_user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_contract",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    insurance_id = table.Column<int>(type: "int", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    value_contract = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    total_paid = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    year_paid = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    number_year_paid = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_contract_insurance",
                        column: x => x.insurance_id,
                        principalTable: "insurance",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_contract_tbl_user",
                        column: x => x.user_id,
                        principalTable: "tbl_user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_loan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        principalTable: "tbl_contract",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_payment_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        principalTable: "tbl_contract",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_insurance_insurance_type_id",
                table: "insurance",
                column: "insurance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_authorization_authen_id",
                table: "tbl_authorization",
                column: "authen_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_contract_insurance_id",
                table: "tbl_contract",
                column: "insurance_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_contract_user_id",
                table: "tbl_contract",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_loan_contract_id",
                table: "tbl_loan",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_notification_to",
                table: "tbl_notification",
                column: "to");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_payment_history_contract_id",
                table: "tbl_payment_history",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_user_auth_id",
                table: "tbl_user",
                column: "auth_id");

            migrationBuilder.CreateIndex(
                name: "UX_tbl_user_email",
                table: "tbl_user",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UX_tbl_user_phone",
                table: "tbl_user",
                column: "phone",
                unique: true,
                filter: "[phone] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_activity_log");

            migrationBuilder.DropTable(
                name: "tbl_assign_history");

            migrationBuilder.DropTable(
                name: "tbl_authorization");

            migrationBuilder.DropTable(
                name: "tbl_loan");

            migrationBuilder.DropTable(
                name: "tbl_notification");

            migrationBuilder.DropTable(
                name: "tbl_payment_history");

            migrationBuilder.DropTable(
                name: "tbl_policy");

            migrationBuilder.DropTable(
                name: "tbl_subsidiary");

            migrationBuilder.DropTable(
                name: "tbl_contract");

            migrationBuilder.DropTable(
                name: "insurance");

            migrationBuilder.DropTable(
                name: "tbl_user");

            migrationBuilder.DropTable(
                name: "tbl_insurance_type");

            migrationBuilder.DropTable(
                name: "tbl_authentication");
        }
    }
}
