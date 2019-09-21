using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.World
{
    public class WorldController : IWorldController
    {
        public void LoadChunks() { }

        public void Update(IGameUpdateService gameService) { }
        public void Render(IGameRenderService gameService) { }
    }
}
