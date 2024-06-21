using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dma.DatasourceLoader.Helpers
{
    public class Expressions
    {
        public static Expression CastNonNullable(MemberExpression property)
        {
            Expression propertyValue = property;
            if (property.Type.IsGenericType && property.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // Handle nullable int properties
                MethodInfo getValueOrDefaultMethod = property.Type.GetMethod("GetValueOrDefault", Type.EmptyTypes);

                propertyValue = Expression.Call(property, getValueOrDefaultMethod);
            }

            return propertyValue;
        }
    }
}
