using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel
{
    public class NodeCollection 
    {
        private readonly INode _owner;
        private readonly List<INode> _nodes = new List<INode>();
        public NodeCollection(INode owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// Returned collection contains all nodes except properyl replaced placeholders (i.e. INode.IsImportant == false)
        /// </summary>
        public IEnumerable<INode> Filtered 
        {
            get
            {
                foreach(var group in _nodes.GroupBy(n => n.NodeId))
                {
                    var importantNodeFound = false;
                    foreach(var node in group.OrderBy(n => n.IsImportant ? 0 : 1))
                    {
                        if (importantNodeFound && (!node.IsImportant))
                            break;

                        importantNodeFound = node.IsImportant;
                        yield return node;
                    }
                }
            }
        }

        /// <summary>
        /// All nodes including replaced placeholders
        /// </summary>
        public IList<INode> All => _nodes;
               
        /// <summary>
        /// Count of ALL nodes (including replaced placeholders)
        /// </summary>
        public int Count => _nodes.Count;

        /// <summary>
        /// Adds a node into the collection. Please note that adding also sets parent node reference on provided node
        /// </summary>
        /// <param name="item"></param>
        public void Add(INode item)
        {
            _nodes.Add(Welcome(item));
        }

        /// <summary>
        /// Removes all nodes 
        /// </summary>
        public void Clear()
        {
            _nodes.Clear();
        }

        public bool Contains(INode item)
        {
            return _nodes.Contains(item);
        }

        /// <summary>
        /// Renders all FILTERED (without replaced placeholders) nodes into provided StringBuilder
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="builder"></param>
        public void Render(StringBuilder stringBuilder, GraphQlQueryBuilder builder)
        {
            foreach (var n in _nodes)
                n.Render(stringBuilder, builder);
        }

        public IEnumerable<INode> Remove(Func<INode, bool> predicate)
        {
            var removed = new List<INode>();

            for (int i = _nodes.Count - 1; i >= 0; i--)
            {
                if (predicate(_nodes[i]))
                {
                    removed.Add(_nodes[i]);
                    _nodes.RemoveAt(i);
                }
            }

            return removed;
        }

        private INode Welcome(INode n)
        {
            n.Parent = _owner;
            return n;
        }
    }
}
