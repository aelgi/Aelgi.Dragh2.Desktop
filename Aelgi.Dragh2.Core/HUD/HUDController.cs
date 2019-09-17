using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.HUD
{
    public class HUDController : Drawable
    {
        public HUDController() : base()
        {
            Children.Add(new DebugKeys());
            Children.Add(new FPSCounter());
        }
    }
}
