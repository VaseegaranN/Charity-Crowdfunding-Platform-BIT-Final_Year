using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnityFunds.Web.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class AddDonationIntegrity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
