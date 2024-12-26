using APIproject.Entity.Base;
using System.Linq.Expressions;

namespace APIproject.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? sort=null,
            bool IsDescending = false,
            bool IsTracking = false,
            int skip = 0,
            int take = 0,
            params string[]? includes);

        Task<T> GetbyIdAsync(int? id, params string[] includes);  
        
        Task AddAsync(T entity);

        void Delete(T entity);

        void Update(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>>? expression);
        Task<int> SaveChangesAsync();
    }
}
