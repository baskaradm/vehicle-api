using Drive.Common.Helpers;
using Drive.DAL;
using Drive.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Drive.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DriveContext _context;
        private DbSet<T> dbSet;


        public GenericRepository(DriveContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();

        }


        public virtual async Task<IEnumerable<T>> GetAll(Filtering filters, Sorting sorting, Paging paging) {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> FindById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Create(T entity)
        {
            try
            {
                dbSet.Add(entity);
                return true;
            }

            catch
            {
                return false;
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                DbEntityEntry dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _context.Set<T>().Attach(entity);
                    _context.Set<T>().Remove(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> Delete(object id)
        {
            try
            {
                T entityToDelete = await dbSet.FindAsync(id);
                Delete(entityToDelete);
                return true;
            }

            catch
            {
                return false;
            }
        }


        public virtual async Task<bool> Edit(T entity)
        {
            try
            {
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
    }
}
