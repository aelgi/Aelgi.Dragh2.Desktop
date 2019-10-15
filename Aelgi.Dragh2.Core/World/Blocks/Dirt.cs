using Aelgi.Dragh2.Core.Inv.Items;
using Aelgi.Dragh2.Core.IServices;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.World.Blocks
{
    public class Dirt : Block
    {
        public override int MaxHealth => 200;
        protected override ICollection<InventoryItem> _dropsOnDeath => new List<InventoryItem>
        {
            new DirtItem()
        };

        public override void Render(IGameRenderService gameService)
        {
            DrawImage(gameService, "BaseDirt");
        }
    }
}
