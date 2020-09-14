namespace Drive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMake",
                c => new
                    {
                        VehicleMakeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Abbreviation = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.VehicleMakeId);
            
            CreateTable(
                "dbo.VehicleModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Abbreviation = c.String(nullable: false, maxLength: 255),
                        VehicleMakeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VehicleModel");
            DropTable("dbo.VehicleMake");
        }
    }
}
