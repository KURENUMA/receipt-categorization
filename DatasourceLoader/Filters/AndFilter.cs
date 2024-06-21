using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dma.DatasourceLoader.Filters
{
    public class AndFilter<T> : FilterBaseBase
    {
        private readonly FilterBaseBase left;
        private readonly FilterBaseBase right;

        public AndFilter(FilterBaseBase left, FilterBaseBase right)
        {
            this.left = left;
            this.right = right;
        }

        public override LambdaExpression GetFilterExpression()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            var leftExpr = left.GetFilterExpression();
            var invokeExpression = Expression.Invoke(leftExpr, parameter);
            var rightExpr = right.GetFilterExpression();
            var rinvokeExpression = Expression.Invoke(rightExpr, parameter);
            // Combine the two expressions with AndAlso
            BinaryExpression andExpression = Expression.AndAlso(
                invokeExpression,
                rinvokeExpression
            );

            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }
}
