using Aelgi.Dragh2.Core.World;

namespace Aelgi.Dragh2.Core.Inv.Items
{
    public class Stick : InventoryItem
    {
        public override string Name => "Stick";
        public override string IconPath => "Inventory/Stick";

        public override int MaxStackSize => 24;
        public override int DamageAmount(Block block)
        {
            return 16;
        }
    }
}
