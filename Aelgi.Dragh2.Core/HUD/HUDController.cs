using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.HUD
{
    public class HUDController
    {
        protected List<IRenderable> _items = new List<IRenderable>();

        public void RegisterItem(IRenderable item)
        {
            _items.Add(item);
        }

        public void Draw()
        {
            foreach (var x in _items)
            {
                x.Render();
            }
        }
    }
}
