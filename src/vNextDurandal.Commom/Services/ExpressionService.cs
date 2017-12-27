using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using vNextDurandal.Commom.Abstract;

namespace vNextDurandal.Commom.Services
{
    public static class ExpressionService
    {
        public static readonly MethodInfo ToStringMethod = typeof(object).GetMethod("ToString");
        private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod("Contains");

        public static Func<T, object> GroupKey<T>(string collumn)
        {
            var lambdaParam = Expression.Parameter(typeof(T));
            var propertiesToLambda = BuildPropertyExpression(collumn, lambdaParam);
            return Expression.Lambda<Func<T, object>>(propertiesToLambda, lambdaParam).Compile();
        }
        //TODO: montar expression pelas propriedades agregadas tambem. 
        private static Expression BuildPropertyExpression(string property, Expression lambdaParam)
            => Expression.Property(lambdaParam, property);

        public static Expression<Func<T, bool>> Filter<T>(string q)
        {

            var query = Expression.Constant(q);
            var type = typeof(T);
            var properties = type.GetProperties();

            var lambdaParam = Expression.Parameter(type);
            Expression body = null;
            var predicates = properties.SelectMany(p => Predicate(lambdaParam, p, query));


            foreach (var predicate in predicates)
            {
                if (body == null)
                    body = predicate;
                else
                    body = Expression.OrElse(body, predicate);
            }
            return Expression.Lambda<Func<T, bool>>(body, lambdaParam);
        }

        private static IEnumerable<MethodCallExpression> Predicate(Expression selector, PropertyInfo prop, Expression query)
        {

            if (prop.PropertyType.IsAssignableFrom(typeof(EntityBase)))
                return new List<MethodCallExpression> { Expression.Call(Expression.Call(Expression.Property(selector, prop), ToStringMethod), StringContainsMethod, query) };

            var properties = prop.PropertyType.GetProperties();
            return properties.Select(p => Expression.Call(Expression.Call(Expression.Property(selector, p), ToStringMethod), StringContainsMethod, query)).ToList();
        }
    }
}
