using Core.Entities;
using Infra;
using Service.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Dynamic;

namespace Service
{
    public class CrudereService : ICrudereService
    {
        protected readonly Db _dbContext;
        public CrudereService(Db dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Query<T>(bool forUpdate = true) where T : BaseEntity
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (!forUpdate)
            {
                query.AsNoTracking();
            }
            foreach (var property in _dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
            {
                query = query.Include(property.Name);
            }
            return query;
        }

        public T GetById<T>(string id) where T : BaseEntity
        {
            return Query<T>().SingleOrDefault(m => m.Id == id);
        }

        public async Task<T> GetByIdAsync<T>(string id) where T : BaseEntity
        {
            return await Query<T>().SingleOrDefaultAsync(m => m.Id == id);
        }

        public T GetById<T>(params object[] id) where T : class
        {
            return _dbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync<T>(params object[] id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll<T>(bool forUpdate = false) where T : BaseEntity
        {
            return Query<T>(forUpdate).AsEnumerable<T>();
        }

        public async Task<List<T>> GetAllAsync<T>(bool forUpdate = false) where T : BaseEntity
        {
            return await Query<T>(forUpdate).ToListAsync();
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate, bool forUpdate = false) where T : BaseEntity
        {
            return Query<T>(forUpdate).Where(predicate).AsEnumerable<T>();
        }

        public IEnumerable<T> Where<T>(string predicate, bool forUpdate = false) where T : BaseEntity
        {
            return Query<T>(forUpdate).Where(@predicate).AsEnumerable<T>();
        }

        public async Task<List<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate, bool forUpdate = false) where T : BaseEntity
        {
            return await Query<T>(forUpdate).Where(predicate).ToListAsync();
        }

        public void Insert<T>(T entity) where T : BaseEntity, new()
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Modify<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public int Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            return Save();
        }

        public async Task<int> DeleteAsync<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            return await SaveAsync();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
