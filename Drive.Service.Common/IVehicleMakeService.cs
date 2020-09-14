using Drive.Common.Helpers;
using Drive.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drive.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<IEnumerable<IVehicleMake>> GetVehicleMakes(Filtering filters, Sorting sorting, Paging paging);
        Task<IVehicleMake> GetVehicleMakeByID(object id);
        Task<bool> CreateVehicleMake(IVehicleMake vehicleMake);
        Task<bool> DeleteVehicleMake(object id);
        Task<bool> EditVehicleMake(IVehicleMake vehicleMake);

        void Dispose();
    }
}
