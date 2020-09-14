using Drive.Common.Helpers;
using Drive.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drive.Service.Common
{
    public interface IVehicleModelService

    {
        Task<IEnumerable<IVehicleModel>> GetVehicleModelsAsync(Filtering filters, Sorting sorting, Paging paging);
        Task<IVehicleModel> GetVehicleModelByIDAsync(object id);
        Task<bool> CreateVehicleModel(IVehicleModel vehicleModel);
        Task<bool> DeleteVehicleModel(object id);
        Task<bool> EditVehicleModel(IVehicleModel vehicleModel);

        void Dispose();
    }
}
