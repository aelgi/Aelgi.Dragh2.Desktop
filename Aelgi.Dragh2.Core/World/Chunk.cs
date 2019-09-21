using System;
using System.Collections.Generic;
using System.Text;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.World.Blocks;

namespace Aelgi.Dragh2.Core.World
{
    public class Chunk : IDrawable
    {
        private Block _testBlock = new Dirt();

        public void Update(IGameUpdateService gameService)
        {
            _testBlock.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            _testBlock.Render(gameService);
        }
    }
}
