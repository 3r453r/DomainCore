using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TestPersistence
{
    public class EntitySubstitutionVisitor<TOriginal, TSubstitute> : ExpressionVisitor
    {
        public Expression<Func<TSubstitute, bool>> Modify(Expression expression)
        {
            var result = Visit(expression) as Expression<Func<TSubstitute, bool>>;
            if (result == null)
                throw new ArgumentException($"Cannot convert given expression to Func<{typeof(TSubstitute).Name}, bool>");
            return result;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            var nodeType = node.GetType();
            if (nodeType.Equals(typeof(TOriginal)))
            {
                var substituteType = typeof(TSubstitute);
                if (substituteType.GetInterfaces().Contains(nodeType))
                {
                    return Expression.Parameter(substituteType, node.Name);
                }
                else
                    throw new InvalidCastException($"{substituteType.Name} doesn't implement {nodeType.Name}");
            }
            else
                return base.VisitParameter(node);
        }
    }
}
