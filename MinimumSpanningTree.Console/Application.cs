using System;

namespace MinimumSpanningTree.ConsoleUI
{
    using NonlinearDs;
    using Example;

    public static class Application
    {
        public static void Main()
        {
            var graph = ExampleGraphs.Small();
            Boruvka.FindMST(graph);
        }
    }
}
