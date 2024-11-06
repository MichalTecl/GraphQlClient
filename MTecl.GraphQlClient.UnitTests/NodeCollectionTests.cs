using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace MTecl.GraphQlClient.UnitTests
{    
    public class NodeCollectionTests
    {
        private class MockNode : INode
        {
            public MockNode()
            {
                 Nodes = new NodeCollection(this);
            }

            public bool IsImportant { get; set; }
            public INode Parent { get; set; }

            public int Level => (Parent?.Level ?? 0) + 1;

            public NodeCollection Nodes { get; }

            public string NodeId { get; set; }
            public Expression Expression { get; set; }

            public void Render(StringBuilder stringBuilder, GraphQlQueryBuilder o) => stringBuilder.Append($"Node{NodeId}");
        }

        [Fact]
        public void Add_ShouldIncreaseCount()
        {
            // Arrange        
            var collection = new NodeCollection(null);

            // Act
            collection.Add(new MockNode());

            // Assert
            Assert.Equal(1, collection.Count);
        }

        [Fact]
        public void Clear_ShouldEmptyCollection()
        {
            // Arrange            
            var collection = new NodeCollection(null);
            collection.Add(new MockNode());
            collection.Add(new MockNode());

            // Act
            collection.Clear();

            // Assert
            Assert.Empty(collection.All);
        }

        [Fact]
        public void Contains_ShouldReturnTrueForExistingNode()
        {
            // Arrange
            var node = new MockNode();
            var collection = new NodeCollection(null);
            collection.Add(node);

            // Act
            var contains = collection.Contains(node);

            // Assert
            Assert.True(contains);
        }

        [Fact]
        public void Filtered_ShouldReturnImportantNodesFirst()
        {
            // Arrange            
            var collection = new NodeCollection(null);
            collection.Add(new MockNode { NodeId = "1", IsImportant = false });
            collection.Add(new MockNode { NodeId = "1", IsImportant = true });
            collection.Add(new MockNode { NodeId = "2", IsImportant = false });

            // Act
            var filteredNodes = collection.Filtered.ToList();

            // Assert
            Assert.Equal(2, filteredNodes.Count);
            Assert.True(filteredNodes[0].IsImportant);
            Assert.False(filteredNodes[1].IsImportant);
        }

        [Fact]
        public void Remove_ShouldRemoveNodesMatchingPredicate()
        {
            // Arrange            
            var collection = new NodeCollection(null);
            var node1 = new MockNode { NodeId = "1" };
            var node2 = new MockNode { NodeId = "2" };
            collection.Add(node1);
            collection.Add(node2);

            // Act
            var removedNodes = collection.Remove(n => ((MockNode)n).NodeId == "1").ToList();

            // Assert
            Assert.Single(removedNodes);
            Assert.Equal(node1, removedNodes.First());
            Assert.False(collection.Contains(node1));
            Assert.True(collection.Contains(node2));
        }

        [Fact]
        public void NodeInCollection_ShouldHaveSetParent()
        {
            // Arrange
            var owner = new MockNode();
            var collection = new NodeCollection(owner);

            var n = new MockNode();

            // Act
            collection.Add(n);

            // Assert
            Assert.Equal(n.Parent, owner);
        }
    }

}
