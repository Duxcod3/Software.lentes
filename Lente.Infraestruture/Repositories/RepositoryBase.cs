using Lente.Domain.Interfaces;
using Lente.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;


namespace Lente.Infraestruture.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        
        protected readonly UsuarioContext _context;
        protected readonly DbSet<T> _dbSet;


        public RepositoryBase(UsuarioContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
