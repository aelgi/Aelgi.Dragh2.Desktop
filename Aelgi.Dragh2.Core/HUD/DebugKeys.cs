using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.HUD
{
    public class DebugKeys : IRenderable
    {
        public ITextService TextService { get; }
        public IKeyboardService KeyboardService { get; }

        public DebugKeys(ITextService textService, IKeyboardService keyboardService)
        {
            TextService = textService;
            KeyboardService = keyboardService;
        }

        public void Render()
        {
            TextService.DrawToScreen(0, 0, 12, "The current keyboard readout");
        }
    }
}
