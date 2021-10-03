using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection.Data {
    public class Repository<T> where T : class {
        protected readonly DBContext _context;
        internal DbSet<T> _dbSet;

        public Repository(DBContext context) {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        ///     <para>
        ///         Begins tracking the given entity, and any other reachable entities that are
        ///         not already being tracked, in the <see cref="EntityState.Added" /> state such that they will
        ///         be inserted into the database when <see cref="DbContext.SaveChanges()" /> is called.
        ///     </para>
        ///     <para>
        ///         This method is <see langword="async"/> only to allow special value generators, such as the one used by
        ///         'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo',
        ///         to access the database asynchronously. For all other cases the non <see langword="async"/> method should be used.
        ///     </para>
        ///     <para>
        ///         Use <see cref="EntityEntry.State" /> to set the state of only a single entity.
        ///     </para>
        /// </summary>
        /// <param name="entity"> The entity to add. </param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous Add operation. The task result contains the
        ///     <see cref="EntityEntry{TEntity}" /> for the entity. The entry provides access to change tracking
        ///     information and operations for the entity.
        /// </returns>
        public async Task AddAsync(T entity) {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        ///     <para>
        ///         Begins tracking the given entities, and any other reachable entities that are
        ///         not already being tracked, in the <see cref="EntityState.Added" /> state such that they will
        ///         be inserted into the database when <see cref="DbContext.SaveChanges()" /> is called.
        ///     </para>
        /// </summary>
        /// <param name="entities"> The entities to add. </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public async Task AddRangeAsync(List<T> entities) {
            for (int i = 0; i < entities.Count; i++) await AddAsync(entities[i]);
        }

        /// <summary>
        ///     Gets an entity with the given primary key values. If an entity with the given primary key values
        ///     is being tracked by the context, then it is returned immediately without making a request to the
        ///     database. Otherwise, a query is made to the database for an entity with the given primary key values
        ///     and this entity, if found, is attached to the context and returned. If no entity is found, then
        ///     null is returned.
        /// </summary>
        /// <param name="id">The values of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        public async Task<T> GetAsync(int id) {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        ///     Gets entities using given lambda expression.
        ///     If no entities is found, then empty list is returned.
        /// </summary>
        /// <param name="filter">A lambda expression function.</param>
        /// <param name="orderby">Orderby IQueryable.</param>
        /// <param name="includeProperties">IncludeProperties works like .Include how ever it just needs either a table name or a comma separated string like this ("itemOne, itemTwo")</param>
        /// <returns>The entities it found, or empty list.</returns>
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null) {
            IQueryable<T> query = _dbSet;
            if (filter != null) query = query.Where(filter);
            if (includeProperties != null) {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProperty);
                }
            }
            if (orderby != null) return await orderby(query).ToListAsync();
            return await query.ToListAsync();
        }

        /// <summary>
        ///     Asynchronously returns the first element of a sequence that satisfies a specified condition
        ///     or a default value if no such element is found.
        /// </summary>
        /// <param name="filter">A lambda expression function.</param>
        /// <param name="includeProperties">IncludeProperties works like .Include how ever it just needs either a table name or a comma separated string like this ("itemOne, itemTwo")</param>
        /// <remarks>
        ///     <para>
        ///         Multiple active operations on the same context instance are not supported. Use <see langword="await" /> to ensure
        ///         that any asynchronous operations have completed before calling another method on this context.
        ///         See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-async-linq">Querying data with EF Core</see> for more information.
        ///     </para>
        /// </remarks>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null) {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            if (includeProperties != null) {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Begins tracking the given entities in the <see cref="EntityState.Deleted" /> state such that they will
        ///     be removed from the database when <see cref="DbContext.SaveChanges()" /> is called.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If any of the entities are already tracked in the <see cref="EntityState.Added" /> state then the context will
        ///         stop tracking those entities (rather than marking them as <see cref="EntityState.Deleted" />) since those
        ///         entities were previously added to the context and do not exist in the database.
        ///     </para>
        ///     <para>
        ///         Any other reachable entities that are not already being tracked will be tracked in the same way that
        ///         they would be if <see cref="AttachRange(TEntity[])" /> was called before calling this method.
        ///         This allows any cascading actions to be applied when <see cref="DbContext.SaveChanges()" /> is called.
        ///     </para>
        /// </remarks>
        /// <param name="entities"> The entities to remove. </param>
        public void RemoveRange(List<T> entities) {
            for (int i = 0; i < entities.Count; i++) Remove(entities[i]);
        }


        /// <summary>
        ///     Gets an entity with the given primary key values. If an entity with the given primary key values
        ///     is being tracked by the context, then it is returned immediately without making a request to the
        ///     database. Otherwise, a query is made to the database for an entity with the given primary key values
        ///     and this entity. After that it will set to removed.
        /// </summary>
        /// <param name="id">The values of the primary key for the entity to be found.</param>
        public async void RemoveAsync(int id) {
            Remove(await GetAsync(id));
        }

        /// <summary>
        ///     Begins tracking the given entity in the <see cref="EntityState.Deleted" /> state such that it will
        ///     be removed from the database when <see cref="DbContext.SaveChanges()" /> is called.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If the entity is already tracked in the <see cref="EntityState.Added" /> state then the context will
        ///         stop tracking the entity (rather than marking it as <see cref="EntityState.Deleted" />) since the
        ///         entity was previously added to the context and does not exist in the database.
        ///     </para>
        ///     <para>
        ///         Any other reachable entities that are not already being tracked will be tracked in the same way that
        ///         they would be if <see cref="Attach(TEntity)" /> was called before calling this method.
        ///         This allows any cascading actions to be applied when <see cref="DbContext.SaveChanges()" /> is called.
        ///     </para>
        ///     <para>
        ///         Use <see cref="EntityEntry.State" /> to set the state of only a single entity.
        ///     </para>
        /// </remarks>
        /// <param name="entity"> The entity to remove. </param>
        /// <returns>
        ///     The <see cref="EntityEntry{TEntity}" /> for the entity. The entry provides
        ///     access to change tracking information and operations for the entity.
        /// </returns>
        public void Remove(T entity) {
            _dbSet.Remove(entity);
        }

        /// <summary>Attaches the entities and changes their CurrentValues to the entities values.</summary>
        /// <param name="entities">The entities to update.</param>
        public void UpdateRange(List<T> entities) {
            foreach (T entity in entities) {
                Update(entity);
            }
        }

        /// <summary>Attaches the entity and SetValues to the entity's value.</summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(T entity) {
            _dbSet.Attach(entity).CurrentValues.SetValues(entity);
        }
    }
}
