using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid.StartupUtilities;

namespace Aelgi.Dragh2
{
    public class Dragh
    {
        protected IServiceProvider RegisterServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
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
            var window = VeldridStartup.CreateWindow(ref windowCI);

            var provider = RegisterServices(services);
        }
    }
}
