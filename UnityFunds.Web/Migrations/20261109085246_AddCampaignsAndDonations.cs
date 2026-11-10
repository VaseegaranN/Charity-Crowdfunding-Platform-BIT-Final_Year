
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnityFunds.Web.Migrations
{
    public partial class AddCampaignsAndDonations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create only the new crowdfunding tables.
            // Existing UnityFunds tables must remain untouched.

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    Title = table.Column<string>(
                        type: "nvarchar(160)",
                        maxLength: 160,
                        nullable: false),

                    Description = table.Column<string>(
                        type: "nvarchar(4000)",
                        maxLength: 4000,
                        nullable: false),

                    Category = table.Column<string>(
                        type: "nvarchar(80)",
                        maxLength: 80,
                        nullable: false),

                    NgoId = table.Column<string>(
                        type: "nvarchar(64)",
                        maxLength: 64,
                        nullable: false),

                    NgoName = table.Column<string>(
                        type: "nvarchar(160)",
                        maxLength: 160,
                        nullable: false),

                    GoalAmount = table.Column<decimal>(
                        type: "decimal(18,2)",
                        nullable: false),

                    RaisedAmount = table.Column<decimal>(
                        type: "decimal(18,2)",
                        nullable: false),

                    ImageUrl = table.Column<string>(
                        type: "nvarchar(500)",
                        maxLength: 500,
                        nullable: true),

                    Status = table.Column<string>(
                        type: "nvarchar(20)",
                        maxLength: 20,
                        nullable: false),

                    CreatedAtUtc = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    EndDateUtc = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    CampaignId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    DonorId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    Amount = table.Column<decimal>(
                        type: "decimal(18,2)",
                        nullable: false),

                    Status = table.Column<string>(
                        type: "nvarchar(24)",
                        maxLength: 24,
                        nullable: false),

                    PaymentReference = table.Column<string>(
                        type: "nvarchar(80)",
                        maxLength: 80,
                        nullable: false),

                    PaymentProvider = table.Column<string>(
                        type: "nvarchar(80)",
                        maxLength: 80,
                        nullable: false),

                    CreatedAtUtc = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    PaidAtUtc = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);

                    table.ForeignKey(
                        name: "FK_Donations_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Performance indexes

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_NgoId",
                table: "Campaigns",
                column: "NgoId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_Status_CreatedAtUtc",
                table: "Campaigns",
                columns: new[] { "Status", "CreatedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CampaignId_Status",
                table: "Donations",
                columns: new[] { "CampaignId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId_CreatedAtUtc",
                table: "Donations",
                columns: new[] { "DonorId", "CreatedAtUtc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Roll back only the newly introduced crowdfunding tables.

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Campaigns");
        }
    }
}
