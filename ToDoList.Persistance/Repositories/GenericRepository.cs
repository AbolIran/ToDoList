using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoList.Application.Contracts.Persistence;

namespace ToDoList.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ToDoListDbContext _context;
        public GenericRepository(ToDoListDbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            try
            {
                var entry = _context.Entry(entity);
                var key = entry.Property("Id").CurrentValue;
                if (await _context.Set<T>().FindAsync(key) != null)
                {
                    throw new InvalidOperationException($"Entity with key {key} already exists.");
                }

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateException ex)
            {
                // Log detailed information
                Console.WriteLine($"Error occurred while adding entity: {ex.Message}");
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    throw new InvalidOperationException("A category with this Id already exists.");
                }
                throw;
            }
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exist(string id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Get(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        public async Task Update(T entity, List<Expression<Func<T, object>>> updatedProperties = null)
        {
            var entry = _context.Entry(entity);

            if (updatedProperties != null && updatedProperties.Count > 0)
            {
                // Mark only the specific properties as modified
                foreach (var property in updatedProperties)
                {
                    entry.Property(property).IsModified = true;
                }
            }
            else
            {
                // Mark all properties as modified (default behavior)
                entry.State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}
