using Drive.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Drive.Repository.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Filtering filters, Sorting sorting, Paging paging );
            

        Task<T> FindById(object id);

        Task<bool> Create(T entity);

        Task<bool> Edit(T entity);

        Task<bool> Delete(object id);

       
        
    }
}
