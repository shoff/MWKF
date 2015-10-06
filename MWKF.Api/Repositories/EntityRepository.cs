

namespace MWKF.Api.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MWKF.Api.Exceptions;
    using MWKF.Api.Extensions;
    using MWKF.Api.Interfaces;
    using MWKF.Api.Repositories.Interfaces;

    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TK">The type of the k.</typeparam>
    public class EntityRepository<TEntity, TK> : IEntityRepository<TEntity, TK>
        where TEntity : class, new()
    {
        protected IDataContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{TEntity, TK}" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public EntityRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public IDataContext Context
        {
            get { return this.dataContext; }
            set { this.dataContext = value; }
        }


        /// <summary>
        /// Generic method to get a collection of Entities
        /// </summary>
        /// <param name="filter">Filter expression for the return Entities</param>
        /// <param name="orderBy">Represents the order of the return Entities</param>
        /// <param name="includeProperties">Include Properties for the navigation properties</param>
        /// <returns>A Enumerable of Entities</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dataContext.SetEntity<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return orderBy?.Invoke(query).ToList() ?? query.ToList();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dataContext.SetEntity<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        /// <summary>
        /// Generic Method to get an Entity by Identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        /// <returns>
        /// The Entity
        /// </returns>
        public virtual TEntity GetById(TK id)
        {
            CheckParameter(id);
            return this.dataContext.SetEntity<TEntity>().Find(id);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(TK id, string includeProperties = null)
        {
            // TODO the includeProperties do not work right now
            return await ((DbContext)this.dataContext).Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = ((DbContext)this.dataContext).Set<TEntity>();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.SingleOrDefaultAsync(filter);
        }

        /// <summary>
        /// Generic method for add an Entity to the context
        /// </summary>
        /// <param name="entity">The Entity to Add</param>
        public virtual TEntity Insert(TEntity entity)
        {
            return this.dataContext.SetEntity<TEntity>().Add(entity);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            this.dataContext.SetEntity<TEntity>().Add(entity);
            await this.dataContext.CommitAsync();
            return entity;
        }

        /// <summary>
        /// Generic method for deleting a method in the context by identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        public virtual void Delete(TK id)
        {
            CheckParameter(id);
            TEntity entityToDelete = this.dataContext.SetEntity<TEntity>().Find(id);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Generic method for deleting a method in the context pasing the Entity
        /// </summary>
        /// <param name="entityToDelete">Entity to Delete</param>
        /// <exception cref="ArgumentNullException">The value of 'entityToDelete' cannot be null. </exception>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null)
            {
                return;
            }
            this.dataContext.Attach(entityToDelete);
            this.dataContext.SetEntity<TEntity>().Remove(entityToDelete);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException">An error occurred sending updates to the database.</exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///             A database command did not affect the expected number of rows. This usually indicates an optimistic 
        ///             concurrency violation; that is, a row has been changed in the database since it was queried.
        ///             </exception>
        /// <exception cref="DbEntityValidationException">
        ///             The save was aborted because validation of entity property values failed.
        ///             </exception>
        public virtual async Task DeleteAsync(TEntity entityToDelete)
        {
            ((DbContext)this.dataContext).Set<TEntity>().Remove(entityToDelete);
            await ((DbContext)this.dataContext).SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified updated.
        /// </summary>
        /// <param name="updated">The updated.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException">An error occurred sending updates to the database.</exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///             A database command did not affect the expected number of rows. This usually indicates an optimistic 
        ///             concurrency violation; that is, a row has been changed in the database since it was queried.
        ///             </exception>
        /// <exception cref="DbEntityValidationException">
        ///             The save was aborted because validation of entity property values failed.
        ///             </exception>
        public TEntity Update(TEntity updated, TK key)
        {
            if (updated == null)
            {
                return null;
            }

            TEntity existing = ((DbContext)this.dataContext).Set<TEntity>().Find(key);

            if (existing != null)
            {
                ((DbContext)this.dataContext).Entry(existing).CurrentValues.SetValues(updated);
                ((DbContext)this.dataContext).SaveChanges();
            }
            return existing;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="updated">The updated.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DbUpdateConcurrencyException">
        ///             A database command did not affect the expected number of rows. This usually indicates an optimistic 
        ///             concurrency violation; that is, a row has been changed in the database since it was queried.
        ///             </exception>
        /// <exception cref="DbEntityValidationException">
        ///             The save was aborted because validation of entity property values failed.
        ///             </exception>
        /// <exception cref="DbUpdateException">An error occurred sending updates to the database.</exception>
        public async Task<TEntity> UpdateAsync(TEntity updated, TK key)
        {
            if (updated == null)
            {
                return null;
            }

            TEntity existing = await ((DbContext)this.dataContext).Set<TEntity>().FindAsync(key);

            if (existing != null)
            {
                ((DbContext)this.dataContext).Entry(existing).CurrentValues.SetValues(updated);
                await ((DbContext)this.dataContext).SaveChangesAsync();
            }
            return existing;
        }

        /// <summary>
        /// Generic implementation for get Paged Entities
        /// </summary>
        /// <typeparam name="TKey">Key for order Expression</typeparam>
        /// <param name="pageIndex">Index of the Page</param>
        /// <param name="pageCount">Number of Entities to get</param>
        /// <param name="orderByExpression">Order expression</param>
        /// <param name="orderby">if set to <c>true</c> [orderby].</param>
        /// <returns>
        /// Enumerable of Entities matching the conditions
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public IEnumerable<TEntity> GetPagedElements<TKey>(
            int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool orderby = true)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                return (orderby)
                    ? this.dataContext.SetEntity<TEntity>().OrderBy(orderByExpression).Skip((pageIndex - 1) * pageCount).Take(pageCount)
                        .ToList()
                    : this.dataContext.SetEntity<TEntity>().OrderByDescending(orderByExpression).Skip((pageIndex - 1) * pageCount).Take(
                        pageCount).ToList();
            }
            catch (DbEntityValidationException dbe)
            {
                // if you override ToString on your story, if there is a validation error, 
                // this will log the whatever you put in ToString so you can identify 
                // the faulting story.

                if (!ReferenceEquals(null, dbe.EntityValidationErrors))
                {
                    dbe.EntityValidationErrors.Each(x =>
                    {
                        //x.ValidationErrors.Each(y => this.logService.WriteToErrorLog(y.ErrorMessage));
                    });
                }
                throw;
            }
            catch (InvalidCastException)
            {
                // we never return null from our repository, just
                // an empty collection.
                return new List<TEntity>();
            }
        }

        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="sqlQuery">The Query to be executed</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>
        /// List of Entity
        /// </returns>
        /// <exception cref="ArgumentNullException">The value of 'sqlQuery' cannot be null. </exception>
        public IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters)
        {
            if (string.IsNullOrEmpty(sqlQuery))
            {
                throw new ParameterNullException(nameof(sqlQuery));
            }
            if (parameters == null)
            {
                throw new ParameterNullException(nameof(parameters));
            }
            return this.dataContext.ExecuteQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute a command in database
        /// </summary>
        /// <param name="sqlCommand">The sql query</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>integer representing the sql code</returns>
        /// <exception cref="ArgumentNullException">The value of 'parameters' cannot be null. </exception>
        public int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters)
        {
            if (parameters == null)
            {
                throw new ParameterNullException(nameof(parameters));
            }
            if (string.IsNullOrEmpty(sqlCommand))
            {
                throw new ParameterNullException(nameof(sqlCommand));
            }
            return this.dataContext.ExecuteCommand(sqlCommand, parameters);
        }

        /// <summary>
        /// Get count of Entities
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return this.dataContext.SetEntity<TEntity>().Count();
        }

        private static void CheckParameter(TK id)
        {
            if (!typeof(TK).IsValueType)
            {
                if (ReferenceEquals(null, id))
                {
                    throw new ParameterNullException(nameof(id));
                }
            }
        }
    }
}