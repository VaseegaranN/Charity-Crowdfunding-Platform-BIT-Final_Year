using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnityFunds.Web.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class RemoveStoredCardDetails : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cardNumber",
                table: "donor_details",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cvCode",
                table: "donor_details",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "expiry",
                table: "donor_details",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
