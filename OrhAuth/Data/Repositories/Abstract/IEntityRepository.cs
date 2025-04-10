using OrhAuth.Models.Entities.Base;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace OrhAuth.Data.Repositories.Abstract
{
    /// <summary>
    /// Represents a generic repository interface for performing basic CRUD operations on entities.
    /// </summary>
    /// <typeparam name="T">The entity type, which must inherit from EntityBase.</typeparam>
    public interface IEntityRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Retrieves a single entity that matches the specified filter expression.
        /// </summary>
        /// <param name="filter">A LINQ expression used to filter the entity.</param>
        /// <returns>The matching entity or null if not found.</returns>
        T Get(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Retrieves a list of entities that match the specified filter expression.
        /// If no filter is provided, all entities are returned.
        /// </summary>
        /// <param name="filter">An optional LINQ expression used to filter the entities.</param>
        /// <returns>A list of matching entities.</returns>
        IList<T> GetList(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Adds a new entity to the data store.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an existing entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);
    }
}
