using Aelgi.Dragh2.Core.World;

namespace Aelgi.Dragh2.Core.Inv.Items
{
    public class HandItem : InventoryItem
    {
        public override string Name => "Hand";
        public override string IconPath => "Inventory/Hand";

        public override int MaxStackSize => 0;
        public override int DamageAmount(Block block)
        {
            return 10;
        }
    }
}
