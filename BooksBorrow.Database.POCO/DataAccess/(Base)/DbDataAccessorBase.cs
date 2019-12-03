using BooksBorrow.Common.Constants;
using BooksBorrow.Database.Context;
using BooksBorrow.Domain.Core.DomainModels.Identities;
using LinqKit;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BooksBorrow.Database.POCO.DataAccess
{
    public abstract class DbDataAccessorBase
    {
        protected ILog LogHelper
        {
            get;
            private set;
        }
        protected DatabaseContext DbContext
        {
            get;
            private set;
        }
        public DbDataAccessorBase(string connectionString = null)
        {
            LogHelper = LogManager.GetLogger(ConfigKey.RepositoryName, typeof(DbDataAccessorBase));
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                this.DbContext = new DatabaseContext();
            }
            else
            {
                this.DbContext = new DatabaseContext(connectionString);
            }
        }
        public void Dispose()
        {
            this.DbContext.Dispose();
        }

        public void Save()
        {
            this.DbContext.SaveChanges();
        }

        protected IQueryable<T> GetQuery<T>() where T : class
        {
            return GetDbSet<T>().AsExpandable();
        }

        protected DbSet<T> GetDbSet<T>() where T : class
        {
            return DbContext.Set<T>();
        }

        protected static Guid GetPersistId(IdentityBase id)
        {
            return id.GetPersistId();
        }
        protected IQueryable<T> GetPageData<T>(string sortName, string sortOrder, int pageNumber, int pageSize, out int totalRecord, Expression<Func<T, bool>> predicate = null, bool AsNoTracking = true) where T : class
        {
            IQueryable<T> query = GetQuery<T>();

            if (AsNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where<T>(predicate);
            }

            totalRecord = query.Count();

            query = OrderBy(query, sortName, sortOrder == "asc")
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize);

            return query;
        }
        private static IQueryable<T> OrderBy<T>(IQueryable<T> source, string propertyName, bool isAsc) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Cannot be null.");
            }

            if (string.IsNullOrEmpty(propertyName)) return source;

            var parameter = Expression.Parameter(source.ElementType);
            var property = Expression.Property(parameter, propertyName);
            if (property == null)
            {
                throw new ArgumentNullException("propertyName", "The property does not exist.");
            }

            var lambdaExpression = Expression.Lambda(property, parameter);
            var methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new[] { source.ElementType, property.Type }, source.Expression, Expression.Quote(lambdaExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
