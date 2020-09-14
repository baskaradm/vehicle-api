using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Drive.Repository.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll
            (Expression<Func<T, bool>>  filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includeProperties = "");

        Task<T> FindById(object id);

        Task<bool> Create(T entity);

        Task<bool> Edit(T entity);

        void Delete(T entity);

        Task<bool> Delete(object id);

       
        
    }
}
