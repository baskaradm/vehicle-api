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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleMakeRepository _vehicleMakeRepository;

        public VehicleMakeService(IUnitOfWork unitOfWork,  IVehicleMakeRepository vehicleMakeRepository)
        {
            _unitOfWork = unitOfWork;
            _vehicleMakeRepository = vehicleMakeRepository;
        }

        public async Task<IEnumerable<IVehicleMake>> GetVehicleMakes(Filtering filters, Sorting sorting, Paging paging)
        {
            

           

            IEnumerable<IVehicleMake> query = await _vehicleMakeRepository.GetAll(filters, sorting, paging);
           
            return query.ToList();
        }

        public async Task<IVehicleMake> GetVehicleMakeByID(object id)
        {
            return await _vehicleMakeRepository.FindById(id);
        }


        public async Task<bool> CreateVehicleMake(IVehicleMake vehicleMake)
        {
               
            try
            {
                await _vehicleMakeRepository.Create(vehicleMake);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditVehicleMake(IVehicleMake vehicleMake)
        {
           
            try
            {
                await _vehicleMakeRepository.Edit(vehicleMake);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteVehicleMake(object id)
        {
            try
            {
                await _vehicleMakeRepository.Delete(id);
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
