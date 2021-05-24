using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MinimumSpanningTree.ConsoleUI
{
    using NonlinearDs;
    
    public static class Application
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();
            var graph = ExampleGraphs.GenerateRandomGraph(2000);

            sw.Start();
            var mst = graph.FindMinimumSpanningTree();
            sw.Stop();

            TimeSpan ts = sw.Elapsed;
            String elapsedTimeConcurrent = ts.ToString();

            sw.Reset();

            sw.Start();
            mst = graph.FindMinimumSpanningTreeParallel();
            sw.Stop();
            
            ts = sw.Elapsed;
            String elapsedTimeParallel = ts.ToString();

            Console.WriteLine($"Runtime concurrent:\t {elapsedTimeConcurrent}\n" +
                              $"Runtime parallel:\t {elapsedTimeParallel}\n");


            var visitedNodes = new HashSet<Node<Int32, Int32>>();

            Int32 mstSumOfEdges = 0;

            foreach (var node in mst)
            {
                foreach (var edge in node.Edges)
                {
                    if (visitedNodes.Contains(edge.Destination))
                        continue;

                    mstSumOfEdges += edge.WeightedFactor;
                }

                visitedNodes.Add(node);
                //Console.WriteLine(node);
            }

            Console.WriteLine($"MST: {mstSumOfEdges}");
        }
    }
}
