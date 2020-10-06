using Drive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        
        Task  SaveChangesAsync();
        new void Dispose();
        
    }
}
