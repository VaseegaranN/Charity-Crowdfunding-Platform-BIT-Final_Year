using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnityFunds.Web.Migrations.SqlServerContext
{
    /// <inheritdoc />
    public partial class AddDonationIntegritySqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cardNumber",
                table: "donor_details");

            migrationBuilder.DropColumn(
                name: "cvCode",
                table: "donor_details");

            migrationBuilder.DropColumn(
                name: "expiry",
                table: "donor_details");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_PaymentReference",
                table: "Donations",
                column: "PaymentReference",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_donor_details_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "donor_details",
                principalColumn: "donor_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_donor_details_DonorId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_PaymentReference",
                table: "Donations");

            migrationBuilder.AddColumn<string>(
                name: "cardNumber",
                table: "donor_details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cvCode",
                table: "donor_details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "expiry",
                table: "donor_details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
