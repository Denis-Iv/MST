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

        public Graph<TValue, TWeightedFactor> FindMinimumSpanningTree()
        {
            var mstEdges = new List<(Node<TValue, TWeightedFactor> Node, Node<TValue, TWeightedFactor>.Edge Edge)>();


            var disjointSet = new DisjointSet<TValue, TWeightedFactor>();
            disjointSet.AddToSet(_data.Values);

            while (disjointSet.NumberOfComponents > 1)
            {
                var cheapest = new Dictionary<Node<TValue, TWeightedFactor>, (Node<TValue, TWeightedFactor> Node, Node<TValue, TWeightedFactor>.Edge Edge)>();
                
                foreach (var node in _data.Values)
                {
                    foreach (var edge in node.Edges)
                    {
                        var component1 = disjointSet.Find(node);
                        var component2 = disjointSet.Find(edge.Destination);

                        if (component1.Equals(component2))
                            continue;

                        if (cheapest.ContainsKey(component1) && cheapest[component1].Edge.CompareTo(edge) > 0)
                            cheapest[component1] = (node, edge);
                        else
                            cheapest.TryAdd(component1, (node, edge));

                        if (cheapest.ContainsKey(component2) && cheapest[component2].Edge.CompareTo(edge) > 0)
                            cheapest[component2] = (node, edge);
                        else
                            cheapest.TryAdd(component2, (node, edge));
                    }
                }

                foreach (var node in _data.Values.Where(node => cheapest.ContainsKey(node)))
                {
                    mstEdges.Add((cheapest[node].Node, cheapest[node].Edge));

                    var component1 = disjointSet.Find(cheapest[node].Node);
                    var component2 = disjointSet.Find(cheapest[node].Edge.Destination);

                    if (component1.Equals(component2))
                        continue;

                    disjointSet.Union(component1, component2);
                }
            }

            return GenerateMinimumSpanningTreeGraph(mstEdges);
        }

        private Graph<TValue, TWeightedFactor> GenerateMinimumSpanningTreeGraph(List<(Node<TValue, TWeightedFactor> Node, Node<TValue, TWeightedFactor>.Edge Edge)> mstEdges)
        {
            var mstGraph = new Graph<TValue, TWeightedFactor>();
            
            foreach (var tuple in mstEdges)
            {
                var sourceNode = tuple.Node;
                var edge = tuple.Edge;
                var destinationNode = edge.Destination;
                
                var sourceNodeId = _data.FirstOrDefault(x => x.Value.Equals(sourceNode)).Key;
                var destinationNodeId = _data.FirstOrDefault(x => x.Value.Equals(destinationNode)).Key;
                
                mstGraph.ConnectTwoWay(
                    sourceNodeId, 
                    sourceNode.Value, 
                    destinationNodeId, 
                    destinationNode.Value, 
                    edge.WeightedFactor
                    );
            }

            return mstGraph;
        }
    }
}
