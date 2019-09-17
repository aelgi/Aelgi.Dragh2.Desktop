using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.HUD
{
    public class DebugKeys : Drawable
    {
        protected bool _escape = false;
        protected bool _left = false;
        protected bool _right = false;
        protected bool _up = false;

        public override void Update(IGameUpdateService gameService)
        {
            _escape = gameService.IsPressed(Key.ESCAPE);
            _left = gameService.IsPressed(Key.LEFT);
            _right = gameService.IsPressed(Key.RIGHT);
            _up = gameService.IsPressed(Key.UP);

            base.Update(gameService);
        }

        public override void Render(IGameRenderService gameService)
        {
            uint lineHeight = 18;
            gameService.Text.DrawToScreen(0, lineHeight, lineHeight, "The current keyboard readout:");
            gameService.Text.DrawToScreen(0, lineHeight * 2, lineHeight, $"Escape Key: {_escape}");
            gameService.Text.DrawToScreen(0, lineHeight * 3, lineHeight, $"Left Key: {_left}");
            gameService.Text.DrawToScreen(0, lineHeight * 4, lineHeight, $"Right Key: {_right}");
            gameService.Text.DrawToScreen(0, lineHeight * 5, lineHeight, $"Up Key: {_up}");

            base.Render(gameService);
        }
    }
}
