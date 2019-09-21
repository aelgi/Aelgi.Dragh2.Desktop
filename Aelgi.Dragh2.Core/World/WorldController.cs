using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.World
{
    public class WorldController : IWorldController
    {
        private Chunk _testChunk = new Chunk();

        public void LoadChunks() { }

        public void Update(IGameUpdateService gameService)
        {
            _testChunk.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            _testChunk.Render(gameService);
        }
    }
}
