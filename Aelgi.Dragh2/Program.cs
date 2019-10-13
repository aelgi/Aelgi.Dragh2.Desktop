using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using GLFW;
using SkiaSharp;

namespace Aelgi.Dragh2
{
    class Program
    {
        private static NativeWindow window;
        private static SKCanvas canvas;

        private static Keys? lastKeyPressed;
        private static Point? lastMousePosition;

        //----------------------------------
        //NOTE: On Windows you must copy SharedLib manually (https://github.com/ForeverZer0/glfw-net#microsoft-windows)
        //----------------------------------

        static void Main(string[] args)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            var dragh = new Dragh();
            dragh.Startup();
            dragh.Run();
        }
    }
}
