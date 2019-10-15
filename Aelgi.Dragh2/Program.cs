using System.Threading;

namespace Aelgi.Dragh2
{
    internal class Program
    {
        //----------------------------------
        //NOTE: On Windows you must copy SharedLib manually (https://github.com/ForeverZer0/glfw-net#microsoft-windows)
        //----------------------------------

        public static void Main(string[] args)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            var dragh = new Dragh();
            dragh.Startup();
            dragh.Run();
        }
    }
}
