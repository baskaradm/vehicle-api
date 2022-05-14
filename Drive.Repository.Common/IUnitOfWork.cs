using System;
using System.Threading.Tasks;

namespace Drive.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
       
        Task  SaveChangesAsync();
        new void Dispose();
        
    }
}
