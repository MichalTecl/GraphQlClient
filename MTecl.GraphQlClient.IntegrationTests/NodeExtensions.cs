using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.IntegrationTests
{
    internal static class NodeExtensions
    {
        public static FieldNode? FindChild(this INode root, Func<FieldNode, bool> predicate, bool recursive = true)
        {
            foreach(var child in root.Nodes.Filtered)
            {
                if (child is FieldNode fieldNode && predicate(fieldNode))
                    return fieldNode;

                if (recursive)
                {
                    var res = FindChild(child, predicate, true);
                    if (res != null)
                        return res;
                }
            }

            return null;
        }

        public static FieldNode? FindChild(this INode root, string name, bool recursive = true)
        {
            return root.FindChild(n => n.Name == name, recursive);
        }

        public static FieldNode? FindChildPath(this INode root, params string[] path)
        {
            FieldNode? n = null;

            foreach(var name in path)
            {
                n = FindChild(n ?? root, name, false);
                if (n == null)
                    return null;
            }

            return n;
        }
    }
}
