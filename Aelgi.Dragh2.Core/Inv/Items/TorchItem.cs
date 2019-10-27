using Aelgi.Dragh2.Core.World;

namespace Aelgi.Dragh2.Core.Inv.Items
{
    public class TorchItem : InventoryItem
    {
        public override string Name => "Torch";
        public override string IconPath => "Inventory/Torch";

        public override int MaxStackSize => 3;
        public override int DamageAmount(Block block)
        {
            return 2;
        }
    }
}
