﻿using Aelgi.Dragh2.Core.IServices;

namespace Aelgi.Dragh2.Core.World.Blocks
{
    public class Dirt : Block
    {
        public override void Render(IGameRenderService gameService)
        {
            DrawImage(gameService, "BaseDirt");
        }
    }
}