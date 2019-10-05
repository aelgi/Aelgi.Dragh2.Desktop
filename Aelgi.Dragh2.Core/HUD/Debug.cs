using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.HUD
{
    public class Debug : IDrawable
    {
        protected bool _escape = false;
        protected bool _left = false;
        protected bool _right = false;
        protected bool _up = false;
        protected Position _lastPosition;
        protected Position _drawPos;

        public void Update(IGameUpdateService gameService)
        {
            _escape = gameService.IsPressed(Key.ESCAPE);
            _left = gameService.IsPressed(Key.LEFT);
            _right = gameService.IsPressed(Key.RIGHT);
            _up = gameService.IsPressed(Key.UP);

            _drawPos = (gameService.WindowSize / 2) * -1;

            _lastPosition = gameService.GamePosition.Clone();
        }

        public void Render(IGameRenderService gameService)
        {
            uint lineHeight = 18;
            var basePosition = _drawPos.Add(0, 1);
            gameService.DrawToScreen(basePosition, lineHeight, "The current keyboard readout:");
            gameService.DrawToScreen(basePosition.Add(0, .5), lineHeight, $"Escape Key: {_escape}");
            gameService.DrawToScreen(basePosition.Add(0, 1), lineHeight, $"Left Key: {_left}");
            gameService.DrawToScreen(basePosition.Add(0, 1.5), lineHeight, $"Right Key: {_right}");
            gameService.DrawToScreen(basePosition.Add(0, 2), lineHeight, $"Up Key: {_up}");
            gameService.DrawToScreen(basePosition.Add(0, 4), lineHeight, $"Game Position: {_lastPosition}");
        }
    }
}
