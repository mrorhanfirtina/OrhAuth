using OrhAuth.Data.Repositories.Abstract;
using OrhAuth.Models.Entities.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace OrhAuth.Data.Repositories.Concrete
{
    /// <summary>
    /// Entity Framework-based implementation of the IEntityRepository interface.
    /// Provides generic CRUD operations for entities using DbContext.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity, which must inherit from EntityBase.</typeparam>
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : EntityBase
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the repository with the specified database context.
        /// </summary>
        /// <param name="context">The DbContext to be used for data access operations.</param>
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Initializes a new instance of the repository with the specified connection string.
        /// Creates a new AuthDbContext<User> instance internally.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public EfEntityRepositoryBase(string connectionString)
        {
            _context = new OrhAuth.Data.Context.AuthDbContext<OrhAuth.Models.Entities.User>(connectionString);
        }

        /// <summary>
        /// Retrieves a single entity that matches the given filter expression.
        /// </summary>
        /// <param name="filter">The filter expression to apply.</param>
        /// <returns>The entity that matches the filter or null if not found.</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        /// <summary>
        /// Retrieves a list of entities matching the provided filter.
        /// If no filter is given, returns all entities of the specified type.
        /// </summary>
        /// <param name="filter">An optional filter expression.</param>
        /// <returns>A list of matching entities.</returns>
        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? _context.Set<TEntity>().ToList()
                : _context.Set<TEntity>().Where(filter).ToList();
        }

        /// <summary>
        /// Adds a new entity to the database and saves the changes.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an existing entity in the database and sets its updated date.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        public void Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Soft deletes an entity by marking it as deleted and saving the changes.
        /// </summary>
        /// <param name="entity">The entity to be soft deleted.</param>
        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            Update(entity);
        }
    }
}
