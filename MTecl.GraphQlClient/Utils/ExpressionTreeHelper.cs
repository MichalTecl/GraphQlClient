using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MTecl.GraphQlClient.Utils
{
    public class ExpressionTreeHelper
    {
        public static object EvaluateExpression(Expression e)
        {
            while (e is LambdaExpression lambda)
                e = lambda.Body;

            if (e is ConstantExpression ce)
                return ce.Value;

            var l = Expression.Lambda(e);
            var compiledLambda = l.Compile();

            return compiledLambda.DynamicInvoke();
        }        
    }
}
