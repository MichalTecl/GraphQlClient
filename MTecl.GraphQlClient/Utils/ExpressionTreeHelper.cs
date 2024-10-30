using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace MTecl.GraphQlClient.Utils
{
    public static class ExpressionTreeHelper
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

        public static bool TryGetMethodArguments<T1>(this MethodCallExpression mce, out T1 arg1)
        {
            if (TryEvalArguments(mce, out var args, typeof(T1)))
            {
                arg1 = (T1)args[0];
                return true;
            }

            arg1 = default;
            return false;
        }

        public static bool TryGetMethodArguments<T1, T2>(this MethodCallExpression mce, out T1 arg1, out T2 arg2)
        {
            if (TryEvalArguments(mce, out var args, typeof(T1), typeof(T2)))
            {
                arg1 = (T1)args[0];
                arg2 = (T2)args[1];
                return true;
            }

            arg1 = default;
            arg2 = default;
            return false;
        }

        public static bool TryGetMethodArguments<T1, T2, T3>(this MethodCallExpression mce, out T1 arg1, out T2 arg2, out T3 arg3)
        {
            if (TryEvalArguments(mce, out var args, typeof(T1), typeof(T2), typeof(T3)))
            {
                arg1 = (T1)args[0];
                arg2 = (T2)args[1];
                arg3 = (T3)args[2];
                return true;
            }

            arg1 = default;
            arg2 = default;
            arg3 = default;
            return false;
        }

        private static bool TryEvalArguments(MethodCallExpression mce, out object[] args, params Type[] t)
        {
            args = new object[t.Length];

            if (t.Length != mce.Arguments.Count)
                return false;

            for(var i = 0; i < t.Length; i++)
            {
                var evaluated = EvaluateExpression(mce.Arguments[i]);
                if (evaluated == null)
                {
                    if (t[i].IsValueType)
                        return false;
                    args[i] = null;
                    continue;
                }

                if (!t[i].IsAssignableFrom(evaluated.GetType()))
                    return false;

                args[i] = evaluated;
            }

            return true;
        }

    }
}
