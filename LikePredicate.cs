using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VYS.Application.Utilities
{
    public static class LikePredicate
    {
        public static Expression<Func<T, bool>> strToFunc<T>(string propName, string opr, string value,
            Expression<Func<T, bool>> expr = null)
        {
            try
            {
                var likeMethod = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) });
                var entityProperty = typeof(T).GetProperty(propName, BindingFlags.Instance | BindingFlags.Public);

                // EF.Functions.Like(x.OrderNumber, v1) || EF.Functions.Like(x.OrderNumber, v2)...
                Expression likePredicate = null;

                var efFunctionsInstance = Expression.Constant(EF.Functions);

                // Will be the predicate paramter (the 'x' in x => EF.Functions.Like(x.OrderNumber, v1)...)

                var lambdaParam = Expression.Parameter(typeof(T));

                List<string> values = new List<string>();
                values.Add(value + "%");
                values.Add("%" + value);
                values.Add("%" + value + "%");

                foreach (var val in values)
                {
                    // EF.Functions.Like(x.OrderNumber, v1)
                    //                                 |__|
                    var numberValue = Expression.Constant(val);

                    //var numberValue = Expression.Constant(value);

                    // EF.Functions.Like(x.OrderNumber, v1)
                    //                  |_____________|
                    var propertyAccess = Expression.Property(lambdaParam, entityProperty);

                    // EF.Functions.Like(x.OrderNumber, v1)
                    //|____________________________________|
                    var likeMethodCall = Expression.Call(likeMethod, efFunctionsInstance, propertyAccess, numberValue);

                    // Aggregating the current predicate with "OR" (||)
                    likePredicate = likePredicate == null
                        ? (Expression)likeMethodCall
                        : Expression.OrElse(likePredicate, likeMethodCall);
                }

                // x => EF.Functions.Like(x.OrderNumber, v1) || EF.Functions.Like(x.OrderNumber, v2)...
                var lambdaPredicate = Expression.Lambda<Func<T, bool>>(likePredicate, lambdaParam);

                if (expr != null)
                    lambdaPredicate = lambdaPredicate.And(expr);

                return lambdaPredicate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
