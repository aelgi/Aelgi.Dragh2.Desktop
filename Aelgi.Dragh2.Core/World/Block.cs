using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.World
{
    public abstract class Block : IDrawable
    {
        public Position Position { get; set; }
        public Position WorldPosition { get; set; }

        public static int BlockSize => 32;

        public void DrawImage(IGameRenderService gameService, string imageName)
        {
            var pos = WorldPosition + (Position * BlockSize);
            gameService.DrawImage(pos, imageName);
        }

        public abstract void Render(IGameRenderService gameService);
        public abstract void Update(IGameUpdateService gameService);
    }
}
