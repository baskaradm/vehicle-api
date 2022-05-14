using Drive.Common.Helpers;
using Drive.DAL;
using Drive.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        Task<IEnumerable<IVehicleMake>> GetAll(Filtering filters, Sorting sorting, Paging paging);
        Task<IVehicleMake> FindById(object id);
        Task<bool> Create(IVehicleMake entity);
        Task<bool> Edit(IVehicleMake entity);
        void Delete(IVehicleMake entityToDelete);
        Task<bool> Delete(object id);
    }
}
    

