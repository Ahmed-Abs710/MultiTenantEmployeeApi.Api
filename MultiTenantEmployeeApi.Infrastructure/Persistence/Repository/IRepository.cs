using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Infrastructure.Persistence.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<List<T>> GetPagedAsync(
            IQueryable<T> query,
            int skip,
            int take,
            CancellationToken cancellationToken);

        Task<int> CountAsync(
            IQueryable<T> query,
            CancellationToken cancellationToken);

        Task<T?> GetByIdAsync(Guid id);

        Task<List<T>> GetAllAsync();

        IQueryable<T> Query();

        Task AddAsync(T entity);

        void Update(T entity);

        void Remove(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
