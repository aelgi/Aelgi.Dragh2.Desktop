using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.HUD;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.World;
using Aelgi.Dragh2.Core.World.Generators;
using Aelgi.Dragh2.Services;
using GLFW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using SkiaSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
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
            var utilityService = new UtilityService();
            services.AddSingleton<IUtilityService>(utilityService);
            services.AddSingleton<IStatsService, StatsService>();
            _keyboard = new KeyboardService();
            services.AddSingleton<IKeyboardService>(_keyboard);
            services.AddSingleton<IGameUpdateService, GameUpdateService>();

            services.AddSingleton<HUDController>();
            services.AddSingleton<IWorldController>(new WorldController(new WorldGenerator()));

            return services.BuildServiceProvider().CreateScope().ServiceProvider;
        }

        public void Startup()
        {
            var services = new ServiceCollection();

            _services = RegisterServices(services);

            _services.GetService<IWorldController>().LoadChunks();

            var gameUpdate = _services.GetService<IGameUpdateService>();
            gameUpdate.GamePosition = new Core.Models.Position(400, 300);
        }

        public void Run()
        {
            using (var window = new NativeWindow(800, 600, "Dragh 2.0"))
            {
                window.SizeChanged += SizeChanged;
                window.MouseMoved += MouseMoved;
                window.KeyAction += KeyAction;

                using (var context = GenerateSkiaContext(window))
                using (var skiaSurface = GenerateSkiaSurface(context, window.ClientSize))
                {
                    var canvas = skiaSurface.Canvas;

                    while (!window.IsClosing)
                    {
                        Update(window);
                        Render(window, canvas);
                        Glfw.PollEvents();
                    }
                }
            }
        }

        protected void Update(NativeWindow window)
        {
            _framesTimer.Stop();
            var ts = _framesTimer.ElapsedMilliseconds;
            _framesTimer.Restart();
            var fps = 1000 * ts / 60;

            var statsService = _services.GetService<IStatsService>();
            statsService.SetFPS((int)ts);
            var gameService = _services.GetService<IGameUpdateService>();
            gameService.WindowSize = new Core.Models.Position(800, 600);

            var keyboard = _services.GetService<IKeyboardService>();
            if (keyboard.IsPressed(Key.ESCAPE)) _close = true;

            var world = _services.GetService<IWorldController>();
            world.Update(gameService);

            var hud = _services.GetService<HUDController>();
            hud.Update(gameService);
        }

        protected void Render(NativeWindow window, SKCanvas canvas)
        {
            var utility = _services.GetService<IUtilityService>();
            canvas.Clear(utility.ColorToSkia(Colors.Background));

            var gameService = new GameRenderService(canvas, utility);

            var world = _services.GetService<IWorldController>();
            world.Render(gameService);

            var hud = _services.GetService<HUDController>();
            hud.Render(gameService);

            canvas.Flush();
            window.SwapBuffers();
        }

        private static SKSurface GenerateSkiaSurface(GRContext skiaContext, Size surfaceSize)
        {
            var frameBufferInfo = new GRGlFramebufferInfo((uint)new UIntPtr(0), GRPixelConfig.Rgba8888.ToGlSizedFormat());
            var backendRenderTarget = new GRBackendRenderTarget(surfaceSize.Width, surfaceSize.Height, 0, 8, frameBufferInfo);
            return SKSurface.Create(skiaContext, backendRenderTarget, GRSurfaceOrigin.BottomLeft, SKImageInfo.PlatformColorType);
        }

        private static GRContext GenerateSkiaContext(NativeWindow window)
        {
            var nativeContext = GetNativeContext(window);
            var glInterface = GRGlInterface.AssembleGlInterface(nativeContext, (contextHandle, name) => Glfw.GetProcAddress(name));
            return GRContext.Create(GRBackend.OpenGL, glInterface);
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

        private void KeyAction(object sender, KeyEventArgs e)
        {
            var key = ConvertKeyInput(e.Key);
            if (key == Key.NONE) return;

            switch(e.State)
            {
                case InputState.Press:
                case InputState.Repeat:
                    _keyboard.SetKey(key, true);
                    break;
                case InputState.Release:
                    _keyboard.SetKey(key, false);
                    break;
            }
        }

        private void SizeChanged(object sender, SizeChangeEventArgs e)
        {
            // We dont need to do anything at the moment because the main render loop re-renders
        }
    }
}
