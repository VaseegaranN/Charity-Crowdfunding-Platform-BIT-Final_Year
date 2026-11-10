using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnityFunds.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_details",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    admin_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_profile_picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_details", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "admin_posts",
                columns: table => new
                {
                    ap_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ap_admin_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap_admin_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap_admin_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap_place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap_desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap_media = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_posts", x => x.ap_id);
                });

            migrationBuilder.CreateTable(
                name: "donor_details",
                columns: table => new
                {
                    donor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    donor_user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_pwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donor_user_gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fundingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    donationAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    upiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expiry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cvCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    childhood_pet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vacation_destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    favourite_hobby = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donor_details", x => x.donor_id);
                });

            migrationBuilder.CreateTable(
                name: "gobi_users",
                columns: table => new
                {
                    gobi_user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gobi_user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_dob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_main_area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_acc_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gobi_user_mpin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gobi_users", x => x.gobi_user_id);
                });

            migrationBuilder.CreateTable(
                name: "govn_posts",
                columns: table => new
                {
                    gp_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    gp_staff_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gp_staff_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gp_staff_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gp_loc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gp_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gp_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gp_desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gp_media = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gp_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_govn_posts", x => x.gp_id);
                });

            migrationBuilder.CreateTable(
                name: "govn_staff_details",
                columns: table => new
                {
                    govn_staff_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    govn_staff_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    govn_staff_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    govn_staff_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    govn_staff_location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    govn_staff_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    govn_staff_work_dept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    govn_staff_mpin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_govn_staff_details", x => x.govn_staff_id);
                });

            migrationBuilder.CreateTable(
                name: "logistics_details",
                columns: table => new
                {
                    logistics_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    logistics_user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_user_position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_user_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_user_pwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_org_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_org_place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_org_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_org_mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logistics_id_image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    logistics_account_req_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    childhood_pet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vacation_destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    favourite_hobby = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logistics_details", x => x.logistics_id);
                });

            migrationBuilder.CreateTable(
                name: "ngo_details",
                columns: table => new
                {
                    ngo_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ngo_user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_user_position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_user_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_user_pwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_org_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_org_place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_org_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_org_mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_id_image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ngo_acc_req_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    childhood_pet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vacation_destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    favourite_hobby = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ngo_details", x => x.ngo_id);
                });

            migrationBuilder.CreateTable(
                name: "ngo_post_details",
                columns: table => new
                {
                    ngo_post_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ngo_user_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_user_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_post_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_post_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_post_desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_post_media = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngo_post_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ngo_post_details", x => x.ngo_post_id);
                });

            migrationBuilder.CreateTable(
                name: "public_contact_help",
                columns: table => new
                {
                    pch_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pch_user_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pch_user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pch_user_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pch_user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pch_user_location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pch_desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pch_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pch_admin_reply = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_public_contact_help", x => x.pch_id);
                });

            migrationBuilder.CreateTable(
                name: "public_posts",
                columns: table => new
                {
                    pp_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pp_user_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pp_username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pp_userphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pp_userloc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pp_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pp_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pp_userpost_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pp_media = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pp_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_public_posts", x => x.pp_id);
                });

            migrationBuilder.CreateTable(
                name: "sathy_users",
                columns: table => new
                {
                    sathy_user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sathy_user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_dob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_main_area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_acc_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sathy_user_mpin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sathy_users", x => x.sathy_user_id);
                });

            migrationBuilder.CreateTable(
                name: "user_reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reporter_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reported_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_reports", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_details");

            migrationBuilder.DropTable(
                name: "admin_posts");

            migrationBuilder.DropTable(
                name: "donor_details");

            migrationBuilder.DropTable(
                name: "gobi_users");

            migrationBuilder.DropTable(
                name: "govn_posts");

            migrationBuilder.DropTable(
                name: "govn_staff_details");

            migrationBuilder.DropTable(
                name: "logistics_details");

            migrationBuilder.DropTable(
                name: "ngo_details");

            migrationBuilder.DropTable(
                name: "ngo_post_details");

            migrationBuilder.DropTable(
                name: "public_contact_help");

            migrationBuilder.DropTable(
                name: "public_posts");

            migrationBuilder.DropTable(
                name: "sathy_users");

            migrationBuilder.DropTable(
                name: "user_reports");
        }
    }
}
