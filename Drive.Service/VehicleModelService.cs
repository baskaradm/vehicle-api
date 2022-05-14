using Drive.Common.Helpers;
using Drive.Model;
using Drive.Model.Common;
using Drive.Repository.Common;
using Drive.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Drive.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleModelRepository _vehicleModelRepository;

        public VehicleModelService(IUnitOfWork unitOfWork, IVehicleModelRepository vehicleModelRepository)
        {
            _unitOfWork = unitOfWork;
            _vehicleModelRepository = vehicleModelRepository;
        }

        public async Task<IEnumerable<IVehicleModel>> GetVehicleModelsAsync(Filtering filters, Sorting sorting, Paging paging)
        {
           IEnumerable<IVehicleModel> query = await _vehicleModelRepository.GetAll(filters, sorting, paging);
            

            return query.ToList();
        }

        public async Task<IVehicleModel> GetVehicleModelByIDAsync(object id)
        {
            return await _vehicleModelRepository.FindById(id);
        }


        public async Task<bool> CreateVehicleModel(IVehicleModel vehicleModel)
        {
           

            try
            {
                await _vehicleModelRepository.Create(vehicleModel);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditVehicleModel(IVehicleModel vehicleModel)
        {
            
            try
            {
                await _vehicleModelRepository.Edit(vehicleModel);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteVehicleModel(object id)
        {
            try
            {
                await _vehicleModelRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
