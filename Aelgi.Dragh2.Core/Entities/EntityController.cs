using System;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;

namespace Aelgi.Dragh2.Core.Entities
{
    public class EntityController : IDrawable
    {
        public Player Player { get; set; }

        public EntityController()
        {
            Player = new Player();
        }

        public void Update(IGameUpdateService gameService)
        {
            Player.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            Player.Render(gameService);
        }
    }
}
