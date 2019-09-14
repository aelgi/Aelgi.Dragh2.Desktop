using System;

namespace Aelgi.Dragh2
{
    class Program
    {
        static void Main(string[] args)
        {
            var dragh = new Dragh();
            dragh.Startup();

            dragh.Run();
        }
    }
}
