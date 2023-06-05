using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace TodoApplication.Repository
{
    public class Repository<TDContext> : IRepository where TDContext : DbContext
    {
        protected TDContext _context;

        public Repository(TDContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync<T>(T entity) where T : class
        {
            try
            {
                return await CreateAsync(entity, CancellationToken.None);
            }
            catch(Exception ex)
            {
                throw new DbUpdateException($"{ex.Message}", ex);
            }
        }

        public async Task<T> CreateAsync<T>(T entity, CancellationToken cancellationToken) where T : class
        {
            try
            {
                var data = await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return data.Entity;
            }
            catch(Exception ex)
            {
                throw new DbUpdateException($"{ex.Message}", ex);
            }
        }

        public async Task<T> DeleteAsync<T>(T entity) where T : class
        {
            try
            {
                return await DeleteAsync(entity, CancellationToken.None);
            }
            catch(Exception ex)
            {
                throw new DbUpdateException($"{ex.Message}", ex);
            }
            
        }

        public async Task<T> DeleteAsync<T>(T entity, CancellationToken cancellationToken) where T : class
        {
            try
            {
                var data = _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return data.Entity;
            }
            catch(Exception ex)
            {
                throw new DbUpdateException($"{ex.Message}", ex);
            }
        }

        async Task<T> IRepository.PatchAsync<T>(T entity)
        {
            try
            {
                return await PatchAsync(entity, CancellationToken.None);
            }
            catch(Exception ex)
            {
                throw new DbUpdateException($"{ex.Message}", ex);
            }
        }

        public async Task<T> PatchAsync<T>(T entity, CancellationToken cancellationToken) where T : class
        {
            try
            {
                var data = _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return data.Entity;
            }
            catch(Exception ex)
            {
                throw new DbUpdateException($"{ex.Message}", ex);
            }
        }

        public IQueryable<T> Query<T>() where T : class
        {
            try
            {
                return _context.Set<T>();
            }
            catch(Exception ex)
            {
                throw new DbUpdateException($"{ex.Message}", ex);
            }
        }
    }
}
