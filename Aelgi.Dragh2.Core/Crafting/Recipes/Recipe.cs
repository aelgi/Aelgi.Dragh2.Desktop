using Aelgi.Dragh2.Core.Inv.Items;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.Crafting.Recipes
{
    public abstract class Recipe
    {
        public abstract List<InventoryItem> RequiredItems { get; }
        public abstract List<InventoryItem> CreatedItem { get; }

        public virtual bool CanBeCrafted(List<InventoryItem> items)
        {
            return true;
        }
    }
}
