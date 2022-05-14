using Drive.Common.Helpers;
using Drive.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Repository.Common
{
    public interface IVehicleModelRepository
    {


        Task<IEnumerable<IVehicleModel>> GetAll(Filtering filters, Sorting sorting, Paging paging);
        Task<IVehicleModel> FindById(object id);
        Task<bool> Create(IVehicleModel entity);
        Task<bool> Edit(IVehicleModel entity);
        void Delete(IVehicleModel entityToDelete);
        Task<bool> Delete(object id);
    }
}
 

