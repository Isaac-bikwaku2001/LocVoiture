namespace LocVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategorieID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CategorieID);
            
            CreateTable(
                "dbo.Modeles",
                c => new
                    {
                        ModeleID = c.Int(nullable: false, identity: true),
                        Marque = c.String(nullable: false, maxLength: 100),
                        Serie = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ModeleID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        VoitureID = c.Int(nullable: false),
                        LongueDuree = c.Boolean(nullable: false),
                        DateDebut = c.DateTime(nullable: false, storeType: "date"),
                        DateFin = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.LocationID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Voitures", t => t.VoitureID, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.VoitureID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nom = c.String(nullable: false, maxLength: 200),
                        Prenom = c.String(nullable: false, maxLength: 200),
                        CIN = c.String(nullable: false),
                        PermisConduire = c.String(nullable: false),
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
                "dbo.Voitures",
                c => new
                    {
                        VoitureID = c.Int(nullable: false, identity: true),
                        NumImmatriculation = c.String(nullable: false, maxLength: 20),
                        CategorieID = c.Int(nullable: false),
                        ModeleID = c.Int(nullable: false),
                        DateMiseEnCirculation = c.DateTime(nullable: false, storeType: "date"),
                        TypeCarburant = c.String(nullable: false, maxLength: 30),
                        TypeTransmission = c.String(nullable: false, maxLength: 50),
                        Kilometrage = c.Int(nullable: false),
                        Place = c.Int(nullable: false),
                        Bagage = c.Int(nullable: false),
                        PrixLocation = c.Double(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VoitureID)
                .ForeignKey("dbo.Categories", t => t.CategorieID, cascadeDelete: true)
                .ForeignKey("dbo.Modeles", t => t.ModeleID, cascadeDelete: true)
                .Index(t => t.NumImmatriculation, unique: true)
                .Index(t => t.CategorieID)
                .Index(t => t.ModeleID);
            
            CreateTable(
                "dbo.AspNetRoles",
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reservations", "VoitureID", "dbo.Voitures");
            DropForeignKey("dbo.Voitures", "ModeleID", "dbo.Modeles");
            DropForeignKey("dbo.Voitures", "CategorieID", "dbo.Categories");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Voitures", new[] { "ModeleID" });
            DropIndex("dbo.Voitures", new[] { "CategorieID" });
            DropIndex("dbo.Voitures", new[] { "NumImmatriculation" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Reservations", new[] { "VoitureID" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Voitures");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Reservations");
            DropTable("dbo.Modeles");
            DropTable("dbo.Categories");
        }
    }
}
