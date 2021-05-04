using System;
using NUnit.Framework;

namespace MinimumSpanningTree.Tests
{
    using NonlinearDs;

    [TestFixture]
    public class GraphTests
    {
        private Graph<Int32, Double> _graph;

        [SetUp]
        public void Setup()
        {
            _graph = new Graph<Int32, Double>();
            _graph.Join("Sofia", 1_000_000);
            _graph.Join("Ruse", 90_000);
        }

        [Test]
        public void Join_NonExistentNode_JoinsTheNode()
        {
            _graph.Join("Varna", 90_000);

            Assert.That(_graph.Size, Is.EqualTo(3));
        }

        [Test]
        public void Join_ExistentNode_IgnoresTheNode()
        {
            _graph.Join("Ruse", 100_000);

            Assert.That(_graph.Size, Is.EqualTo(2));
        }

        [Test]
        public void Connect_SuchEdgeDoesNotExist_ConnectsTheNodes()
        {
            _graph.Connect("Sofia", "Ruse", 350.00);

            Assert.That(_graph["Sofia"].Edges.Count, Is.EqualTo(1));
        }

        [Test]
        public void Connect_SuchEdgeExists_IgnoresTheEdge()
        {
            _graph.Connect("Sofia", "Ruse", 350.00);

            _graph.Connect("Sofia", "Ruse", 350.00);

            Assert.That(_graph["Sofia"].Edges.Count, Is.EqualTo(1));
        }

        [Test]
        public void Connect_DestinationNodeDoesNotExist_CreatesTheDestinationNodeAndConnectsThem()
        {
            _graph.Connect("Sofia", "Varna", 70_000, 450.00);

            Assert.That(_graph.Size, Is.EqualTo(3));
            Assert.That(_graph["Sofia"].Edges.Count, Is.EqualTo(1));
        }

        [Test]
        public void ConnectTwoWay_EdgeBetweenTheNodesDoesNotExist_ConnectsTheNodesInATwoWayFashion()
        {
            _graph.ConnectTwoWay("Sofia", "Ruse", 350.00);

            Assert.That(_graph["Sofia"].Edges.Count, Is.EqualTo(1));
            Assert.That(_graph["Ruse"].Edges.Count, Is.EqualTo(1));
        }

        [Test]
        public void ConnectTwoWay_SuchNodesDoNotExist_CreatesTheNodesAndConnectsThemInATwoWayFashion()
        {
            _graph.ConnectTwoWay("Petrich", 28_000, "Blagoevgrad", 87_000, 79.00);

            Assert.That(_graph.Size, Is.EqualTo(4));
            Assert.That(_graph["Petrich"].Edges.Count, Is.EqualTo(1));
            Assert.That(_graph["Blagoevgrad"].Edges.Count, Is.EqualTo(1));
        }

        [Test]
        public void ConnectTwoWay_EdgeExistsOnlyInOneDirection_CreatesEdgeInTheMissingDirectionAsWell()
        {
            _graph.Connect("Sofia", "Ruse", 350.00);

            _graph.ConnectTwoWay("Sofia", "Ruse", 350.00);

            Assert.That(_graph["Sofia"].Edges.Count, Is.EqualTo(1));
            Assert.That(_graph["Ruse"].Edges.Count, Is.EqualTo(1));
        }
    }
}
