using System;
using System.Collections.Generic;

namespace MinimumSpanningTree.NonlinearDs
{
    public class DisjointSet<TNodeValue, TWeight>
        where TWeight : IComparable<TWeight>
    {
        private Dictionary<Node<TNodeValue, TWeight>, DisjointSetNode> _disjointSetSingleElements;
        private List<DisjointSetNode> _disjointSet;

        public DisjointSet()
        {
            _disjointSetSingleElements = new Dictionary<Node<TNodeValue, TWeight>, DisjointSetNode>();
            _disjointSet = new List<DisjointSetNode>();
        }

        public Int32 NumberOfComponents
            => _disjointSet.Count;

        public void AddToSet(IEnumerable<Node<TNodeValue, TWeight>> elements)
        {
            foreach(var element in elements)
            {
                if (_disjointSetSingleElements.TryGetValue(element, out _))
                    continue;

                var newNode = new DisjointSetNode(element);
                _disjointSetSingleElements.Add(element, newNode);
                _disjointSet.Add(newNode);
            }
        }

        public Node<TNodeValue, TWeight> Find(Node<TNodeValue, TWeight> element)
        {
            var node = _disjointSetSingleElements[element];

            var componentRepresentative = FindComponentRepresentative(node);

            return componentRepresentative.Element;
        }

        public void Union(Node<TNodeValue, TWeight> elementX, Node<TNodeValue, TWeight> elementY)
        {
            DisjointSetNode representativeOfX = FindComponentRepresentative(_disjointSetSingleElements[elementX]);
            DisjointSetNode representativeOfY = FindComponentRepresentative(_disjointSetSingleElements[elementY]);

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

        private DisjointSetNode FindComponentRepresentative(DisjointSetNode node)
        {
            if (node == node.Parent)
                return node;

            return FindComponentRepresentative(node.Parent);
        }

        public class DisjointSetNode : IComparable<DisjointSetNode>
        {
            public DisjointSetNode(Node<TNodeValue, TWeight> element)
            {
                Element = element;
                Parent = this;
                Rank = 0;
            }

            public Node<TNodeValue, TWeight> Element { get; }

            public DisjointSetNode Parent { get; set; }

            public Int32 Rank { get; set; }

            public int CompareTo(DisjointSetNode other)
                => Rank.CompareTo(other.Rank);
        }
    }
}
