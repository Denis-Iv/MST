using System;
using System.Collections.Generic;

namespace MinimumSpanningTree.ConsoleUI
{
    using NonlinearDs;
    
    public static class Application
    {
        public static void Main()
        {
            var graph = ExampleGraphs.Small().FindMinimumSpanningTree();
            var visitedNodes = new HashSet<Node<Int32, Int32>>();
            
            Int32 mstSumOfEdges = 0;

            foreach (var node in graph)
            {
                foreach (var edge in node.Edges)
                {
                    if (visitedNodes.Contains(edge.Destination))
                        continue;

                    mstSumOfEdges += edge.WeightedFactor;
                }

                visitedNodes.Add(node);
                Console.WriteLine(node);
            }

            Console.WriteLine(mstSumOfEdges);
            
            
        }
    }
}
