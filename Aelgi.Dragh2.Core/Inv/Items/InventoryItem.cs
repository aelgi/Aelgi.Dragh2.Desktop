using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.Inv.Items
{
    public abstract class InventoryItem
    {
        public abstract string Name { get; }

        public abstract int MaxStackSize { get; }
        protected int _stackSize;

        public abstract string IconPath { get; }

        public InventoryItem(int stackSize)
        {
            _stackSize = stackSize;
        }

        public InventoryItem() : this(1) { }

        public int Add(int count)
        {
            _stackSize += count;
            if (_stackSize >= MaxStackSize)
            {
                var overSize = _stackSize - MaxStackSize;
                _stackSize = MaxStackSize;
                return overSize;
            }
            return 0;
        }

        public void Subtract(int count)
        {
            _stackSize -= count;
            if (_stackSize < 0) _stackSize = 0;
        }

        public int Count => _stackSize;

        public void Draw(IGameRenderService gameService, Position pos)
        {
            gameService.DrawImage(pos, IconPath);
            gameService.DrawToScreen(pos.Add(0.9, 0.9), 12, Enums.Colors.White, _stackSize.ToString(), true);
        }
    }
}
