using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.World
{
    public abstract class Block : IDrawable
    {
        public Position Position { get; set; }
        public Position WorldPosition { get; set; }

        protected Position _positionOnScreen;

        public static int BlockSize => 32;

        public void DrawImage(IGameRenderService gameService, string imageName)
        {
            if (_positionOnScreen.X < 0 || _positionOnScreen.Y < 0) return;
            gameService.DrawImage(_positionOnScreen, imageName);
        }

        public abstract void Render(IGameRenderService gameService);
        public void Update(IGameUpdateService gameService)
        {
            _positionOnScreen = (WorldPosition - gameService.GamePosition) + (Position * BlockSize);
        }
    }
}
