using Aelgi.Dragh2.Core.Inv.Items;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.Crafting.Recipes
{
    public class TorchRecipe : Recipe
    {
        public override List<InventoryItem> RequiredItems => new List<InventoryItem>
        {
            new StickItem(),
            new StickItem(),
            new StickItem(),
            new StickItem(),
            new StickItem()
        };

        public override List<InventoryItem> CreatedItem => new List<InventoryItem>
        {
            new TorchItem()
        };
    }
}
