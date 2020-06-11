namespace GraduationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.AdminID)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminID)
                .Index(t => t.AdminID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        ProfilePicture = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        PublicationYear = c.Int(nullable: false),
                        Category = c.String(),
                        PublicationCountry = c.String(),
                        Price = c.Single(nullable: false),
                        Currency = c.String(),
                        ISBN = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        ApprovalStatus = c.Int(nullable: false),
                        Summary = c.String(),
                        NumberSearched = c.Int(nullable: false),
                        CoverImage = c.String(),
                        Notes = c.String(),
                        PublisherID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Publishers", t => t.PublisherID)
                .Index(t => t.PublisherID);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherID = c.String(nullable: false, maxLength: 128),
                        Country = c.String(),
                        Governorate = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        ApprovalStatus = c.Int(nullable: false),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.PublisherID)
                .ForeignKey("dbo.AspNetUsers", t => t.PublisherID)
                .Index(t => t.PublisherID);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        VisitorID = c.String(nullable: false, maxLength: 128),
                        BookID = c.Int(nullable: false),
                        RateValue = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.VisitorID, t.BookID })
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Visitors", t => t.VisitorID, cascadeDelete: true)
                .Index(t => t.VisitorID)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        VisitorID = c.String(nullable: false, maxLength: 128),
                        Country = c.String(),
                        Governorate = c.String(),
                        City = c.String(),
                        Street = c.String(),
                    })
                .PrimaryKey(t => t.VisitorID)
                .ForeignKey("dbo.AspNetUsers", t => t.VisitorID)
                .Index(t => t.VisitorID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.BookID })
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.BookID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Admins", "AdminID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Favourites", "BookID", "dbo.Books");
            DropForeignKey("dbo.Favourites", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Visitors", "VisitorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rates", "VisitorID", "dbo.Visitors");
            DropForeignKey("dbo.Rates", "BookID", "dbo.Books");
            DropForeignKey("dbo.Publishers", "PublisherID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "PublisherID", "dbo.Publishers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Favourites", new[] { "BookID" });
            DropIndex("dbo.Favourites", new[] { "UserID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Visitors", new[] { "VisitorID" });
            DropIndex("dbo.Rates", new[] { "BookID" });
            DropIndex("dbo.Rates", new[] { "VisitorID" });
            DropIndex("dbo.Publishers", new[] { "PublisherID" });
            DropIndex("dbo.Books", new[] { "PublisherID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Admins", new[] { "AdminID" });
            DropTable("dbo.Favourites");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Visitors");
            DropTable("dbo.Rates");
            DropTable("dbo.Publishers");
            DropTable("dbo.Books");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Admins");
        }
    }
}
