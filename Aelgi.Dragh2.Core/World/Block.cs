using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.World
{
    public abstract class Block : IDrawable
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public int WorldPositionX { get; set; }
        public int WorldPositionY { get; set; }

        public void DrawImage(IGameRenderService gameService, string imageName)
        {
            var x = WorldPositionX - PositionX;
            var y = WorldPositionY - PositionY;
            gameService.DrawImage(x, y, imageName);
        }

        public abstract void Render(IGameRenderService gameService);
        public abstract void Update(IGameUpdateService gameService);
    }
}
