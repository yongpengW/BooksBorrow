using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BooksBorrow.Database.POCO.Extensions
{
    internal static class IQueryableExtension
    {
        public static IEnumerable<T> GetPagingRecords<T>(this IQueryable<T> query, int pageNum, int pageSize, bool no_pagination = false) where T : class
        {
            if (no_pagination)
            {
                return query.ToList().Cast<T>();
            }
            return query.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList().Cast<T>();
        }

        public static IEnumerable<T> GetPagingRecords<T>(this IQueryable<T> query, string sortName, string sortOrder, int pageNum, int pageSize) where T : class
        {
            return OrderBy(query, sortName, sortOrder == "asc").Skip((pageNum - 1) * pageSize).Take(pageSize).ToList().Cast<T>();
        }

        public static IEnumerable<T> GetAllRecords<T>(this IQueryable<T> query, string sortName, string sortOrder) where T : class
        {
            return OrderBy(query, sortName, sortOrder == "asc").ToList().Cast<T>();
        }

        private static IQueryable<T> OrderBy<T>(IQueryable<T> source, string propertyName, bool isAsc) where T : class
        {

            if (source == null)
            {
                throw new ArgumentNullException("source", "Cannot be null.");
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            var parameter = Expression.Parameter(source.ElementType);
            var property = Expression.Property(parameter, propertyName);
            if (property == null) throw new ArgumentNullException("propertyName", "The property does not exist.");

            var lambdaExpression = Expression.Lambda(property, parameter);
            var methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new[] { source.ElementType, property.Type }, source.Expression, Expression.Quote(lambdaExpression));

            return source.Provider.CreateQuery<T>(resultExpression);

        }
    }
}
