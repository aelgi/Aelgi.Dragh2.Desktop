using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.HUD
{
    public class FPSCounter : IDrawable
    {
        public int FPS { get; set; }

        protected Position _position;

        public void Update(IGameUpdateService gameService)
        {
            FPS = gameService.GetFPS();
            _position = (gameService.WindowSize / 2) - new Position(1, 1);
            _position.Y *= -1;
        }

        public void Render(IGameRenderService gameService)
        {
            gameService.DrawToScreen(_position, 18, $"{FPS}", true);
        }
    }
}
