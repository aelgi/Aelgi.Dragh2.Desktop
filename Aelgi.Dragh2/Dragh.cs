using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.HUD;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Services;
using GLFW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using SkiaSharp;
using System;
using System.Diagnostics;
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
        protected bool _close = false;
        protected Stopwatch _framesTimer = new Stopwatch();

        protected IServiceProvider RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IUtilityService, UtilityService>();
            _keyboard = new KeyboardService();
            services.AddSingleton<IKeyboardService>(_keyboard);
            services.AddSingleton<ITextService, TextService>();
            services.AddSingleton<IGameRenderService, GameRenderService>();
            services.AddSingleton<IGameUpdateService, GameUpdateService>();

            services.AddSingleton<HUDController>();

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
                Update();
                Render();
                Glfw.PollEvents();
            }
        }

        public void Update()
        {
            _framesTimer.Stop();
            var ts = _framesTimer.ElapsedMilliseconds;
            _framesTimer.Restart();
            var fps = 1000 * ts / 60;

            var gameService = _services.GetService<IGameUpdateService>();
            gameService.SetFPS((int)fps);

            var keyboard = _services.GetService<IKeyboardService>();
            if (keyboard.IsPressed(Key.ESCAPE)) _close = true;

            var hud = _services.GetService<HUDController>();
            hud.Update(gameService);
        }

        public void Render()
        {
            var window = _services.GetService<NativeWindow>();
            var canvas = _services.GetService<SKCanvas>();
            if (_close)
            {
                window.Close();
                return;
            }

            var gameService = _services.GetService<IGameRenderService>();

            var utility = _services.GetService<IUtilityService>();
            canvas.Clear(utility.ColorToSkia(Colors.Blue));

            var hud = _services.GetService<HUDController>();
            hud.Render(gameService);

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
                case Keys.W:
                case Keys.Space:
                    return Key.UP;
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
