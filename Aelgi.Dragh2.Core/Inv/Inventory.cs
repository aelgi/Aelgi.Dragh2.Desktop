using Aelgi.Dragh2.Core.Inv.Items;

namespace Aelgi.Dragh2.Core.Inv
{
    public class Inventory
    {
        protected static int _topRowMaxItems => 5;
        protected InventoryItem[] _topRow;
        protected HandItem _hand;
        protected int _activeIndex = -1;

        public Inventory()
        {
            _topRow = new InventoryItem[_topRowMaxItems];
            _hand = new HandItem();
        }

        public int MaxInventoryItems => _topRowMaxItems;
        public InventoryItem GetItem(int pos)
        {
            if (pos == -1) return _hand;
            else return _topRow[pos];
        }

        public void NextItem()
        {
            _activeIndex++;
            if (_activeIndex >= _topRowMaxItems) _activeIndex = -1;
            if (GetItem(_activeIndex) == null) NextItem();
        }

        public void PreviousItem()
        {
            _activeIndex--;
            if (_activeIndex < -1) _activeIndex = _topRowMaxItems - 1;
            if (GetItem(_activeIndex) == null) PreviousItem();
        }

        public InventoryItem GetActiveItem()
        {
            return GetItem(_activeIndex);
        }

        public bool AddItem(InventoryItem newItem)
        {
            for (var i = 0; i < _topRowMaxItems; i++)
            {
                if (_topRow[i] == null)
                {
                    _topRow[i] = newItem;
                    break;
                }

                if (_topRow[i].Name == newItem.Name)
                {
                    var leftOver = _topRow[i].Add(newItem.Count);
                    if (leftOver == 0) return true;
                    newItem.Subtract(newItem.Count - leftOver);
                }
            }

            return false;
        }
    }
}
