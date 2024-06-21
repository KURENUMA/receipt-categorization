﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Dma.DatasourceLoader.Filters
{
    public class StartsWithFilter<T> : FilterBase<T>
    {
        private readonly string value;

        public StartsWithFilter(string propertyName, string value) : base(propertyName)
        {
            this.value = value;
        }

        public override LambdaExpression GetFilterExpression()
        {   
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression outerProperty = Expression.Property(parameter, "View");
            MemberExpression property = Expression.Property(outerProperty, propertyName);
            MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(value);
            BinaryExpression nullCheck = Expression.NotEqual(property, Expression.Constant(null));

            MethodCallExpression containsExpression = Expression.Call(property, startsWithMethod, constant);

            BinaryExpression combinedExpression = Expression.AndAlso(nullCheck, containsExpression);

            return Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        }
    }
}
