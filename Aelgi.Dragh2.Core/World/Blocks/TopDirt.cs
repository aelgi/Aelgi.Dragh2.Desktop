using Aelgi.Dragh2.Core.Inv.Items;
using Aelgi.Dragh2.Core.IServices;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.World.Blocks
{
    public class TopDirt : Block
    {
        public override int MaxHealth => 300;
        protected override ICollection<InventoryItem> _dropsOnDeath => new List<InventoryItem>
        {
            new TopDirtItem()
        };

        public override void Render(IGameRenderService gameService)
        {
            DrawImage(gameService, "Blocks/TopDirt");
        }
    }
}
