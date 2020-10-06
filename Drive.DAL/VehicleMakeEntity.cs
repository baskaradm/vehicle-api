using Drive.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Drive.DAL
{
    public class VehicleMakeEntity {

       [Key]
       public int VehicleMakeId { get; set; }
       public string Name { get; set; }
       public string Abbreviation { get; set; }
       public virtual ICollection<IVehicleModel> VehicleModels { get; set; }
    }
}
