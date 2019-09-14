using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Aelgi.Dragh2
{
    public class Dragh
    {
        protected GraphicsDevice _graphicsDevice;
        protected Sdl2Window _window;
        protected readonly Dictionary<Type, BinaryAssetSerializer>

        protected IServiceProvider RegisterServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        protected void CreateResources()
        {
            var factory = _graphicsDevice.ResourceFactory;
        }

        protected T LoadEmbeddedAsset<T>(string name)
        {

        }

        public void Startup()
        {
            var services = new ServiceCollection();

            var windowCI = new WindowCreateInfo()
            {
                X = 100,
                Y = 100,
                WindowWidth = 960,
                WindowHeight = 540,
                WindowTitle = "Dragh2",
            };
            _window = VeldridStartup.CreateWindow(ref windowCI);
            _graphicsDevice = VeldridStartup.CreateGraphicsDevice(_window);

            var provider = RegisterServices(services);
        }

        public void Run()
        {
            while (_window.Exists)
            {
                _window.PumpEvents();
            }
        }
    }
}
