using Buyonida.Data.DAL;
using Buyonida.Data.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Data.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly BuyonidaContext _context;

        public Repository(BuyonidaContext context)
        {
            _context = context;
        }
        private DbSet<T> Table { get => _context.Set<T>(); }



        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? func = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool EnableTraking = false)
        {
            IQueryable<T> query = Table;
            if (!EnableTraking) query = query.AsNoTracking();
            if (include is not null) query = include(query);
            if (func != null) query = query.Where(func);
            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetAllAsyncByPaging(Expression<Func<T, bool>>? func = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool EnableTraking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<T> query = Table;
            if (!EnableTraking) query = query.AsNoTracking();
            if (include is not null) query = include(query);
            if (func != null) query = query.Where(func);
            if (orderBy != null)
                return await orderBy(query).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> func, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool EnableTraking = false)
        {
            IQueryable<T> query = Table;
            if (!EnableTraking) query = query.AsNoTracking();
            if (include is not null) query = include(query);
            

            return await query.Where(func).FirstOrDefaultAsync();
        }

        public Task<int> GetCountAsync(Expression<Func<T, bool>>? func = null)
        {
            Table.AsNoTracking();
            return Table.Where(func).CountAsync();
        }


        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task HardDeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));

        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
