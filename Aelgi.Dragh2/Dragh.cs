using Microsoft.Extensions.DependencyInjection;
using System;
using GLFWDotNet;

namespace Aelgi.Dragh2
{
    public class Dragh
    {

        protected IServiceProvider RegisterServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        protected void CreateResources()
        {
        }

        public void Startup()
        {
            var services = new ServiceCollection();

            if (GLFW.glfwInit() == 0)
            {
                throw new NotImplementedException();
            }

            GLFW.glfwWindowHint(GLFW.GLFW_CLIENT_API, GLFW.GLFW_OPENGL_API);
            GLFW.glfwWindowHint(GLFW.GLFW_OPENGL_PROFILE, GLFW.GLFW_OPENGL_CORE_PROFILE);
            GLFW.glfwWindowHint(GLFW.GLFW_CONTEXT_VERSION_MAJOR, 4);
            GLFW.glfwWindowHint(GLFW.GLFW_CONTEXT_VERSION_MINOR, 0);

            var window = GLFW.glfwCreateWindow(640, 480, "Dragh 2.0", IntPtr.Zero, IntPtr.Zero);
            if (window == IntPtr.Zero)
            {
                GLFW.glfwTerminate();
                throw new NotImplementedException();
            }

            GLFW.glfwMakeContextCurrent(window);



            var provider = RegisterServices(services);
        }

        public void Run()
        {
        }
    }
}
