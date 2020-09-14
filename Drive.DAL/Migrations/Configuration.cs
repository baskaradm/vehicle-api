namespace Drive.DAL.Migrations
{
    using Drive.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Drive.DAL.DriveContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Drive.DAL.Context";
        }

        protected override void Seed(Drive.DAL.DriveContext context)
        {
            var vehiclemakes = new List<VehicleMake>
            {
            new VehicleMake{Name="Bayerische Motoren Werke",Abbreviation="BMW"},
            new VehicleMake{Name="Ford Motor Company",Abbreviation="Ford"},
            new VehicleMake{Name="Volkswagen",Abbreviation="VW"},

            };

            vehiclemakes.ForEach(v => context.VehicleMakes.Add(v));
            context.SaveChanges();
            var vehiclemodels = new List<VehicleModel>
            {
            new VehicleModel{Name="X5",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModel{Name="X6",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModel{Name="X1",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModel{Name="Mondeo",Abbreviation= "Ford",VehicleMakeId = 2},
            new VehicleModel{Name="Fiesta",Abbreviation= "Ford",VehicleMakeId = 2},
            new VehicleModel{Name="Golf5",Abbreviation= "VW",VehicleMakeId = 3},
            new VehicleModel{Name="Golf4",Abbreviation= "VW",VehicleMakeId = 3},

            };
            vehiclemodels.ForEach(v => context.VehicleModels.Add(v));
            context.SaveChanges();
        }
    }
}
