using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;

namespace Aelgi.Dragh2.Core.Models
{
    public class KeyboardDeBounce
    {
        protected bool _lastAction;
        protected bool _wasReleased;
        protected Key _key;

        public KeyboardDeBounce(Key key)
        {
            _key = key;
        }

        public void Update(IGameUpdateService gameService)
        {
            var action = gameService.IsPressed(_key);
            _wasReleased = _lastAction == true && action == false;
            _lastAction = action;
        }

        public bool WasReleased() => _wasReleased;
    }
}
