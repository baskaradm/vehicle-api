using Drive.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace Drive.DAL
{
    public class DriveContext : DbContext
    {
        public DriveContext() : base("DriveContext")
        {
            Database.Log = sql => Debug.Write(sql);

        }

        public DbSet<VehicleMake>VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
