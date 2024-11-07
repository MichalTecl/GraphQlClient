using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel
{
    /// <summary>
    /// Implementing object represents a node in GraphQL query graph
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Unique identifier in the query
        /// </summary>
        string NodeId { get; }

        /// <summary>
        /// Depth - for rendering purposes
        /// </summary>
        int Level { get; }

        /// <summary>
        /// Reference to parent node, maintained by NodeCollection
        /// </summary>
        INode Parent { get; set; }

        /// <summary>
        /// Child nodes
        /// </summary>
        NodeCollection Nodes { get; }

        /// <summary>
        /// True means this node can replace another node with the same NodeId on the same level. Sometimes it's unevitable that during 
        /// query assembly process we have some nodes just as place holders
        /// </summary>
        bool IsImportant { get; }

        /// <summary>
        /// Renders itself to the provided StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="builder"></param>
        void Render(StringBuilder sb, GraphQlQueryBuilder builder);        
    }
}
