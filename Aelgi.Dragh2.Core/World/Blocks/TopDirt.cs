using Aelgi.Dragh2.Core.IServices;

namespace Aelgi.Dragh2.Core.World.Blocks
{
    public class TopDirt : Block
    {
        public override int MaxHealth => 300;

        public override void Render(IGameRenderService gameService)
        {
            DrawImage(gameService, "TopDirt");
        }
    }
}
