using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.HUD
{
    public class FPSCounter : Drawable
    {
        public int FPS { get; set; }

        public override void Update(IGameUpdateService gameService)
        {
            FPS = gameService.GetFPS();

            base.Update(gameService);
        }

        public override void Render(IGameRenderService gameService)
        {
            var width = (uint)gameService.GetWindowWidth();
            gameService.Text.DrawToScreen(width - 10, 28, 18, $"{FPS}", true);

            base.Render(gameService);
        }
    }
}
