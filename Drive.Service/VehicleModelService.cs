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

        public VehicleModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<IVehicleModel>> GetVehicleModelsAsync(Filtering filters, Sorting sorting, Paging paging)
        {
            Func<IQueryable<VehicleModel>, IOrderedQueryable<VehicleModel>> sortBy = null;
            Expression<Func<VehicleModel, bool>> filter = null;

            if (filters.ShouldApplyFilters())
            {
                filter = (v => v.Name.Contains(filters.FilterBy)
                                    || v.Abbreviation.Contains(filters.FilterBy)
                                    || v.VehicleMakeId.ToString().Contains(filters.FilterBy));
            }


            switch (sorting.SortBy)
            {
                case "name_desc":
                    sortBy = q => q.OrderByDescending(v => v.Name);
                    break;

                case "abrv":
                    sortBy = q => q.OrderBy(v => v.Abbreviation);
                    break;

                case "abrv_desc":
                    sortBy = q => q.OrderByDescending(v => v.Abbreviation);
                    break;

               
                    
                case "VehicleMakeId":
                    sortBy = q => q.OrderBy(v => v.VehicleMakeId);
                    break;

                case "VehicleMakeId_desc":
                    sortBy = q => q.OrderByDescending(v => v.VehicleMakeId);
                    break;

                default:
                    sortBy = q => q.OrderBy(v => v.Name);
                    break;
            }

            IQueryable<IVehicleModel> query = await _unitOfWork.VehicleModels.GetAll(filter: filter, orderBy: sortBy);
            paging.TotalCount = query.Count();

            return query.Skip(paging.ItemsToSkip).Take(paging.NumberOfObjectsPerPage).ToList();
        }

        public async Task<IVehicleModel> GetVehicleModelByIDAsync(object id)
        {
            return await _unitOfWork.VehicleModels.FindById(id);
        }


        public async Task<bool> CreateVehicleModel(IVehicleModel vehicleModel)
        {
            VehicleModel vehicleModelToCreate = new VehicleModel
            {
                Name = vehicleModel.Name,
                Abbreviation = vehicleModel.Abbreviation,
                VehicleMakeId = vehicleModel.VehicleMakeId,
                ID = vehicleModel.ID
            };

            try
            {
                await _unitOfWork.VehicleModels.Create(vehicleModelToCreate);
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
            VehicleModel vehicleModelToUpdate = new VehicleModel
            {
                Name = vehicleModel.Name,
                Abbreviation = vehicleModel.Abbreviation,
                VehicleMakeId = vehicleModel.VehicleMakeId,
                ID = vehicleModel.ID

            };
            try
            {
                await _unitOfWork.VehicleModels.Edit(vehicleModelToUpdate);
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
                await _unitOfWork.VehicleModels.Delete(id);
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
