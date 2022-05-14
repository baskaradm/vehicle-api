namespace Drive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _InitalCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMakeEntity",
                c => new
                    {
                        VehicleMakeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.VehicleMakeId);
            
            CreateTable(
                "dbo.VehicleModelEntity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Abbreviation = c.String(),
                        VehicleMakeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VehicleModelEntity");
            DropTable("dbo.VehicleMakeEntity");
        }
    }
}
