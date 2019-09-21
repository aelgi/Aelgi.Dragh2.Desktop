using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.HUD
{
    public class HUDController : IDrawable
    {
        protected FPSCounter _counter = new FPSCounter();
        protected DebugKeys _debugKeys = new DebugKeys();

        public void Update(IGameUpdateService gameService)
        {
            _counter.Update(gameService);
            _debugKeys.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            _counter.Render(gameService);
            _debugKeys.Render(gameService);
        }
    }
}
