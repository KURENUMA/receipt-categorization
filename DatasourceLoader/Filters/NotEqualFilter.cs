﻿using Dma.DatasourceLoader.Helpers;
using System;
using System.Linq.Expressions;

namespace Dma.DatasourceLoader.Filters
{
    public class NotEqualFilter<T> : FilterBase<T>
    {
        private readonly object value;

        public NotEqualFilter(string propertyName, object value) : base(propertyName) 
        {
            this.value = value;
        }

        public override LambdaExpression GetFilterExpression()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression outerProperty = Expression.Property(parameter, "View");
            MemberExpression property = Expression.Property(outerProperty, propertyName);
            ConstantExpression constant = Expression.Constant(value);
            Expression propertyValue = Expressions.CastNonNullable(property);

            BinaryExpression notEqualExpression = Expression.NotEqual(propertyValue, constant);

            return Expression.Lambda<Func<T, bool>>(notEqualExpression, parameter);
        }
    }

}
