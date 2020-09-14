using Drive.DAL;
using Drive.Model;
using Drive.Repository.Common;
using System;
using System.Threading.Tasks;
using System.Transactions;
namespace Drive.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DriveContext _context;
        private IGenericRepository<VehicleMake> _vehicleMakeRepository;
        private IGenericRepository<VehicleModel> _vehicleModelRepository;

        public UnitOfWork(DriveContext context, IGenericRepository<VehicleMake> vehicleMakeRepository, IGenericRepository<VehicleModel> vehicleModelRepository)
        {
            _context = context;
            _vehicleMakeRepository = vehicleMakeRepository;
            _vehicleModelRepository = vehicleModelRepository;

        }


        public IGenericRepository<VehicleMake> VehicleMakes
        {
            get
            {
                if (_vehicleMakeRepository == null)
                {
                    _vehicleMakeRepository = new GenericRepository<VehicleMake>(_context);
                }
                return _vehicleMakeRepository;
            }
        }

        public IGenericRepository<VehicleModel> VehicleModels
        {
            get
            {
                if (_vehicleModelRepository == null)
                {
                    _vehicleModelRepository = new GenericRepository<VehicleModel>(_context);
                }
                return _vehicleModelRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await _context.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }
       
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
    

