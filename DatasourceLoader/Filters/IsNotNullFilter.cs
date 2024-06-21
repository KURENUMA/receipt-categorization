using System;
using System.Linq.Expressions;

namespace Dma.DatasourceLoader.Filters
{
    public class IsNotNullFilter<T> : FilterBase<T>
    {

        public IsNotNullFilter(string propertyName) : base(propertyName)
        {
        }

        public override LambdaExpression GetFilterExpression()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression outerProperty = Expression.Property(parameter, "View");
            MemberExpression property = Expression.Property(outerProperty, propertyName);
            ConstantExpression nullValue = Expression.Constant(null, typeof(object));
            BinaryExpression isNotNullExpression = Expression.NotEqual(property, nullValue);

            return Expression.Lambda<Func<T, bool>>(isNotNullExpression, parameter);
        }
    }
}
