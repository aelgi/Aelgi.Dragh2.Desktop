using GLFW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using SkiaSharp;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Aelgi.Dragh2
{
    class AssemblyLoader : AssemblyLoadContext
    {
        protected override Assembly Load(AssemblyName assemblyName)
        {
            var deps = DependencyContext.Default;
            var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
            var assembly = Assembly.Load(new AssemblyName(res.First().Name));
            return assembly;
        }
    }

    public class Dragh
    {

        protected IServiceProvider RegisterServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        protected void CreateResources()
        {
        }

        private void LoadGlfw()
        {
            var asl = new AssemblyLoader();
            var asm = asl.LoadFromAssemblyPath("glfw3.dll");
        }

        public void Startup()
        {
            var services = new ServiceCollection();

            var provider = RegisterServices(services);
        }

        public void Run()
        {
            using (var window = new NativeWindow(800, 600, "Dragh 2.0"))
            {
                window.SizeChanged += SizeChanged;
                window.Refreshed += Refreshed;
                window.KeyPress += KeyPress;
                window.MouseMoved += MouseMoved;

                var nativeContext = GetNativeContext(window);
                var glInterface = GRGlInterface.AssembleGlInterface(nativeContext, (contextHandle, name) => Glfw.GetProcAddress(name));
                using (var context = GRContext.Create(GRBackend.OpenGL, glInterface))
                {
                    var frameBufferInfo = new GRGlFramebufferInfo((uint)new UIntPtr(0), GRPixelConfig.Rgba8888.ToGlSizedFormat());
                    var surfaceSize = window.ClientSize;
                    var backendRenderTarget = new GRBackendRenderTarget(surfaceSize.Width, surfaceSize.Height, 0, 8, frameBufferInfo);
                    using (var surface = SKSurface.Create(context, backendRenderTarget, GRSurfaceOrigin.BottomLeft, SKImageInfo.PlatformColorType))
                    {
                        var canvas = surface.Canvas;
                        while (!window.IsClosing)
                        {
                            Render(window, canvas);
                            Glfw.PollEvents();
                        }
                    }
                }

            }
        }

        public void Render(NativeWindow window, SKCanvas canvas)
        {
            canvas.Clear(SKColor.Parse("#F0F0F0"));

            var headerPaint = new SKPaint { Color = SKColor.Parse("#333333"), TextSize = 50, IsAntialias = true };
            canvas.DrawText("Hello from GLFW.NET + SkiaSharp!", 10, 60, headerPaint);

            var inputInfoPaint = new SKPaint { Color = SKColor.Parse("#F34336"), TextSize = 18, IsAntialias = true };
            canvas.DrawText($"Last key pressed", 10, 90, inputInfoPaint);
            canvas.DrawText($"Last mouse position", 10, 120, inputInfoPaint);

            var exitInfoPaint = new SKPaint { Color = SKColor.Parse("#3F51B5"), TextSize = 18, IsAntialias = true };
            canvas.DrawText("Press Enter to Exist", 10, 160, exitInfoPaint);

            canvas.Flush();
            window.SwapBuffers();
        }

        private static object GetNativeContext(NativeWindow nativeWindow)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Native.GetWglContext(nativeWindow);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return Native.GetGLXContext(nativeWindow);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return Native.GetNSGLContext(nativeWindow);

            throw new PlatformNotSupportedException();
        }

        private void MouseMoved(object sender, MouseMoveEventArgs e)
        {
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
        }

        private void Refreshed(object sender, EventArgs e)
        {
        }

        private void SizeChanged(object sender, SizeChangeEventArgs e)
        {
        }
    }
}
