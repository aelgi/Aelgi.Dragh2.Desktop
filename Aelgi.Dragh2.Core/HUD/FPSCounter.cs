using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.HUD
{
    public class FPSCounter : IDrawable
    {
        public int FPS { get; set; }

        public void Update(IGameUpdateService gameService)
        {
            FPS = gameService.GetFPS();
        }

        public void Render(IGameRenderService gameService)
        {
            var width = (uint)gameService.GetWindowWidth();
            gameService.Text.DrawToScreen(width - 10, 28, 18, $"{FPS}", true);
        }
    }
}
