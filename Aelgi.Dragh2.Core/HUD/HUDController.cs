using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.HUD
{
    public class HUDController : Drawable
    {
        public new ICollection<Drawable> Children = new List<Drawable>()
        {
            new DebugKeys()
        };
    }
}
