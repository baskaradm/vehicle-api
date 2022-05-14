using Drive.Model.Common;
using System.ComponentModel.DataAnnotations;


namespace Drive.Model
{
    public class VehicleMake : IVehicleMake
    {
        
        
        public int VehicleMakeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Abbreviation { get; set; }

        
        
    }
}
