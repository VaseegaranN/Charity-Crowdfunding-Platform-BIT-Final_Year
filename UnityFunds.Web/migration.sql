IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [admin_details] (
        [admin_id] int NOT NULL IDENTITY,
        [admin_name] nvarchar(max) NOT NULL,
        [admin_email] nvarchar(max) NOT NULL,
        [admin_phone_number] nvarchar(max) NOT NULL,
        [admin_pass] nvarchar(max) NOT NULL,
        [admin_profile_picture] varbinary(max) NULL,
        CONSTRAINT [PK_admin_details] PRIMARY KEY ([admin_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [admin_posts] (
        [ap_id] nvarchar(450) NOT NULL,
        [ap_admin_id] nvarchar(max) NOT NULL,
        [ap_admin_name] nvarchar(max) NOT NULL,
        [ap_admin_phone] nvarchar(max) NOT NULL,
        [ap_place] nvarchar(max) NOT NULL,
        [ap_date] nvarchar(max) NOT NULL,
        [ap_type] nvarchar(max) NOT NULL,
        [ap_desc] nvarchar(max) NOT NULL,
        [ap_media] nvarchar(max) NOT NULL,
        [ap_time] datetime2 NOT NULL,
        CONSTRAINT [PK_admin_posts] PRIMARY KEY ([ap_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [donor_details] (
        [donor_id] int NOT NULL IDENTITY,
        [donor_user_name] nvarchar(max) NOT NULL,
        [donor_user_email] nvarchar(max) NOT NULL,
        [donor_user_phone] nvarchar(max) NOT NULL,
        [donor_user_street] nvarchar(max) NOT NULL,
        [donor_user_city] nvarchar(max) NOT NULL,
        [donor_user_state] nvarchar(max) NOT NULL,
        [donor_user_zip] nvarchar(max) NOT NULL,
        [donor_user_country] nvarchar(max) NOT NULL,
        [donor_user_pwd] nvarchar(max) NOT NULL,
        [donor_user_gender] nvarchar(max) NOT NULL,
        [fundingType] nvarchar(max) NOT NULL,
        [donationAmount] nvarchar(max) NOT NULL,
        [paymentType] nvarchar(max) NOT NULL,
        [upiId] nvarchar(max) NOT NULL,
        [cardNumber] nvarchar(max) NOT NULL,
        [expiry] nvarchar(max) NOT NULL,
        [cvCode] nvarchar(max) NOT NULL,
        [childhood_pet] nvarchar(max) NOT NULL,
        [vacation_destination] nvarchar(max) NOT NULL,
        [favourite_hobby] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_donor_details] PRIMARY KEY ([donor_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [gobi_users] (
        [gobi_user_id] int NOT NULL IDENTITY,
        [gobi_user_name] nvarchar(max) NOT NULL,
        [gobi_user_dob] nvarchar(max) NOT NULL,
        [gobi_user_phone_number] nvarchar(max) NOT NULL,
        [gobi_user_place] nvarchar(max) NOT NULL,
        [gobi_user_email] nvarchar(max) NOT NULL,
        [gobi_user_password] nvarchar(max) NOT NULL,
        [gobi_user_address] nvarchar(max) NOT NULL,
        [gobi_user_main_area] nvarchar(max) NOT NULL,
        [gobi_user_acc_status] nvarchar(max) NOT NULL,
        [gobi_user_mpin] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_gobi_users] PRIMARY KEY ([gobi_user_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [govn_posts] (
        [gp_id] nvarchar(450) NOT NULL,
        [gp_staff_id] nvarchar(max) NOT NULL,
        [gp_staff_name] nvarchar(max) NOT NULL,
        [gp_staff_phone] nvarchar(max) NOT NULL,
        [gp_loc] nvarchar(max) NOT NULL,
        [gp_date] nvarchar(max) NOT NULL,
        [gp_type] nvarchar(max) NOT NULL,
        [gp_desc] nvarchar(max) NOT NULL,
        [gp_media] nvarchar(max) NOT NULL,
        [gp_time] datetime2 NOT NULL,
        CONSTRAINT [PK_govn_posts] PRIMARY KEY ([gp_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [govn_staff_details] (
        [govn_staff_id] nvarchar(450) NOT NULL,
        [govn_staff_name] nvarchar(max) NOT NULL,
        [govn_staff_phone] nvarchar(max) NOT NULL,
        [govn_staff_email] nvarchar(max) NOT NULL,
        [govn_staff_location] nvarchar(max) NOT NULL,
        [govn_staff_password] nvarchar(max) NOT NULL,
        [govn_staff_work_dept] nvarchar(max) NOT NULL,
        [govn_staff_mpin] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_govn_staff_details] PRIMARY KEY ([govn_staff_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [logistics_details] (
        [logistics_id] nvarchar(450) NOT NULL,
        [logistics_user_name] nvarchar(max) NOT NULL,
        [logistics_user_position] nvarchar(max) NOT NULL,
        [logistics_user_phone] nvarchar(max) NOT NULL,
        [logistics_user_email] nvarchar(max) NOT NULL,
        [logistics_user_pwd] nvarchar(max) NOT NULL,
        [logistics_org_name] nvarchar(max) NOT NULL,
        [logistics_org_place] nvarchar(max) NOT NULL,
        [logistics_org_phone] nvarchar(max) NOT NULL,
        [logistics_org_mail] nvarchar(max) NOT NULL,
        [logistics_id_image] varbinary(max) NULL,
        [logistics_account_req_status] nvarchar(max) NOT NULL,
        [childhood_pet] nvarchar(max) NOT NULL,
        [vacation_destination] nvarchar(max) NOT NULL,
        [favourite_hobby] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_logistics_details] PRIMARY KEY ([logistics_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [ngo_details] (
        [ngo_id] nvarchar(450) NOT NULL,
        [ngo_user_name] nvarchar(max) NOT NULL,
        [ngo_user_position] nvarchar(max) NOT NULL,
        [ngo_user_phone] nvarchar(max) NOT NULL,
        [ngo_user_email] nvarchar(max) NOT NULL,
        [ngo_user_pwd] nvarchar(max) NOT NULL,
        [ngo_org_name] nvarchar(max) NOT NULL,
        [ngo_org_place] nvarchar(max) NOT NULL,
        [ngo_org_phone] nvarchar(max) NOT NULL,
        [ngo_org_mail] nvarchar(max) NOT NULL,
        [ngo_id_image] varbinary(max) NULL,
        [ngo_acc_req_status] nvarchar(max) NOT NULL,
        [childhood_pet] nvarchar(max) NOT NULL,
        [vacation_destination] nvarchar(max) NOT NULL,
        [favourite_hobby] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ngo_details] PRIMARY KEY ([ngo_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [ngo_post_details] (
        [ngo_post_id] nvarchar(450) NOT NULL,
        [ngo_user_id] nvarchar(max) NOT NULL,
        [ngo_user_name] nvarchar(max) NOT NULL,
        [ngo_user_email] nvarchar(max) NOT NULL,
        [ngo_user_phone] nvarchar(max) NOT NULL,
        [ngo_post_date] nvarchar(max) NOT NULL,
        [ngo_post_type] nvarchar(max) NOT NULL,
        [ngo_post_desc] nvarchar(max) NOT NULL,
        [ngo_post_media] nvarchar(max) NOT NULL,
        [ngo_post_time] datetime2 NOT NULL,
        CONSTRAINT [PK_ngo_post_details] PRIMARY KEY ([ngo_post_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [public_contact_help] (
        [pch_id] int NOT NULL IDENTITY,
        [pch_user_id] nvarchar(max) NOT NULL,
        [pch_user_name] nvarchar(max) NOT NULL,
        [pch_user_phone] nvarchar(max) NOT NULL,
        [pch_user_email] nvarchar(max) NOT NULL,
        [pch_user_location] nvarchar(max) NOT NULL,
        [pch_desc] nvarchar(max) NOT NULL,
        [pch_date] nvarchar(max) NOT NULL,
        [pch_admin_reply] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_public_contact_help] PRIMARY KEY ([pch_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [public_posts] (
        [pp_id] nvarchar(450) NOT NULL,
        [pp_user_id] nvarchar(max) NOT NULL,
        [pp_username] nvarchar(max) NOT NULL,
        [pp_userphone] nvarchar(max) NOT NULL,
        [pp_userloc] nvarchar(max) NOT NULL,
        [pp_date] nvarchar(max) NOT NULL,
        [pp_type] nvarchar(max) NOT NULL,
        [pp_userpost_description] nvarchar(max) NOT NULL,
        [pp_media] nvarchar(max) NOT NULL,
        [pp_time] datetime2 NOT NULL,
        CONSTRAINT [PK_public_posts] PRIMARY KEY ([pp_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [sathy_users] (
        [sathy_user_id] int NOT NULL IDENTITY,
        [sathy_user_name] nvarchar(max) NOT NULL,
        [sathy_user_dob] nvarchar(max) NOT NULL,
        [sathy_user_phone_number] nvarchar(max) NOT NULL,
        [sathy_user_place] nvarchar(max) NOT NULL,
        [sathy_user_email] nvarchar(max) NOT NULL,
        [sathy_user_password] nvarchar(max) NOT NULL,
        [sathy_user_address] nvarchar(max) NOT NULL,
        [sathy_user_main_area] nvarchar(max) NOT NULL,
        [sathy_user_acc_status] nvarchar(max) NOT NULL,
        [sathy_user_mpin] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_sathy_users] PRIMARY KEY ([sathy_user_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    CREATE TABLE [user_reports] (
        [id] int NOT NULL IDENTITY,
        [reporter_name] nvarchar(max) NOT NULL,
        [reported_user] nvarchar(max) NOT NULL,
        [reason] nvarchar(max) NOT NULL,
        [status] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_user_reports] PRIMARY KEY ([id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260909132941_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260909132941_InitialCreate', N'10.0.12');
END;

COMMIT;
GO

