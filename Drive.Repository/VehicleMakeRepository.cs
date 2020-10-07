using AutoMapper;
using AutoMapper.QueryableExtensions;
using Drive.Common.Helpers;
using Drive.DAL;
using Drive.Model;
using Drive.Model.Common;
using Drive.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Drive.Repository
{
    public class VehicleMakeRepository : GenericRepository<VehicleMakeEntity>, IVehicleMakeRepository
    {
        private readonly DriveContext _context;
        private readonly DbSet<VehicleMakeEntity> dbSet;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;


        public VehicleMakeRepository(DriveContext context, IMapper mapper, MapperConfiguration mapperConfiguration) : base(context)
        {
            _context = context;
            dbSet = _context.Set<VehicleMakeEntity>();
           _mapper = mapper;
            _mapperConfiguration = mapperConfiguration;
        }
        public async Task<IEnumerable<IVehicleMake>> GetAll(Filtering filters, Sorting sorting, Paging paging)
        {
            IQueryable<VehicleMakeEntity> vehicles = dbSet;


            if (filters.ShouldApplyFilters())
            {
                vehicles = vehicles.Where(m => m.Name.Contains(filters.FilterBy) || m.Abbreviation.Contains(filters.FilterBy));
            }

            paging.TotalCount = vehicles.Count();
            switch (sorting.SortBy)
            {
                case "name_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Name);
                    break;

                case "Abrv":
                    vehicles = vehicles.OrderBy(v => v.Abbreviation);
                    break;

                case "abrv_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Abbreviation);
                    break;

                default:
                    vehicles = vehicles.OrderBy(v => v.Name);
                    break;
            }


            return await vehicles.Skip(paging.ItemsToSkip).Take(paging.NumberOfObjectsPerPage).ProjectTo<VehicleMake>(_mapperConfiguration).ToListAsync();
        }
        public async Task<IVehicleMake> FindById(object id)
        {
            VehicleMakeEntity vehicleEntity = await dbSet.FindAsync(id);
            IVehicleMake vehicleMake = _mapper.Map<VehicleMake>(vehicleEntity);
            return vehicleMake;
        }
        public async Task<bool> Create(IVehicleMake vehicleMake)
        {
            try
            {
                VehicleMakeEntity entity = _mapper.Map<VehicleMakeEntity>(vehicleMake);
                dbSet.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Edit(IVehicleMake entity)
        {
            try
            {
                VehicleMakeEntity vehicleEntity = _mapper.Map<VehicleMakeEntity>(entity);

                DbEntityEntry dbEntityEntry = _context.Entry(vehicleEntity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    dbSet.Attach(vehicleEntity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Delete(IVehicleMake entityToDelete)
        {
            try
            {
                VehicleMakeEntity vehicleEntity = _mapper.Map<VehicleMakeEntity>(entityToDelete);

                DbEntityEntry dbEntityEntry = _context.Entry(vehicleEntity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    dbSet.Attach(vehicleEntity);
                    dbSet.Remove(vehicleEntity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public  override async Task<bool> Delete(object id)
        {
            try
            {

                VehicleMakeEntity entity = await dbSet.FindAsync(id);
                 Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
