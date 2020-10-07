using Drive.DAL;
using Drive.Repository.Common;
using System;
using System.Threading.Tasks;
namespace Drive.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DriveContext _context;
        

        public UnitOfWork(DriveContext context)
        {
            _context = context;
            

        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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
    

