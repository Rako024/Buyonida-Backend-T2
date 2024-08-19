using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Data.Repositories.Abstracts
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IList<T>> GetAllAsync
            (
            Expression<Func<T, bool>>? func = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool EnableTraking = false
            );
        Task<IList<T>> GetAllAsyncByPaging
            (
            Expression<Func<T, bool>>? func = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool EnableTraking = false,
            int currentPage = 1,
            int pageSize = 3
            );
        Task<T> GetAsync
            (
            Expression<Func<T, bool>> func,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool EnableTraking = false
            );

        Task<int> GetCountAsync(Expression<Func<T, bool>>? func = null);

        Task AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task HardDeleteAsync(T entity);
        Task<int> CommitAsync();
    }
}
