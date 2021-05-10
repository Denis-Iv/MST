using System;

namespace MinimumSpanningTree.ConsoleUI
{
    using NonlinearDs;
    
    public static class Application
    {
        public static void Main()
        {
            var graph = ExampleGraphs.Small();
            graph.FindMST();
        }
    }
}
