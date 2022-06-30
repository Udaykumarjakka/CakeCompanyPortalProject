using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.DataAccess
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal CakeDbContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(CakeDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public virtual TEntity GetById(object id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }


        public virtual void Delete(object id)
        {
            try
            {
                TEntity entityToDelete = _dbSet.Find(id);
                Delete(entityToDelete);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public virtual void DeleteAll()
        {
            try
            {
                if (_dbSet.Any())
                {
                    _dbSet.RemoveRange(_dbSet.ToList());
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                _dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

    }
}
