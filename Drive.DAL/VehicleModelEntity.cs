using Drive.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace Drive.DAL
{
    public class VehicleModelEntity
    {
        [Key]
        public int ID { get; set; }

        
        public string Name { get; set; }

        
        public string Abbreviation { get; set; }

        public int VehicleMakeId { get; set; }


        public IVehicleMake VehicleMake { get; set; }
    }
}
