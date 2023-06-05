using Library.Application.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> Entities;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            Entities = _context.Set<T>();  
        }

        // <summary>
        /// Retrieves an entity of type `T` with the specified `id` from the database.
        /// </summary>
        public async Task<T> GetById(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all entities of type `T` from the database.
        /// </summary>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public async Task<T> FindSingle(Expression<Func<T, bool>> expression)
        {
            //return await Entities.SingleOrDefaultAsync(expression);
            return await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves all entities of type `T` that match the specified filter criteria
        /// expressed as an `Expression<Func<T, bool>>` object.
        /// </summary>
        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> expression)
        {
            return await Entities.Where(expression).ToListAsync();
        }

        /// <summary>
        /// Adds a new entity of type `T` to the database.
        /// </summary>
        public async Task Add(T entity)
        {
            await Entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a collection of entities of type `T` to the database.
        /// </summary>
        public async Task AddRange(IEnumerable<T> entities)
        {
            Entities.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an existing entity of type `T` from the database.
        /// </summary>
        public async Task Remove(T entity)
        {
            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a collection of entities of type `T` from the database.
        /// </summary>
        public async Task RemoveRange(IEnumerable<T> entities)
        {
            Entities.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing entity of type `T` in the database.
        /// </summary>
        public async Task Update(T entity)
        {
            Entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateId(int id, T entity)
        {
            var existingEntity = await Entities.FindAsync(id);

            if (existingEntity == null)
                throw new ArgumentException("Entity with specified id not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

    }
}
