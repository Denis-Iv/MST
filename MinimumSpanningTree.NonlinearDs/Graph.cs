using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public void FindMST() // replace void with a return value in the end
        {
            var disjointSet = new DisjointSet<TValue, TWeightedFactor>();
            var nodesList = new List<Node<TValue, TWeightedFactor>>(_data.Values.ToList());
            disjointSet.AddToSet(nodesList);

            while (disjointSet.NumberOfComponents > 1)
            {
                var cheapest = new Dictionary<Node<TValue, TWeightedFactor>,Dictionary<Node<TValue, TWeightedFactor>, Node<TValue, TWeightedFactor>.Edge>>();

                foreach (var node in nodesList)
                {
                    foreach (var edge in node.Edges)
                    {
                        var component1 = disjointSet.Find(node);
                        var component2 = disjointSet.Find(edge.Destination);

                        if (component1.Equals(component2))
                            continue;

                        if (cheapest.ContainsKey(component1))
                        {
                            Int32 comparison = cheapest[component1].Values.SingleOrDefault().CompareTo(edge);
                            if (comparison > 0)
                            {
                                cheapest[component1] =
                                    new Dictionary<Node<TValue, TWeightedFactor>, Node<TValue, TWeightedFactor>.Edge>
                                    {
                                        {node, edge}
                                    };
                            }
                        }
                        else
                        {
                            cheapest.Add(component1, new Dictionary<Node<TValue, TWeightedFactor>, Node<TValue, TWeightedFactor>.Edge>());
                            cheapest[component1].Add(node, edge);
                        }

                        if (cheapest.ContainsKey(component2))
                        {
                            Int32 comparison = cheapest[component2].Values.SingleOrDefault().CompareTo(edge);
                            if (comparison > 0)
                            {
                                cheapest[component1] =
                                    new Dictionary<Node<TValue, TWeightedFactor>, Node<TValue, TWeightedFactor>.Edge>
                                    {
                                        {node, edge}
                                    };
                            }
                        }
                        else
                        {
                            cheapest.Add(component2, new Dictionary<Node<TValue, TWeightedFactor>, Node<TValue, TWeightedFactor>.Edge>());
                            cheapest[component2].Add(node, edge);
                        }
                    }
                }

                return;
            }
        }
    }
}
