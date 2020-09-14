using Drive.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Drive.Model
{
    public class VehicleMake : IVehicleMake
    {
        
        [Key]
        public int VehicleMakeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Abbreviation { get; set; }

        public virtual ICollection<IVehicleModel> VehicleModels { get; set; }
        
    }
}
