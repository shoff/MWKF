namespace AUSKF.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WORKDAMNIT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Guid(nullable: false, identity: true),
                        AddressLine1 = c.String(maxLength: 256),
                        AddressLine2 = c.String(),
                        City = c.String(maxLength: 256),
                        State = c.String(maxLength: 60),
                        ZipCode = c.String(maxLength: 10),
                        CreateUser = c.String(maxLength: 20),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUser = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.DojoMemberships",
                c => new
                    {
                        DojoMembershipId = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        DojoId = c.Guid(nullable: false),
                        EffectiveStart = c.DateTime(nullable: false),
                        EffectiveEnd = c.DateTime(nullable: false),
                        CreateUser = c.String(maxLength: 20),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUser = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DojoMembershipId)
                .ForeignKey("dbo.Dojos", t => t.DojoId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.DojoId);
            
            CreateTable(
                "dbo.Dojos",
                c => new
                    {
                        DojoId = c.Guid(nullable: false, identity: true),
                        FederationId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        PrimaryContactId = c.Guid(nullable: false),
                        DojoName = c.String(nullable: false),
                        Phone = c.String(maxLength: 13),
                        WebsiteUrl = c.String(maxLength: 512),
                        EmailAddress = c.String(maxLength: 512),
                        Notes = c.String(maxLength: 1024),
                        CreateUser = c.String(maxLength: 20),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUser = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DojoId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Federations", t => t.FederationId)
                .ForeignKey("dbo.Users", t => t.PrimaryContactId)
                .Index(t => t.FederationId)
                .Index(t => t.AddressId)
                .Index(t => t.PrimaryContactId);
            
            CreateTable(
                "dbo.Federations",
                c => new
                    {
                        FederationId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(maxLength: 512),
                        Phone = c.String(maxLength: 13),
                        WebsiteUrl = c.String(maxLength: 512),
                        Logo = c.Binary(),
                        CreateUser = c.String(maxLength: 20),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUser = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FederationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AddressId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        DisplayName = c.String(maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        MiddleName = c.String(maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Gender = c.String(nullable: false, maxLength: 1),
                        DateOfBirth = c.DateTime(nullable: false),
                        AuskfIdNumber = c.Int(nullable: false),
                        Password = c.String(maxLength: 256),
                        PasswordLastChangedDate = c.DateTime(nullable: false),
                        MaximumDaysBetweenPasswordChange = c.Int(nullable: false),
                        PostCount = c.Int(nullable: false),
                        TopicCount = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        Location = c.String(maxLength: 80),
                        LastSearch = c.String(maxLength: 256),
                        KendoRankId = c.Guid(nullable: false),
                        JoinedDate = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Notes = c.String(maxLength: 512),
                        UserProfileId = c.Int(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.KendoRanks", t => t.KendoRankId)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId)
                .Index(t => t.AddressId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.KendoRankId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.KendoRanks",
                c => new
                    {
                        KendoRankId = c.Guid(nullable: false, identity: true),
                        KendoRankName = c.String(nullable: false),
                        KendoRankNumeric = c.Int(nullable: false),
                        Eligibility = c.String(nullable: false, maxLength: 512),
                        MinimumRankOfExaminers = c.String(nullable: false, maxLength: 30),
                        NumberOfExaminers = c.Int(nullable: false),
                        ConsentingExaminersRequired = c.Int(nullable: false),
                        CreateUser = c.String(maxLength: 20),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUser = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KendoRankId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        UserLoginId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ProviderKey = c.String(maxLength: 256),
                        LoginProvider = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.UserLoginId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                        BirthDay = c.DateTime(),
                        Location = c.String(maxLength: 200),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Sig = c.String(maxLength: 512),
                        AllowHtmlSig = c.Boolean(nullable: false),
                        FacebookPage = c.String(maxLength: 512),
                        SkypeUserName = c.String(maxLength: 256),
                        TwitterName = c.String(maxLength: 50),
                        HomePage = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Guid(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 56),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.FederationMemberships",
                c => new
                    {
                        FederationMembershipId = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        FederationId = c.Guid(nullable: false),
                        MembershipYear = c.Int(nullable: false),
                        CreateUser = c.String(maxLength: 20),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUser = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FederationMembershipId)
                .ForeignKey("dbo.Federations", t => t.FederationId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FederationId);
            
            CreateTable(
                "dbo.FederationOfficers",
                c => new
                    {
                        FederationOfficerId = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        OfficerRoleId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 512),
                        EffectiveStart = c.DateTime(nullable: false),
                        EffectiveEnd = c.DateTime(nullable: false),
                        CreateUser = c.String(maxLength: 20),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUser = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FederationOfficerId)
                .ForeignKey("dbo.OfficerRoles", t => t.OfficerRoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.OfficerRoleId);
            
            CreateTable(
                "dbo.OfficerRoles",
                c => new
                    {
                        OfficerRoleId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreateUser = c.String(maxLength: 20),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUser = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OfficerRoleId);
            
            CreateTable(
                "dbo.LogTable",
                c => new
                    {
                        LogId = c.Guid(nullable: false),
                        DateCreated = c.DateTime(),
                        LogLevel = c.String(maxLength: 10),
                        Logger = c.String(maxLength: 128),
                        Message = c.String(),
                        MessageId = c.String(),
                        WindowsUserName = c.String(maxLength: 256),
                        CallSite = c.String(maxLength: 256),
                        ThreadId = c.String(maxLength: 128),
                        Exception = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.LogId);
            
            CreateTable(
                "dbo.UserUserRoles",
                c => new
                    {
                        User_Id = c.Guid(nullable: false),
                        UserRole_RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserRole_RoleId })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_RoleId, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.UserRole_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FederationOfficers", "UserId", "dbo.Users");
            DropForeignKey("dbo.FederationOfficers", "OfficerRoleId", "dbo.OfficerRoles");
            DropForeignKey("dbo.FederationMemberships", "UserId", "dbo.Users");
            DropForeignKey("dbo.FederationMemberships", "FederationId", "dbo.Federations");
            DropForeignKey("dbo.DojoMemberships", "UserId", "dbo.Users");
            DropForeignKey("dbo.DojoMemberships", "DojoId", "dbo.Dojos");
            DropForeignKey("dbo.Dojos", "PrimaryContactId", "dbo.Users");
            DropForeignKey("dbo.UserUserRoles", "UserRole_RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserUserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "KendoRankId", "dbo.KendoRanks");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Dojos", "FederationId", "dbo.Federations");
            DropForeignKey("dbo.Dojos", "AddressId", "dbo.Addresses");
            DropIndex("dbo.UserUserRoles", new[] { "UserRole_RoleId" });
            DropIndex("dbo.UserUserRoles", new[] { "User_Id" });
            DropIndex("dbo.FederationOfficers", new[] { "OfficerRoleId" });
            DropIndex("dbo.FederationOfficers", new[] { "UserId" });
            DropIndex("dbo.FederationMemberships", new[] { "FederationId" });
            DropIndex("dbo.FederationMemberships", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "UserProfileId" });
            DropIndex("dbo.Users", new[] { "KendoRankId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.Dojos", new[] { "PrimaryContactId" });
            DropIndex("dbo.Dojos", new[] { "AddressId" });
            DropIndex("dbo.Dojos", new[] { "FederationId" });
            DropIndex("dbo.DojoMemberships", new[] { "DojoId" });
            DropIndex("dbo.DojoMemberships", new[] { "UserId" });
            DropTable("dbo.UserUserRoles");
            DropTable("dbo.LogTable");
            DropTable("dbo.OfficerRoles");
            DropTable("dbo.FederationOfficers");
            DropTable("dbo.FederationMemberships");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.KendoRanks");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Federations");
            DropTable("dbo.Dojos");
            DropTable("dbo.DojoMemberships");
            DropTable("dbo.Addresses");
        }
    }
}
