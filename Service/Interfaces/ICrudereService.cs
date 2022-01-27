using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICrudereService
    {
        IQueryable<T> Query<T>(bool forUpdate=true) where T : BaseEntity;
        T GetById<T>(string id) where T : BaseEntity;
        Task<T> GetByIdAsync<T>(string id) where T : BaseEntity;
        T GetById<T>(params object[] id) where T : class;
        Task<T> GetByIdAsync<T>(params object[] id) where T : class;
        IEnumerable<T> GetAll<T>(bool forUpdate = true) where T : BaseEntity;
        Task<List<T>> GetAllAsync<T>(bool forUpdate = true) where T : BaseEntity;
        IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate, bool forUpdate = true) where T : BaseEntity;
        IEnumerable<T> Where<T>(string predicate, bool forUpdate = false) where T : BaseEntity;
        Task<List<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate, bool forUpdate = true) where T : BaseEntity;
        void Insert<T>(T entity) where T : BaseEntity, new();
        void Update<T>(T entity) where T : BaseEntity;
        void Modify<T>(T entity) where T : BaseEntity;
        int Delete<T>(T entity) where T : BaseEntity;
        Task<int> DeleteAsync<T>(T entity) where T : BaseEntity;
        int Save();
        Task<int> SaveAsync();
        //List<SelectItem> SelectList<T>(
        //   Expression<Func<T, SelectItem>> selector) where T : BaseEntity;
        //List<SelectItem> Where<T>(Expression<Func<T, bool>> predicate,
        //   Expression<Func<T, SelectItem>> selector) where T : BaseEntity;
    }
}
