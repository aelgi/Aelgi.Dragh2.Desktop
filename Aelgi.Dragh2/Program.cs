using System;
using System.Drawing;
using System.Runtime.InteropServices;
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
            var dragh = new Dragh();
            dragh.Startup();
            dragh.Run();
        }

        static void Main2(string[] args)
        {
            using (Program.window = new NativeWindow(800, 600, "Skia Example"))
            {
                Program.SubscribeToWindowEvents();

                using (var context = Program.GenerateSkiaContext(Program.window))
                {
                    using (var skiaSurface = Program.GenerateSkiaSurface(context, Program.window.ClientSize))
                    {
                        Program.canvas = skiaSurface.Canvas;

                        while (!Program.window.IsClosing)
                        {
                            Program.Render();
                            Glfw.WaitEvents();
                        }
                    }
                }
            }
        }

        private static void SubscribeToWindowEvents()
        {
            Program.window.SizeChanged += Program.OnWindowsSizeChanged;
            Program.window.Refreshed += Program.OnWindowRefreshed;
            Program.window.KeyPress += Program.OnWindowKeyPress;
            Program.window.MouseMoved += Program.OnWindowMouseMoved;
        }

        private static GRContext GenerateSkiaContext(NativeWindow nativeWindow)
        {
            var nativeContext = Program.GetNativeContext(nativeWindow);
            var glInterface = GRGlInterface.AssembleGlInterface(nativeContext, (contextHandle, name) => Glfw.GetProcAddress(name));
            return GRContext.Create(GRBackend.OpenGL, glInterface);
        }

        private static object GetNativeContext(NativeWindow nativeWindow)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Native.GetWglContext(nativeWindow);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // XServer
                return Native.GetGLXContext(nativeWindow);
                // Wayland
                //return Native.GetEglContext(nativeWindow);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Native.GetNSGLContext(nativeWindow);
            }

            throw new PlatformNotSupportedException();
        }

        private static SKSurface GenerateSkiaSurface(GRContext skiaContext, Size surfaceSize)
        {
            var frameBufferInfo = new GRGlFramebufferInfo((uint)new UIntPtr(0), GRPixelConfig.Rgba8888.ToGlSizedFormat());
            var backendRenderTarget = new GRBackendRenderTarget(surfaceSize.Width,
                                                                surfaceSize.Height,
                                                                0,
                                                                8,
                                                                frameBufferInfo);
            return SKSurface.Create(skiaContext, backendRenderTarget, GRSurfaceOrigin.BottomLeft, SKImageInfo.PlatformColorType);
        }

        private static void Render()
        {
            Program.canvas.Clear(SKColor.Parse("#F0F0F0"));
            var headerPaint = new SKPaint { Color = SKColor.Parse("#333333"), TextSize = 50, IsAntialias = true };
            Program.canvas.DrawText("Hello from GLFW.NET + SkiaSharp!", 10, 60, headerPaint);

            var inputInfoPaint = new SKPaint { Color = SKColor.Parse("#F34336"), TextSize = 18, IsAntialias = true };
            Program.canvas.DrawText($"Last key pressed: {Program.lastKeyPressed}", 10, 90, inputInfoPaint);
            Program.canvas.DrawText($"Last mouse position: {Program.lastMousePosition}", 10, 120, inputInfoPaint);

            var exitInfoPaint = new SKPaint { Color = SKColor.Parse("#3F51B5"), TextSize = 18, IsAntialias = true };
            Program.canvas.DrawText("Press Enter to Exit.", 10, 160, exitInfoPaint);

            var p = new SKPaint
            {
                IsAntialias = true,
                Color = new SKColor(0x2c, 0x3e, 0x50),
                StrokeCap = SKStrokeCap.Round
            };
            var path = new SKPath();
            path.MoveTo(71.4311121f, 56f);
            path.CubicTo(68.6763107f, 56.0058575f, 65.9796704f, 57.5737917f, 64.5928855f, 59.965729f);
            path.LineTo(43.0238921f, 97.5342563f);
            path.CubicTo(41.6587026f, 99.9325978f, 41.6587026f, 103.067402f, 43.0238921f, 105.465744f);
            path.LineTo(64.5928855f, 143.034271f);
            path.CubicTo(65.9798162f, 145.426228f, 68.6763107f, 146.994582f, 71.4311121f, 147f);
            path.LineTo(114.568946f, 147f);
            path.CubicTo(117.323748f, 146.994143f, 120.020241f, 145.426228f, 121.407172f, 143.034271f);
            path.LineTo(142.976161f, 105.465744f);
            path.CubicTo(144.34135f, 103.067402f, 144.341209f, 99.9325978f, 142.976161f, 97.5342563f);
            path.LineTo(121.407172f, 59.965729f);
            path.CubicTo(120.020241f, 57.5737917f, 117.323748f, 56.0054182f, 114.568946f, 56f);
            path.LineTo(71.4311121f, 56f);
            path.Close();
            canvas.DrawPath(path, p);

            Program.canvas.Flush();
            Program.window.SwapBuffers();
        }

        #region Window Events Handlers

        private static void OnWindowsSizeChanged(object sender, SizeChangeEventArgs e)
        {
            Program.Render();
        }

        private static void OnWindowKeyPress(object sender, KeyEventArgs e)
        {
            Program.lastKeyPressed = e.Key;
            if (e.Key == Keys.Enter || e.Key == Keys.NumpadEnter)
            {
                Program.window.Close();
            }
        }

        private static void OnWindowMouseMoved(object sender, MouseMoveEventArgs e)
        {
            Program.lastMousePosition = e.Position;
        }

        private static void OnWindowRefreshed(object sender, EventArgs e)
        {
            Console.WriteLine("Hello");
            Program.Render();
        }

        #endregion
    }
}
