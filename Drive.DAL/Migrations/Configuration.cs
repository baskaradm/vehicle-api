namespace Drive.DAL.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DriveContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Drive.DAL.DriveContext";
        }

        protected override void Seed(DriveContext context)
        {
            var vehiclemakes = new List<VehicleMakeEntity>
            {
            new VehicleMakeEntity{Name="Bayerische Motoren Werke",Abbreviation="BMW"},
            new VehicleMakeEntity{Name="Ford Motor Company",Abbreviation="Ford"},
            new VehicleMakeEntity{Name="Volkswagen",Abbreviation="VW"},
            new VehicleMakeEntity{Name="Chevrolet",Abbreviation="Chevy"},


            };

            vehiclemakes.ForEach(v => context.VehicleMakes.Add(v));
            context.SaveChanges();
            var vehiclemodels = new List<VehicleModelEntity>
            {
            new VehicleModelEntity{Name="X5",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModelEntity{Name="X6",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModelEntity{Name="X1",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModelEntity{Name="Mondeo",Abbreviation= "Ford",VehicleMakeId = 2},
            new VehicleModelEntity{Name="Fiesta",Abbreviation= "Ford",VehicleMakeId = 2},
            new VehicleModelEntity{Name="Golf5",Abbreviation= "VW",VehicleMakeId = 3},
            new VehicleModelEntity{Name="Golf4",Abbreviation= "VW",VehicleMakeId = 3},

            };
            vehiclemodels.ForEach(v => context.VehicleModels.Add(v));
            context.SaveChanges();
        }
    }
}
