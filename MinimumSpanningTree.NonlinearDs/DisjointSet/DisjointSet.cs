using System;
using System.Collections.Generic;


namespace MinimumSpanningTree.NonlinearDs.DisjointSet
{
    public class DisjointSet<T>
    {
        private Dictionary<T, DisjointSetNode<T>> _disjointSetSingleElements;
        private List<DisjointSetNode<T>> _disjointSet;

        public DisjointSet()
        {
            _disjointSetSingleElements = new Dictionary<T, DisjointSetNode<T>>();
            _disjointSet = new List<DisjointSetNode<T>>();
        }

        public Int32 NumberOfComponents
            => _disjointSet.Count;

        //must ask team for feedback on this method
        public void AddToSet(T element)
        {
            var node = _disjointSetSingleElements[element];

            if(node == null)
            {
                var newNode = new DisjointSetNode<T>(element);
                _disjointSetSingleElements.Add(element, newNode);
                _disjointSet.Add(newNode);
            }
        }

        public T Find(T element)
        {
            var node = _disjointSetSingleElements[element];

            var componentRepresentative = FindComponentRepresentative(node);

            return componentRepresentative.Element;
        }

        public void Union(T elementX, T elementY)
        {
            DisjointSetNode<T> representativeOfX = FindComponentRepresentative(_disjointSetSingleElements[elementX]);
            DisjointSetNode<T> representativeOfY = FindComponentRepresentative(_disjointSetSingleElements[elementY]);

            if (representativeOfX == representativeOfY)
                return;

            int comparison = representativeOfX.CompareTo(representativeOfY);

            if (comparison < 0)
            {
                representativeOfX.Parent = representativeOfY;
                _disjointSet.Remove(representativeOfX);
            }
            else if (comparison > 0)
            {
                representativeOfY.Parent = representativeOfX;
                _disjointSet.Remove(representativeOfY);
            }
            else
            {
                representativeOfX.Parent = representativeOfY;
                _disjointSet.Remove(representativeOfX);
                representativeOfY.Rank++;
            }
        }

        private DisjointSetNode<T> FindComponentRepresentative(DisjointSetNode<T> node)
        {
            if (node == node.Parent)
                return node;

            return FindComponentRepresentative(node.Parent);
        }

        public class DisjointSetNode<NodeType> : IComparable<DisjointSetNode<NodeType>>
        {
            public DisjointSetNode(NodeType element)
            {
                Element = element;
                Parent = this;
                Rank = 0;
            }

            public NodeType Element { get; }

            public DisjointSetNode<NodeType> Parent { get; set; }

            public Int32 Rank { get; set; }

            public int CompareTo(DisjointSetNode<NodeType> other)
                => Rank.CompareTo(other.Rank);
        }

    }
}
