using Aelgi.Dragh2.Core.Entities;
using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.HUD;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
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
        public int InitialGameWidth => 1024;
        public int InitialGameHeight => 800;

        public int GameToWindowSize(double size) => (int)Math.Ceiling(size * Block.BlockSize);

        protected IServiceProvider _services;
        protected GameUpdateService _gameUpdateService;
        protected KeyboardService _keyboard;
        protected Stopwatch _framesTimer = new Stopwatch();

        protected IServiceProvider RegisterServices(IServiceCollection services)
        {
            var utilityService = new UtilityService();
            services.AddSingleton<IUtilityService>(utilityService);
            services.AddSingleton<IStatsService, StatsService>();
            _keyboard = new KeyboardService();
            services.AddSingleton<IKeyboardService>(_keyboard);
            services.AddSingleton<IWorldController>(new WorldController(new WorldGenerator()));
            services.AddSingleton<GameUpdateService>();
            services.AddSingleton<IGameUpdateService>(provider => provider.GetRequiredService<GameUpdateService>());

            services.AddSingleton<EntityController>();

            services.AddSingleton<HUDController>();

            return services.BuildServiceProvider().CreateScope().ServiceProvider;
        }

        public void Startup()
        {
            var services = new ServiceCollection();

            _services = RegisterServices(services);
            _gameUpdateService = _services.GetRequiredService<GameUpdateService>();

            _services.GetService<IWorldController>().LoadChunks();

            var gameUpdate = _services.GetService<IGameUpdateService>();
            gameUpdate.GamePosition = new Core.Models.Position(0, Chunk.AverageHeight);
        }

        public void Run()
        {
            using (var window = new NativeWindow(InitialGameWidth, InitialGameHeight, "Dragh 2.0"))
            {
                window.SizeChanged += SizeChanged;
                window.MouseMoved += MouseMoved;
                window.KeyAction += KeyAction;

                using (var context = GenerateSkiaContext(window))
                using (var skiaSurface = GenerateSkiaSurface(context, window.ClientSize))
                {
                    var canvas = skiaSurface.Canvas;

                    var utility = _services.GetRequiredService<IUtilityService>();
                    var statsService = _services.GetRequiredService<IStatsService>();
                    var gameUpdateService = _services.GetRequiredService<IGameUpdateService>();
                    gameUpdateService.WindowSize = new Position(window.Size.Width / Block.BlockSize, window.Size.Height / Block.BlockSize);
                    var keyboard = _services.GetRequiredService<IKeyboardService>();
                    var world = _services.GetRequiredService<IWorldController>();
                    var hud = _services.GetRequiredService<HUDController>();
                    var entities = _services.GetRequiredService<EntityController>();

                    while (!window.IsClosing)
                    {
                        //canvas.ClipRect(new SKRect(0, 0, GameToWindowSize(gameUpdateService.WindowSize.X), GameToWindowSize(gameUpdateService.WindowSize.Y)), SKClipOperation.Intersect, true);
                        Update(window, statsService, gameUpdateService, keyboard, world, hud, entities);
                        Render(window, canvas, utility, world, hud, entities, gameUpdateService.WindowSize);
                        Glfw.PollEvents();

                        if (keyboard.IsPressed(Key.ESCAPE)) break;
                    }
                }
            }
        }

        protected void Update(NativeWindow window, IStatsService statsService, IGameUpdateService gameService, IKeyboardService keyboard, IWorldController world, HUDController hud, EntityController entities)
        {
            _framesTimer.Stop();
            var ts = _framesTimer.ElapsedMilliseconds;
            _framesTimer.Restart();
            var fps = 1000 * ts / 60;

            statsService.SetFPS((int)fps);

            world.Update(gameService);
            hud.Update(gameService);
            entities.Update(gameService);
        }

        protected void Render(NativeWindow window, SKCanvas canvas, IUtilityService utility, IWorldController world, HUDController hud, EntityController entities, Position windowSize)
        {
            canvas.Clear(utility.ColorToSkia(Colors.Background));

            var gameService = new GameRenderService(canvas, utility, windowSize);

            world.Render(gameService);
            hud.Render(gameService);
            entities.Render(gameService);

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
                    return Key.UP;
                case Keys.S:
                    return Key.DOWN;

                case Keys.E:
                case Keys.Space:
                    return Key.USE;

                default: return Key.NONE;
            }
        }

        private void KeyAction(object sender, KeyEventArgs e)
        {
            var key = ConvertKeyInput(e.Key);
            if (key == Key.NONE) return;

            switch (e.State)
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
            _gameUpdateService.WindowSize.X = (double)e.Size.Width / Block.BlockSize;
            _gameUpdateService.WindowSize.Y = (double)e.Size.Height / Block.BlockSize;
        }
    }
}
