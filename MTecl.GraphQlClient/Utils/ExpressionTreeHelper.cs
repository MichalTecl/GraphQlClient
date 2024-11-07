using System;
using System.Linq.Expressions;

namespace MTecl.GraphQlClient.Utils
{
    internal static class ExpressionTreeHelper
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

        public static T TryGetArgument<T>(MethodCallExpression mce, string argumentName, Func<T> getDefaultValue)
        {
            var methodParams = mce.Method.GetParameters();

            for(var i = 0; i < methodParams.Length; i++)
            {
                if (methodParams[i].Name == argumentName && typeof(T).IsAssignableFrom(methodParams[i].ParameterType))
                {
                    return (T)EvaluateExpression(mce.Arguments[i]);
                }
            }

            return getDefaultValue();
        }
       

    }
}
