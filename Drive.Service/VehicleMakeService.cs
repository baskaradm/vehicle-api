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

        public VehicleMakeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<IVehicleMake>> GetVehicleMakes(Filtering filters, Sorting sorting, Paging paging)
        {
            Func<IQueryable<VehicleMake>, IOrderedQueryable<VehicleMake>> sortBy = null;
            Expression<Func<VehicleMake, bool>> filter = null;

            if (filters.ShouldApplyFilters())
            {
                filter = (v => v.Name.Contains(filters.FilterBy) || v.Abbreviation.Contains(filters.FilterBy));
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

                default:
                    sortBy = q => q.OrderBy(v => v.Name);
                    break;
            }

            IQueryable<IVehicleMake> query = await _unitOfWork.VehicleMakes.GetAll(filter: filter, orderBy: sortBy);
            paging.TotalCount = query.Count();

            return query.Skip(paging.ItemsToSkip).Take(paging.NumberOfObjectsPerPage).ToList();
        }

        public async Task<IVehicleMake> GetVehicleMakeByID(object id)
        {
            return await _unitOfWork.VehicleMakes.FindById(id);
        }


        public async Task<bool> CreateVehicleMake(IVehicleMake vehicleMake)
        {
            VehicleMake vehicleMakeToCreate = new VehicleMake
            {
                VehicleMakeId = vehicleMake.VehicleMakeId,
                Name = vehicleMake.Name,
                Abbreviation = vehicleMake.Abbreviation
            };
               
            try
            {
                await _unitOfWork.VehicleMakes.Create(vehicleMakeToCreate);
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
            VehicleMake vehicleMakeToUpdate = new VehicleMake
            {
                VehicleMakeId = vehicleMake.VehicleMakeId,
                Name = vehicleMake.Name,
                Abbreviation = vehicleMake.Abbreviation
                
            };
            try
            {
                await _unitOfWork.VehicleMakes.Edit(vehicleMakeToUpdate);
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
                await _unitOfWork.VehicleMakes.Delete(id);
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
