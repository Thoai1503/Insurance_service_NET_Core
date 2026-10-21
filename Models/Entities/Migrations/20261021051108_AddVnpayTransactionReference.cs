using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance_agency.Models.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddVnpayTransactionReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "vnpay_transaction_ref",
                table: "tbl_payment_history",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_payment_history_vnpay_transaction_ref",
                table: "tbl_payment_history",
                column: "vnpay_transaction_ref",
                unique: true,
                filter: "[vnpay_transaction_ref] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_payment_history_vnpay_transaction_ref",
                table: "tbl_payment_history");

            migrationBuilder.DropColumn(
                name: "vnpay_transaction_ref",
                table: "tbl_payment_history");
        }
    }
}
