using System;
using MinimumSpanningTree.NonlinearDs;


namespace MinimumSpanningTree.ConsoleUI
{
    public static class ExampleGraphs
    {
        public static Graph<Int32, Int32> Small()
        {
            var graph = new Graph<Int32, Int32>();

            graph.ConnectTwoWay(0, 0, 1 ,1, 4);
            graph.ConnectTwoWay(1, 1, 2, 2, 1);
            graph.ConnectTwoWay(2, 2, 3, 3, 2);
            graph.ConnectTwoWay(3, 3, 0, 0, 5);
            graph.ConnectTwoWay(1, 1, 3, 3, 3);

            // MST = 7
            return graph;
        }

        public static Graph<Int32, Int32> Medium()
        {
            var graph = new Graph<Int32, Int32>();

            graph.ConnectTwoWay(0, 0, 1, 1, 15);
            graph.ConnectTwoWay(1, 1, 2, 2, 10);
            graph.ConnectTwoWay(2, 2, 3, 3, 8);
            graph.ConnectTwoWay(3, 3, 4, 4, 18);
            graph.ConnectTwoWay(4, 4, 5, 5, 1);
            graph.ConnectTwoWay(5, 5, 0, 0, 4);
            graph.ConnectTwoWay(1, 1, 4, 4, 11);
            graph.ConnectTwoWay(0, 0, 3, 3, 13);

            // MST = 34
            return graph;
        }

        public static Graph<Int32, Int32> Large()
        {
            var graph = new Graph<Int32, Int32>();

            graph.ConnectTwoWay(0, 0, 1, 1, 4);
            graph.ConnectTwoWay(1, 1, 2, 2, 8);
            graph.ConnectTwoWay(2, 2, 3, 3, 7);
            graph.ConnectTwoWay(3, 3, 4, 4, 9);
            graph.ConnectTwoWay(4, 4, 5, 5, 10);
            graph.ConnectTwoWay(5, 5, 6, 6, 2);
            graph.ConnectTwoWay(6, 6, 7, 7, 1);
            graph.ConnectTwoWay(7, 7, 8, 8, 7);
            graph.ConnectTwoWay(0, 0, 7, 7, 8);
            graph.ConnectTwoWay(7, 7, 1, 1, 11);
            graph.ConnectTwoWay(8, 8, 2, 2, 2);
            graph.ConnectTwoWay(2, 2, 5, 5, 4);
            graph.ConnectTwoWay(3, 3, 5, 5, 14);
            graph.ConnectTwoWay(8, 8, 6, 6, 6);

            // MST = 37
            return graph;
        }
    }
}
