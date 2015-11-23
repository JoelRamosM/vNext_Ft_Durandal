using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace vNextDurandal.Commom.Services
{
    public static class ExpressionService
    {
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
            var toString = typeof(object).GetMethod("ToString");
            var strContains = typeof(string).GetMethod("Contains");
            var query = Expression.Constant(q);
            var type = typeof(T);
            var properties = type.GetProperties();

            var lambdaParam = Expression.Parameter(type);
            Expression body = null;
            var predicates = properties
                .Select(p => Expression.Call(Expression.Call(Expression.Property(lambdaParam, p), toString), strContains, query)).ToList();
            foreach (var predicate in predicates)
            {
                if (body == null)
                    body = predicate;
                else
                    body = Expression.OrElse(body, predicate);
            }
            return Expression.Lambda<Func<T, bool>>(body, lambdaParam);
        }
    }
}
