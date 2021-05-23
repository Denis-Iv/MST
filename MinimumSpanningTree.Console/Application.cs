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
            var graph = ExampleGraphs.Large();

            sw.Start();
            var mst = graph.FindMinimumSpanningTreeParallel();
            sw.Stop();
            //var visitedNodes = new HashSet<Node<Int32, Int32>>();
            
            //Int32 mstSumOfEdges = 0;

            //foreach (var node in mst)
            //{
            //    foreach (var edge in node.Edges)
            //    {
            //        if (visitedNodes.Contains(edge.Destination))
            //            continue;

            //        mstSumOfEdges += edge.WeightedFactor;
            //    }

            //    visitedNodes.Add(node);
            //    Console.WriteLine(node);
            //}

            //Console.WriteLine(mstSumOfEdges);

            TimeSpan ts = sw.Elapsed;
            string elapsedTime = ts.Milliseconds.ToString();
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
