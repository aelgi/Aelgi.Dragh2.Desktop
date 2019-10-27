namespace Aelgi.Dragh2.Core.Inv.Items
{
    public class DirtItem : InventoryItem
    {
        public override string Name => "Dirt";
        public override string IconPath => "Blocks/BaseDirt";

        public override int MaxStackSize => 12;
    }
}
