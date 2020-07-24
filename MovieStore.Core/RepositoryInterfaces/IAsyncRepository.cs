using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        // base interface with all CRUD operations
        // create read update delete

        // default value is null, you don't have to pass in a condition
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }

    //public interface IAsyncRepository<T> where T : class
    //{
    //    // base interface with all CRUD operations
    //    // create read update delete
    //    T GetByID(int id);
    //    IEnumerable<T> ListAll();
    //    IEnumerable<T> ListWhere(Expression<Func<T, bool>> filter);
    //    // default value is null, you don't have to pass in a condition
    //    int GetCount(Expression<Func<T, bool>> filter = null);
    //    bool GetExists(Expression<Func<T, bool>> filter = null);
    //    T Add(T entity);
    //    T Update(T entity);
    //    void Delete(T entity);
    //} 
}
