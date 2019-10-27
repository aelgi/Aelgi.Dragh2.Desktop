using Aelgi.Dragh2.Core.Inv.Items;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.World.Blocks
{
    public class TreeLeaf : Block
    {
        public override int MaxHealth => 50;
        protected override ICollection<InventoryItem> _dropsOnDeath => new List<InventoryItem>();
        public override bool IsCollidable => false;

        public override void Update(IGameUpdateService gameService)
        {
            base.Update(gameService);

            var realPosition = ChunkPosition + Position;

            var isConnected = false;

            for (var x = -2; x <= 2; x++)
                for (var y = -2; y <= 2; y++)
                {
                    var b = gameService.WorldController.GetBlock(realPosition.Add(x, y));
                    if (b != null)
                    {
                        // DEbug here
                    }
                    if (b != null && b is Tree)
                    {
                        isConnected = true;
                        break;
                    }
                }

            if (isConnected)
            {
                _health += 2;
                if (_health > MaxHealth) _health = MaxHealth;
            }
            else
            {
                _health -= (new Random()).Next(0, 5);
            }
        }

        public override void Render(IGameRenderService gameService)
        {
            DrawImage(gameService, "Blocks/TreeLeaf");
        }
    }
}
