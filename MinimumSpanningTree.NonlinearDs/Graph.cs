using System;
using System.Collections;
using System.Collections.Generic;

namespace MinimumSpanningTree.NonlinearDs
{
    public class Graph<TValue, TWeightedFactor> : IEnumerable<Node<TValue, TWeightedFactor>>
        where TWeightedFactor : IComparable<TWeightedFactor>
    {
        private readonly Dictionary<Object, Node<TValue, TWeightedFactor>> _data;

        public Graph()
        {
            Size = 0;
            _data = new Dictionary<Object, Node<TValue, TWeightedFactor>>();            
        }

        public Int32 Size { get; private set; }

        public Node<TValue, TWeightedFactor> this[Object identity]
            => _data[identity];

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public IEnumerator<Node<TValue, TWeightedFactor>> GetEnumerator()
        {
            foreach (var item in _data)
                yield return item.Value;
        }

        public void Join(Object identity, TValue value)
        {
            var newNode = new Node<TValue, TWeightedFactor>(value);

            if (!_data.TryAdd(identity, newNode))
                return;

            Size++;
        }

        public void Connect(
            Object sourceIdentity, 
            Object destinationIdentity, 
            TWeightedFactor weightedFactor)
        {
            var sourceNode = _data[sourceIdentity];
            var destinationNode = _data[destinationIdentity];

            var edge = new Node<TValue, TWeightedFactor>.Edge(weightedFactor, destinationNode);
            sourceNode.AddEdge(edge);
        }

        public void Connect(
            Object sourceIdentity, 
            Object destinationIdentity, 
            TValue destinationValue, 
            TWeightedFactor weightedFactor)
        {
            Join(destinationIdentity, destinationValue);

            Connect(sourceIdentity, destinationIdentity, weightedFactor);
        }

        public void ConnectTwoWay(
            Object nodeAIdentity, 
            Object nodeBIdentity, 
            TWeightedFactor weightedFactor)
        {
            Connect(nodeAIdentity, nodeBIdentity, weightedFactor);
            Connect(nodeBIdentity, nodeAIdentity, weightedFactor);
        }

        public void ConnectTwoWay(
            Object nodeAIdentity, 
            TValue nodeAValue,
            Object nodeBIdentity,
            TValue nodeBValue,
            TWeightedFactor weightedFactor)
        {
            Join(nodeAIdentity, nodeAValue);
            Join(nodeBIdentity, nodeBValue);

            ConnectTwoWay(nodeAIdentity, nodeBIdentity, weightedFactor);
        }
    }
}
