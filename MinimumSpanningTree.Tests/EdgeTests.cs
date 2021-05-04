using System;
using NUnit.Framework;

namespace MinimumSpanningTree.Tests
{
    using NonlinearDs;

    [TestFixture]
    public class EdgeTests
    {
        private Node<Int32, Double> _node;
        private Node<Int32, Double>.Edge _edge;

        [SetUp]
        public void SetUp()
        {
            _node = new Node<Int32, Double>(100_000);
            _edge = new Node<Int32, Double>.Edge(65.80, _node);
        }

        [Test]
        public void EqualsOfIEquatable_EdgeIsNull_ReturnsFalse()
        {
            Node<Int32, Double>.Edge other = null;

            Boolean result = _edge.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsOfIEquatable_EdgeIsDifferent_ReturnsFalse()
        {
            Node<Int32, Double>.Edge other = new Node<Int32, Double>.Edge(44.30, _node);

            Boolean result = _edge.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsOfIEquatable_EdgesAreEqual_ReturnsTrue()
        {
            Node<Int32, Double>.Edge other = new Node<Int32, Double>.Edge(_edge.WeightedFactor, _edge.Destination);

            Boolean result = _edge.Equals(other);

            Assert.IsTrue(result);
        }

        [Test]
        public void EqualsOfObject_ArgumentIsNull_ReturnsFalse()
        {
            Object other = null;

            Boolean result = _edge.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsOfObject_ArgumentIsTheSameInstance_ReturnsTrue()
        {
            Object other = _edge;

            Boolean result = _edge.Equals(other);

            Assert.IsTrue(result);
        }

        [Test]
        public void EqualsOfObject_ArgumentIsOfDifferentType_ReturnsFalse()
        {
            Object other = 5;

            Boolean result = _edge.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsOfObject_ArgumentIsOfTheSameType_PerformsEquality()
        {
            Object other = new Node<Int32, Double>.Edge(200_000, _node);

            Boolean result = _edge.Equals(other);

            Assert.IsFalse(result);
        }

        [Test]
        public void CompareTo_EdgeIsGreater_ReturnsNumberLessThanZero()
        {
            var other = new Node<Int32, Double>.Edge(100.00, _node);

            Int32 result = _edge.CompareTo(other);

            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void CompareTo_EdgesAreEqual_ReturnsZero()
        {
            var other = new Node<Int32, Double>.Edge(65.80, _node);

            Int32 result = _edge.CompareTo(other);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CompareTo_EdgeIsLess_ReturnsNumberGreaterThanZero()
        {
            var other = new Node<Int32, Double>.Edge(50.00, _node);

            Int32 result = _edge.CompareTo(other);

            Assert.That(result, Is.GreaterThan(0));
        }
    }
}
