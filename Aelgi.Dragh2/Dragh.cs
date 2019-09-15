using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Services;
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
        protected IServiceProvider _services;
        protected KeyboardService _keyboard;

        protected IServiceProvider RegisterServices(IServiceCollection services)
        {
            _keyboard = new KeyboardService();
            services.AddSingleton<IKeyboardService>(_keyboard);
            services.AddSingleton<ITextService, TextService>();

            return services.BuildServiceProvider().CreateScope().ServiceProvider;
        }

        public void Startup()
        {
            var services = new ServiceCollection();

            var window = new NativeWindow(800, 600, "Dragh 2.0");
            window.SizeChanged += SizeChanged;
            window.KeyPress += KeyPress;
            window.KeyRelease += KeyRelease;
            window.MouseMoved += MouseMoved;

            var nativeContext = GetNativeContext(window);
            var glInterface = GRGlInterface.AssembleGlInterface(nativeContext, (contextHandle, name) => Glfw.GetProcAddress(name));
            var context = GRContext.Create(GRBackend.OpenGL, glInterface);

            var frameBufferInfo = new GRGlFramebufferInfo((uint)new UIntPtr(0), GRPixelConfig.Rgba8888.ToGlSizedFormat());
            var surfaceSize = window.ClientSize;
            var backendRenderTarget = new GRBackendRenderTarget(surfaceSize.Width, surfaceSize.Height, 0, 8, frameBufferInfo);
            var surface = SKSurface.Create(context, backendRenderTarget, GRSurfaceOrigin.BottomLeft, SKImageInfo.PlatformColorType);
            var canvas = surface.Canvas;

            services.AddSingleton(window);
            services.AddSingleton(canvas);

            _services = RegisterServices(services);
        }

        public void Run()
        {
            var window = _services.GetService<NativeWindow>();
            var canvas = _services.GetService<SKCanvas>();

            while (!window.IsClosing)
            {
                Render();
                Glfw.PollEvents();
            }
        }

        public void Render()
        {
            var window = _services.GetService<NativeWindow>();
            var canvas = _services.GetService<SKCanvas>();

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

        private Key ConvertKeyInput(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    return Key.LEFT;
                case Keys.D:
                    return Key.RIGHT;
                case Keys.Escape:
                    return Key.ESCAPE;
                default: return Key.NONE;
            }
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            var key = ConvertKeyInput(e.Key);
            if (key != Key.NONE) _keyboard.SetKey(key, true);
        }

        private void KeyRelease(object sender, KeyEventArgs e)
        {
            var key = ConvertKeyInput(e.Key);
            if (key != Key.NONE) _keyboard.SetKey(key, false);
        }

        private void Refreshed(object sender, EventArgs e)
        {
            // Again I dont think I need to do anything for this
        }

        private void SizeChanged(object sender, SizeChangeEventArgs e)
        {
            // We dont need to do anything at the moment because the main render loop re-renders
        }
    }
}
