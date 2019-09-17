using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class GameUpdateService : IGameUpdateService
    {
        public IKeyboardService KeyboardService { get; set; }

        public GameUpdateService(IKeyboardService keyboardService)
        {
            KeyboardService = keyboardService;
        }

        public bool IsPressed(Key key)
        {
            return KeyboardService.IsPressed(key);
        }
    }
}
