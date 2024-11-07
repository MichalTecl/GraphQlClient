using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes;
using MTecl.GraphQlClient.ObjectMapping.Visitors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MTecl.GraphQlClient.ObjectMapping
{
    internal static class QueryMapper
    {        
        private static readonly string[] _nulItemArray = new string[] { null };

        private static readonly DefaultMethodVisitor _defaultVisitor = new DefaultMethodVisitor();
        private static readonly ConcurrentDictionary<MethodInfo, IMethodVisitor> _visitorCache = new ConcurrentDictionary<MethodInfo, IMethodVisitor>();
       
        public static QueryNode MapQuery<TQuery, TResult>(Expression<Func<TQuery, TResult>> expression)
        {
            var node = new QueryNode();
            Visit(node, expression);            
            return node;
        }

        private static INode VisitMethod(INode parent, MethodCallExpression mce)
        {
            var visitor = ResolveVisitor(mce.Method);

            return visitor.Visit(parent, mce);
        }

        private static IMethodVisitor ResolveVisitor(MethodInfo m)
        {
            return _visitorCache.GetOrAdd(m, (method) => {
                var viattr = method.GetCustomAttribute<TreeVisitorAttribute>(true);
                if (viattr == null)
                    return _defaultVisitor;

                return (IMethodVisitor)Activator.CreateInstance(viattr.VisitorType);
            });
        }

        internal static INode Visit(INode parent, Expression e)
        {
            while (e is LambdaExpression lex)
                e = lex.Body;

            INode created = null;

            if (e is UnaryExpression unr && unr.NodeType == ExpressionType.Convert)
                created = Visit(parent, unr.Operand);

            if (e is MethodCallExpression mce)
                created = VisitMethod(parent, mce);

            if (e is MemberExpression pe)
                created = VisitMember(parent, pe);

            if (created == null)
                throw new NotSupportedException($"Unexpected type of expression {e.GetType().Name}: {e}");

            return created;
        }

        private static INode VisitMember(INode parent, MemberExpression pe)
        {
            if (pe.Expression is MemberExpression parentMex)
            {
                parent = VisitMember(parent, parentMex);
            }

            if (pe.Expression is MethodCallExpression mce)
            {
                parent = VisitMethod(parent, mce);
            }

            Type t;
            if (pe.Member is PropertyInfo pi)
                t = pi.PropertyType;
            else if (pe.Member is FieldInfo fi)
                t = fi.FieldType;
            else
                throw new NotSupportedException($"Unexpected member type {pe.Member.MemberType}");

            var memberNode = ConstructMemberNode(pe.Member, t);
            parent.Nodes.Add(memberNode);

            return memberNode;
        }

        internal static FieldNode ConstructMemberNode(MemberInfo member, Type memberType, bool topNodeOnly = false)
        {
            var memberInfo = GqlMemberHelper.MapMember(member);

            var memberNode = new FieldNode
            {
                Name = memberInfo.Attribute.Name,
                IsAliasFor = memberInfo.Attribute.IsAliasFor
            };

            if (topNodeOnly || !IsComplexType(memberType))
                return memberNode;

            var fields = GqlMemberHelper.MapMembers<PropertyInfo>(memberType);

            var fragmentNodes = new Dictionary<string, INode>();

            foreach (var field in fields)
            {
                if (field.Attribute.InclusionMode == FieldInclusionMode.Exclude)
                    continue;

                var complex = IsComplexType(((PropertyInfo)field.Member).PropertyType);

                if (complex && field.Attribute.InclusionMode == FieldInclusionMode.Default)
                    continue;
                                
                INode targetNode = memberNode;
                foreach (var type in field.GqlTypes.Length == 0 ? _nulItemArray : field.GqlTypes)
                {
                    if (type != null)
                    {
                        if (!fragmentNodes.TryGetValue(type, out targetNode))
                        {
                            targetNode = new InlineFragmentNode { OnType = type };
                            fragmentNodes.Add(type, targetNode);
                            memberNode.Nodes.Add(targetNode);
                        }
                    }

                    if (!complex)
                    {
                        targetNode.Nodes.Add(new FieldNode
                        {
                            Name = field.Attribute.Name,
                            IsAliasFor = field.Attribute.IsAliasFor
                        });
                    }
                    else
                    {
                        targetNode.Nodes.Add(ConstructMemberNode(field.Member, ((PropertyInfo)field.Member).PropertyType));
                    }
                }
            }

            return memberNode;
        }

        private static bool IsComplexType(Type t)
        {
            return !ClrGqlTypeMap.TypeMap.ContainsKey(t);
        }
    }
}
