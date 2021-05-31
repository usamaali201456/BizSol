namespace BizSol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ad_post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Price = c.Int(nullable: false),
                        Condition = c.String(),
                        Detail = c.String(),
                        UserID = c.String(maxLength: 128),
                        cat_Id = c.Int(),
                        fk_vehicle_Id = c.Int(),
                        fkBikeid = c.Int(),
                        fkMobileId = c.Int(),
                        fkElectronicId = c.Int(),
                        fkClothid = c.Int(),
                        fkPropertyId = c.Int(),
                        WhenAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FkCityId = c.Int(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.bike", t => t.fkBikeid)
                .ForeignKey("dbo.Category", t => t.cat_Id)
                .ForeignKey("dbo.city", t => t.FkCityId)
                .ForeignKey("dbo.cloth", t => t.fkClothid)
                .ForeignKey("dbo.electronic", t => t.fkElectronicId)
                .ForeignKey("dbo.Mobiles", t => t.fkMobileId)
                .ForeignKey("dbo.property", t => t.fkPropertyId)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.vehicle", t => t.fk_vehicle_Id)
                .Index(t => t.UserID)
                .Index(t => t.cat_Id)
                .Index(t => t.fk_vehicle_Id)
                .Index(t => t.fkBikeid)
                .Index(t => t.fkMobileId)
                .Index(t => t.fkElectronicId)
                .Index(t => t.fkClothid)
                .Index(t => t.fkPropertyId)
                .Index(t => t.FkCityId);
            
            CreateTable(
                "dbo.ads_images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        AdId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ad_post", t => t.AdId)
                .Index(t => t.AdId);
            
            CreateTable(
                "dbo.bike",
                c => new
                    {
                        BikeID = c.Int(nullable: false, identity: true),
                        BikeMileage = c.Int(nullable: false),
                        BrandName = c.String(),
                        IsFileAvailable = c.String(),
                        BikeYear = c.Int(nullable: false),
                        fk_Cat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.BikeID)
                .ForeignKey("dbo.Category", t => t.fk_Cat_Id)
                .Index(t => t.fk_Cat_Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        cat_Id = c.Int(nullable: false, identity: true),
                        cat_name = c.String(),
                        cat_Image = c.String(),
                        ad_Id = c.Int(),
                        cat_Status = c.Int(),
                    })
                .PrimaryKey(t => t.cat_Id);
            
            CreateTable(
                "dbo.cloth",
                c => new
                    {
                        ClothId = c.Int(nullable: false, identity: true),
                        ClothType = c.String(),
                        Color = c.String(),
                        Size = c.String(),
                        FkCatId = c.Int(),
                    })
                .PrimaryKey(t => t.ClothId)
                .ForeignKey("dbo.Category", t => t.FkCatId)
                .Index(t => t.FkCatId);
            
            CreateTable(
                "dbo.electronic",
                c => new
                    {
                        ElectronicId = c.Int(nullable: false, identity: true),
                        ModelYear = c.Int(nullable: false),
                        BrandName = c.String(),
                        FkCatID = c.Int(),
                    })
                .PrimaryKey(t => t.ElectronicId)
                .ForeignKey("dbo.Category", t => t.FkCatID)
                .Index(t => t.FkCatID);
            
            CreateTable(
                "dbo.Mobiles",
                c => new
                    {
                        MobilesId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        fkCatId = c.Int(),
                    })
                .PrimaryKey(t => t.MobilesId)
                .ForeignKey("dbo.Category", t => t.fkCatId)
                .Index(t => t.fkCatId);
            
            CreateTable(
                "dbo.property",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        AreaType = c.String(),
                        PropertyArea = c.Int(nullable: false),
                        WashRoom = c.Int(nullable: false),
                        BedRoom = c.Int(nullable: false),
                        IsCarParking = c.String(),
                        FKCatId = c.Int(),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Category", t => t.FKCatId)
                .Index(t => t.FKCatId);
            
            CreateTable(
                "dbo.vehicle",
                c => new
                    {
                        Vehicle_Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        IsFileAvailable = c.String(),
                        Vehicle_Milage = c.Int(nullable: false),
                        fk_cat_Id = c.Int(),
                        Vehicle_Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Vehicle_Id)
                .ForeignKey("dbo.Category", t => t.fk_cat_Id)
                .Index(t => t.fk_cat_Id);
            
            CreateTable(
                "dbo.city",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        CityImage = c.String(),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ResetPasswordCode = c.String(),
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
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.contact",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        Message_ = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.ad_post", "fk_vehicle_Id", "dbo.vehicle");
            DropForeignKey("dbo.ad_post", "UserID", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.ad_post", "fkPropertyId", "dbo.property");
            DropForeignKey("dbo.ad_post", "fkMobileId", "dbo.Mobiles");
            DropForeignKey("dbo.ad_post", "fkElectronicId", "dbo.electronic");
            DropForeignKey("dbo.ad_post", "fkClothid", "dbo.cloth");
            DropForeignKey("dbo.ad_post", "FkCityId", "dbo.city");
            DropForeignKey("dbo.ad_post", "cat_Id", "dbo.Category");
            DropForeignKey("dbo.ad_post", "fkBikeid", "dbo.bike");
            DropForeignKey("dbo.bike", "fk_Cat_Id", "dbo.Category");
            DropForeignKey("dbo.vehicle", "fk_cat_Id", "dbo.Category");
            DropForeignKey("dbo.property", "FKCatId", "dbo.Category");
            DropForeignKey("dbo.Mobiles", "fkCatId", "dbo.Category");
            DropForeignKey("dbo.electronic", "FkCatID", "dbo.Category");
            DropForeignKey("dbo.cloth", "FkCatId", "dbo.Category");
            DropForeignKey("dbo.ads_images", "AdId", "dbo.ad_post");
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.vehicle", new[] { "fk_cat_Id" });
            DropIndex("dbo.property", new[] { "FKCatId" });
            DropIndex("dbo.Mobiles", new[] { "fkCatId" });
            DropIndex("dbo.electronic", new[] { "FkCatID" });
            DropIndex("dbo.cloth", new[] { "FkCatId" });
            DropIndex("dbo.bike", new[] { "fk_Cat_Id" });
            DropIndex("dbo.ads_images", new[] { "AdId" });
            DropIndex("dbo.ad_post", new[] { "FkCityId" });
            DropIndex("dbo.ad_post", new[] { "fkPropertyId" });
            DropIndex("dbo.ad_post", new[] { "fkClothid" });
            DropIndex("dbo.ad_post", new[] { "fkElectronicId" });
            DropIndex("dbo.ad_post", new[] { "fkMobileId" });
            DropIndex("dbo.ad_post", new[] { "fkBikeid" });
            DropIndex("dbo.ad_post", new[] { "fk_vehicle_Id" });
            DropIndex("dbo.ad_post", new[] { "cat_Id" });
            DropIndex("dbo.ad_post", new[] { "UserID" });
            DropTable("dbo.Role");
            DropTable("dbo.contact");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.city");
            DropTable("dbo.vehicle");
            DropTable("dbo.property");
            DropTable("dbo.Mobiles");
            DropTable("dbo.electronic");
            DropTable("dbo.cloth");
            DropTable("dbo.Category");
            DropTable("dbo.bike");
            DropTable("dbo.ads_images");
            DropTable("dbo.ad_post");
        }
    }
}
