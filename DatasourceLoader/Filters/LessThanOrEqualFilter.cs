﻿using Dma.DatasourceLoader.Helpers;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Dma.DatasourceLoader.Filters
{
    public class LessThanOrEqualFilter<T> : FilterBase<T>
    {
        private readonly object value;

        public LessThanOrEqualFilter(string propertyName, object value) : base(propertyName) 
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


            BinaryExpression lessThanExpression = Expression.LessThanOrEqual(propertyValue, constant);

            return Expression.Lambda<Func<T, bool>>(lessThanExpression, parameter);
        }
    }

}
