using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnityFunds.Web.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class AddCampaignDonationRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Donations_CampaignId_Status",
                table: "Donations",
                columns: new[] { "CampaignId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId_CreatedAtUtc",
                table: "Donations",
                columns: new[] { "DonorId", "CreatedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_NgoId",
                table: "Campaigns",
                column: "NgoId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_Status_CreatedAtUtc",
                table: "Campaigns",
                columns: new[] { "Status", "CreatedAtUtc" });

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Campaigns_CampaignId",
                table: "Donations",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Campaigns_CampaignId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_CampaignId_Status",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_DonorId_CreatedAtUtc",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_NgoId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_Status_CreatedAtUtc",
                table: "Campaigns");
        }
    }
}
