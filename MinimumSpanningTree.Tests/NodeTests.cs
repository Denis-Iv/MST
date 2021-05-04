using System;
using NUnit.Framework;

namespace MinimumSpanningTree.Tests
{
    using NonlinearDs;

    [TestFixture]
    public class NodeTests
    {
        private Node<Int32, Double> _node;

        [SetUp]
        public void SetUp()
        {
            _node = new Node<Int32, Double>(89_000);
        }

        [Test]
        public void AddEdge_NonExistentEdge_AddsTheEdge()
        {
            var edge = new Node<Int32, Double>.Edge(110.00, new Node<Int32, Double>(1_500_000));

            _node.AddEdge(edge);

            Assert.That(_node.Edges.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddEdge_ExistentEdge_IgnoresTheEdge()
        {
            var edge = new Node<Int32, Double>.Edge(110.00, new Node<Int32, Double>(1_500_000));
            _node.AddEdge(edge);

            _node.AddEdge(edge);

            Assert.That(_node.Edges.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveEdge_NonExistentEdge_IgnoresTheEdge()
        {
            var edge = new Node<Int32, Double>.Edge(110.00, new Node<Int32, Double>(1_500_000));

            _node.RemoveEdge(edge);

            Assert.That(_node.Edges.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveEdge_ExistentEdge_RemovesTheEdge()
        {
            var edge = new Node<Int32, Double>.Edge(110.00, new Node<Int32, Double>(1_500_000));
            _node.AddEdge(edge);

            _node.RemoveEdge(edge);

            Assert.That(_node.Edges.Count, Is.EqualTo(0));
        }

        [Test]
        public void EqualsOfIEquatable_NodeIsNull_ReturnsFalse()
        {
            Node<Int32, Double> other = null;

            Boolean result = _node.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsOfIEquatable_NodeIsDifferent_ReturnsFalse()
        {
            Node<Int32, Double> other = new Node<int, double>(50_000);

            Boolean result = _node.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsOfIEquatable_NodesAreEqual_ReturnsTrue()
        {
            Node<Int32, Double> other = new Node<Int32, Double>(_node.Value);

            Boolean result = _node.Equals(other);

            Assert.IsTrue(result);
        }

        [Test]
        public void EqualsOfObject_ArgumentIsNull_ReturnsFalse()
        {
            Object other = null;

            Boolean result = _node.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsOfObject_ArgumentIsTheSameInstance_ReturnsTrue()
        {
            Object other = _node;

            Boolean result = _node.Equals(other);

            Assert.IsTrue(result);
        }

        [Test]
        public void EqualsOfObject_ArgumentIsOfDifferentType_ReturnsFalse()
        {
            Object other = 5;

            Boolean result = _node.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsOfObject_ArgumentIsOfTheSameType_PerformsEquality()
        {
            Object other = new Node<Int32, Double>(100_000);

            Boolean result = _node.Equals(other);

            Assert.IsFalse(result);
        }
    }
}