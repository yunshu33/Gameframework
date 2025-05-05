using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace LJVoyage.Game
{
    public static class LambadUtilities
    {
        /// <summary>
        /// 解析 lambad 表达式 成员名称
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string ParseMemberName(LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var method = expression.Body as MethodCallExpression;
            if (method != null)
                return method.Method.Name;

            //Delegate.CreateDelegate(Type type, object firstArgument, MethodInfo method)
            var unary = expression.Body as UnaryExpression;
            if (unary != null && unary.NodeType == ExpressionType.Convert)
            {
                MethodCallExpression methodCall = (MethodCallExpression)unary.Operand;
                if (methodCall.Method.Name.Equals("CreateDelegate"))
                {
                    var info = GetDelegateMethodInfo(methodCall);
                    if (info != null)
                        return info.Name;
                }

                throw new ArgumentException(string.Format("Invalid expression:{0}", expression));
            }

            var body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException(string.Format("Invalid expression:{0}", expression));

            if (!(body.Expression is ParameterExpression))
                throw new ArgumentException(string.Format("Invalid expression:{0}", expression));

            return body.Member.Name;
        }

        private static MethodInfo GetDelegateMethodInfo(MethodCallExpression expression)
        {
            var target = expression.Object;
            var arguments = expression.Arguments;
            if (target == null)
            {
                foreach (var expr in arguments)
                {
                    if (!(expr is ConstantExpression))
                        continue;

                    var value = (expr as ConstantExpression).Value;
                    if (value is MethodInfo)
                        return (MethodInfo)value;
                }

                return null;
            }
            else if (target is ConstantExpression)
            {
                var value = (target as ConstantExpression).Value;
                if (value is MethodInfo)
                    return (MethodInfo)value;
            }

            return null;
        }
    }
}