using Aelgi.Dragh2.Core.Enums;
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
            uint lineHeight = 18;
            TextService.DrawToScreen(0, lineHeight, lineHeight, "The current keyboard readout:");
            TextService.DrawToScreen(0, lineHeight * 2, lineHeight, $"Escape Key: {KeyboardService.IsPressed(Key.ESCAPE)}");
            TextService.DrawToScreen(0, lineHeight * 3, lineHeight, $"Left Key: {KeyboardService.IsPressed(Key.LEFT)}");
            TextService.DrawToScreen(0, lineHeight * 4, lineHeight, $"Right Key: {KeyboardService.IsPressed(Key.RIGHT)}");
            TextService.DrawToScreen(0, lineHeight * 5, lineHeight, $"Up Key: {KeyboardService.IsPressed(Key.UP)}");
        }
    }
}
