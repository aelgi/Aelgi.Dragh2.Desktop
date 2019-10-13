using System;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;

namespace Aelgi.Dragh2.Core.Entities
{
    public class EntityController : IDrawable
    {
        protected Player _player;

        public EntityController()
        {
            _player = new Player();
        }

        public Player FindPlayer()
        {
            return _player;
        }

        public void Update(IGameUpdateService gameService)
        {
            _player.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            _player.Render(gameService);
        }
    }
}
