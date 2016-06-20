using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Chat.Domain.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            ThrowIfNull(context);

            _context = context;
            _dbSet = context.Set<TEntity>();

#if DEBUG
            _context.Database.Log = message => System.Diagnostics.Debug.Write(message);
#endif
        }

        protected virtual IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null)
        {
            return Query(_dbSet, filter);
        }

        private IQueryable<TEntity> Query(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        protected void ThrowIfNull<TArgument>(TArgument argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(typeof(TArgument).Name);
            }
        }

        protected virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }
    }
}
