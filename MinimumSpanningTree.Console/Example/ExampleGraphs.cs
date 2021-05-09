using System;
using MinimumSpanningTree.NonlinearDs;


namespace MinimumSpanningTree.ConsoleUI.Example
{
    public static class ExampleGraphs
    {
        public static Graph<Int32, Int32> Small()
        {
            var graph = new Graph<Int32, Int32>();

            graph.Join(0, 0);
            graph.Join(1, 1);
            graph.Join(2, 2);
            graph.Join(3, 3);

            graph.ConnectTwoWay(0, 1, 4);
            graph.ConnectTwoWay(1, 2, 1);
            graph.ConnectTwoWay(2, 3, 2);
            graph.ConnectTwoWay(3, 0, 5);
            graph.ConnectTwoWay(1, 3, 3);

            // MST = 10
            return graph;
        }

        public static Graph<Int32, Int32> Medium()
        {
            var graph = new Graph<Int32, Int32>();

            graph.Join(0, 0);
            graph.Join(1, 1);
            graph.Join(2, 2);
            graph.Join(3, 3);
            graph.Join(4, 4);
            graph.Join(5, 5);

            graph.ConnectTwoWay(0, 1, 15);
            graph.ConnectTwoWay(1, 2, 10);
            graph.ConnectTwoWay(2, 3, 8);
            graph.ConnectTwoWay(3, 4, 18);
            graph.ConnectTwoWay(4, 5, 1);
            graph.ConnectTwoWay(5, 0, 4);
            graph.ConnectTwoWay(1, 4, 11);

            // MST = 34
            return graph;
        }

        public static Graph<Int32, Int32> Large()
        {
            var graph = new Graph<Int32, Int32>();

            graph.Join(0, 0);
            graph.Join(1, 1);
            graph.Join(2, 2);
            graph.Join(3, 3);
            graph.Join(4, 4);
            graph.Join(5, 5);
            graph.Join(6, 6);
            graph.Join(7, 7);
            graph.Join(8, 8);

            graph.ConnectTwoWay(0, 1, 4);
            graph.ConnectTwoWay(1, 2, 8);
            graph.ConnectTwoWay(2, 3, 7);
            graph.ConnectTwoWay(3, 4, 9);
            graph.ConnectTwoWay(4, 5, 10);
            graph.ConnectTwoWay(5, 6, 2);
            graph.ConnectTwoWay(6, 7, 1);
            graph.ConnectTwoWay(7, 8, 7);
            graph.ConnectTwoWay(0, 7, 8);
            graph.ConnectTwoWay(7, 1, 11);
            graph.ConnectTwoWay(8, 2, 2);
            graph.ConnectTwoWay(2, 5, 4);
            graph.ConnectTwoWay(3, 5, 14);
            graph.ConnectTwoWay(8, 6, 6);

            // MST = 37
            return graph;
        }
    }
}
