using Aelgi.Dragh2.Core.Inv.Items;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.Inv
{
    public class Inventory
    {
        protected static int _topRowMaxItems => 5;
        protected List<InventoryItem> _topRow;

        public Inventory()
        {
            _topRow = new List<InventoryItem>();
        }

        public ICollection<InventoryItem> GetTopRow() => _topRow;

        public bool AddItem(InventoryItem newItem)
        {
            foreach (var item in _topRow)
            {
                if (item.Name == newItem.Name)
                {
                    var leftOver = item.Add(newItem.Count);
                    if (leftOver == 0) return true;
                    newItem.Subtract(newItem.Count - leftOver);
                }
            }

            if (_topRow.Count < _topRowMaxItems)
            {
                _topRow.Add(newItem);
            }

            return false;
        }
    }
}
