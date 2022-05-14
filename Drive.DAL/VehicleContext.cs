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

        public DbSet<VehicleMakeEntity>VehicleMakes { get; set; }
        public DbSet<VehicleModelEntity> VehicleModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
