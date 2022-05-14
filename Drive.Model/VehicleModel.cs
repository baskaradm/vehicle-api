using Drive.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace Drive.Model
{
    public class VehicleModel : IVehicleModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Abbreviation { get; set; }
       
        public int VehicleMakeId { get; set; }

        
       

    }
}
