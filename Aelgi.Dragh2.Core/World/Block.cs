using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.World
{
    public abstract class Block : IDrawable
    {
        public Position Position { get; set; }
        public Position ChunkPosition { get; set; }

        protected Position _positionOnScreen;

        public static int BlockSize => 32;

        public void DrawImage(IGameRenderService gameService, string imageName)
        {
            gameService.DrawImage(_positionOnScreen, imageName);
        }

        public abstract void Render(IGameRenderService gameService);
        public void Update(IGameUpdateService gameService)
        {
            var realPosition = ChunkPosition + Position;
            _positionOnScreen = realPosition - gameService.GamePosition;
            //_positionOnScreen = (ChunkPosition - gameService.GamePosition) + Position;
            //_positionOnScreen = new Position(0, 0);
        }
    }
}
