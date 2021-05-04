using System;
using System.Collections.Generic;

namespace MinimumSpanningTree.NonlinearDs
{
    public class Node<TValue, TWeightedFactor> : IEquatable<Node<TValue, TWeightedFactor>>
        where TWeightedFactor : IComparable<TWeightedFactor>
    {
        private readonly HashSet<Edge> _edges;

        public Node(TValue value)
        {
            Value = value;
            _edges = new HashSet<Edge>();
        }

        public TValue Value { get; }

        public IReadOnlyCollection<Edge> Edges
            => _edges;

        public Boolean Equals(Node<TValue, TWeightedFactor> other)
        {
            if (other is null)
                return false;

            return Value.Equals(other.Value) && _edges.SetEquals(other.Edges);
        }

        public void AddEdge(Edge edge)
            => _edges.Add(edge);

        public void RemoveEdge(Edge edge)
            => _edges.Remove(edge);

        public override Boolean Equals(Object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is Node<TValue, TWeightedFactor> other)
                return Equals(other);

            return false;
        }

        public override String ToString()
            => Value.ToString();

        public override Int32 GetHashCode()
            => HashCode.Combine(Value, Edges);


        public class Edge : IEquatable<Edge>, IComparable<Edge>
        {
            public Edge(TWeightedFactor weightedFactor, Node<TValue, TWeightedFactor> destination)
            {
                WeightedFactor = weightedFactor;
                Destination = destination;                
            }

            public TWeightedFactor WeightedFactor { get; }

            public Node<TValue, TWeightedFactor> Destination { get; }

            public Boolean Equals(Edge other)
            {
                if (other is null)
                    return false;

                return WeightedFactor.Equals(other.WeightedFactor) && Destination.Equals(other.Destination);
            }

            public Int32 CompareTo(Edge other)
                => WeightedFactor.CompareTo(other.WeightedFactor);

            public override Boolean Equals(Object obj)
            {
                if (obj is null)
                    return false;

                if (ReferenceEquals(this, obj))
                    return true;

                if (obj is Edge other)
                    return Equals(other);

                return false;
            }

            public override String ToString()
                => String.Format("Weighted Factor: {0}\nDestination: {1}", WeightedFactor, Destination);

            public override Int32 GetHashCode()
                => HashCode.Combine(WeightedFactor, Destination);
        }
    }
}
