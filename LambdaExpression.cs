using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace VYS.Application.Utilities
{
    public static class LambdaExpression
    {

        //public static Func<IQueryable<T>, IOrderedQueryable<T>> Lambda<T>(T source,
        //    string orderByProperty,bool desc)
        //{
        //    Func<IQueryable<T>, IOrderedQueryable<T>> func;

        //    if (source == null) return null;
        //    orderByProperty = orderByProperty == "İd" ? "Id" : orderByProperty;
        //    var type = typeof(T);
        //    var property = type.GetProperty(orderByProperty);
        //    var parameter = Expression.Parameter(type, "p");
        //    if (property == null) { return null; }
        //    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //    Expression<Func<T, TKey>> innerExpr = Expression.Lambda<Func<T, bool>>(ApplyFilter(opr, left, right), tpe);
        //    var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        //    return desc ? 
        //        x=>x.OrderByDescending(orderByExpression) :
        //        x=>x.OrderBy(orderByExpression);
        //}
    }
}
