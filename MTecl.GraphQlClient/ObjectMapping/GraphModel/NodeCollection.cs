using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections;
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

        public IList<INode> All => _nodes;

        private INode Welcome(INode n)
        {
            n.Parent = _owner;
            return n;
        }

        public int Count => _nodes.Count;

        public void Add(INode item)
        {
            _nodes.Add(Welcome(item));
        }

        public void Clear()
        {
            _nodes.Clear();
        }

        public bool Contains(INode item)
        {
            return _nodes.Contains(item);
        }

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
    }
}
