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
    public class VehicleModelRepository : GenericRepository<VehicleModelEntity>, IVehicleModelRepository
    {
        private readonly DriveContext _context;
        private readonly DbSet<VehicleModelEntity> dbSet;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        public VehicleModelRepository(DriveContext context, IMapper mapper, MapperConfiguration mapperConfiguration) : base(context)
        {
            _context = context;
            dbSet = _context.Set<VehicleModelEntity>();
           _mapper = mapper;
            _mapperConfiguration = mapperConfiguration;
        }
        public async Task<IEnumerable<IVehicleModel>> GetAll(Filtering filters, Sorting sorting, Paging paging)
        {
            IQueryable<VehicleModelEntity> models = dbSet;

            if (filters.ShouldApplyFilters())
            {
                models = models.Where(m => m.Name.Contains(filters.FilterBy)
                                    || m.Abbreviation.Contains(filters.FilterBy)
                                    || m.VehicleMakeId.ToString().Contains(filters.FilterBy));
            }

            paging.TotalCount = models.Count();

            switch (sorting.SortBy)
            {
                case "name_desc":
                    models = models.OrderByDescending(v => v.Name);
                    break;

                case "Abrv":
                    models = models.OrderBy(v => v.Abbreviation);
                    break;

                case "abrv_desc":
                    models = models.OrderByDescending(v => v.Abbreviation);
                    break;

                case "VehicleMakeId":
                    models = models.OrderBy(v => v.VehicleMakeId);
                    break;

                case "vehiclemakeid_desc":
                    models = models.OrderByDescending(v => v.VehicleMakeId);
                    break;

                default:
                    models = models.OrderBy(v => v.Name);
                    break;
            }
            return await models.Skip(paging.ItemsToSkip).Take(paging.NumberOfObjectsPerPage).ProjectTo<VehicleModel>(_mapperConfiguration).ToListAsync();
        }
        public async Task<IVehicleModel> FindById(object id)
        {
            VehicleModelEntity entity = await dbSet.FindAsync(id);
            IVehicleModel model = _mapper.Map<VehicleModel>(entity);
            return model;
        }
        public async Task<bool> Create(IVehicleModel vehicleModel)
        {
            try
            {
                VehicleModelEntity entity = _mapper.Map<VehicleModelEntity>(vehicleModel);
                dbSet.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Edit(IVehicleModel vehicleModel)
        {
            try
            {
                VehicleModelEntity entity = _mapper.Map<VehicleModelEntity>(vehicleModel);
                DbEntityEntry dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Delete(IVehicleModel vehicleModel)
        {
            try
            {
                VehicleModelEntity entityToDelete = _mapper.Map<VehicleModelEntity>(vehicleModel);
                DbEntityEntry dbEntityEntry = _context.Entry(entityToDelete);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    dbSet.Attach(entityToDelete);
                    dbSet.Remove(entityToDelete);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override async Task<bool> Delete(object id)
        {
            try
            {
                VehicleModelEntity entity = await dbSet.FindAsync(id);
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
