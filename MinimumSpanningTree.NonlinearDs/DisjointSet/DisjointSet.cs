using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumSpanningTree.NonlinearDs.DisjointSet
{
    public class DisjointSet<TNodeValue, TWeight>
        where TWeight : IComparable<TWeight>
    {
        private Dictionary<Node<TNodeValue, TWeight>, DisjointSetNode<Node<TNodeValue, TWeight>>> _disjointSetSingleElements;
        private List<DisjointSetNode<Node<TNodeValue, TWeight>>> _disjointSet;

        public DisjointSet()
        {
            _disjointSetSingleElements = new Dictionary<Node<TNodeValue, TWeight>, DisjointSetNode<Node<TNodeValue, TWeight>>>();
            _disjointSet = new List<DisjointSetNode<Node<TNodeValue, TWeight>>>();
        }

        public Int32 NumberOfComponents
            => _disjointSet.Count;

        public void AddToSet(IEnumerable<Node<TNodeValue, TWeight>> elements)
        {
            foreach(var element in elements)
            {
                if (!_disjointSetSingleElements.TryGetValue(element, out _))
                    return;

                var newNode = new DisjointSetNode<Node<TNodeValue, TWeight>>(element);
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
            DisjointSetNode<Node<TNodeValue, TWeight>> representativeOfX = FindComponentRepresentative(_disjointSetSingleElements[elementX]);
            DisjointSetNode<Node<TNodeValue, TWeight>> representativeOfY = FindComponentRepresentative(_disjointSetSingleElements[elementY]);

            if (representativeOfX == representativeOfY)
                return;

            int comparisson = representativeOfX.CompareTo(representativeOfY);

            if (comparisson < 0)
            {
                representativeOfX.Parent = representativeOfY;
                _disjointSet.Remove(representativeOfX);
            }
            else if (comparisson > 0)
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

        private DisjointSetNode<Node<TNodeValue, TWeight>> FindComponentRepresentative(DisjointSetNode<Node<TNodeValue, TWeight>> node)
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
