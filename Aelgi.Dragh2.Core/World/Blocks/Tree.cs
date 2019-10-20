using System;
using System.Collections.Generic;
using Aelgi.Dragh2.Core.Inv.Items;
using Aelgi.Dragh2.Core.IServices;

namespace Aelgi.Dragh2.Core.World.Blocks
{
    public class Tree : Block
    {
        public override int MaxHealth => 500;
        protected override ICollection<InventoryItem> _dropsOnDeath => new List<InventoryItem>();
        public override bool IsCollidable => false;

        public override ICollection<InventoryItem> OnHit(IWorldController worldController)
        {
            var items = new List<InventoryItem>();
            _health -= 10;

            var blockAbove = ChunkPosition + Position - new Models.Position(0, 1);
            var bAbove = worldController.GetBlock(blockAbove);
            if (bAbove != null && bAbove is Tree)
            {
                items.AddRange(bAbove.OnHit(worldController));
            }

            if (_health <= 0) items.AddRange(_dropsOnDeath);
            return items;
        }

        public override void Render(IGameRenderService gameService)
        {
            DrawImage(gameService, "TreeBase");
        }
    }
}
