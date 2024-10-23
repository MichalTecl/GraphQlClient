using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MTecl.GraphQlClient.Utils
{
    public class ExpressionTreeHelper
    {
        public static MemberInfo GetCalledMethod<TInterface, TResult>(Expression<Func<TInterface, TResult>> expression, out Dictionary<string, object> arguments)
        {
            arguments = new Dictionary<string, object>();

            if (!(expression.Body is MethodCallExpression methodCall))
                throw new ArgumentException("Expression must be a method call");

            var method = methodCall.Method;
            var methodParameters = methodCall.Method.GetParameters();

            for(var i = 0 ; i < methodParameters.Length; i++)
            {
                var argument = methodCall.Arguments[i];
                var argumentValue = EvaluateExpression(argument);
                arguments.Add(methodParameters[i].Name, argumentValue);
            }

            return method;            
        }        

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
