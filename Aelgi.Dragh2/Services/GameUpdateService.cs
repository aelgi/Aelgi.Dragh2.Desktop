using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class GameUpdateService : IGameUpdateService
    {
        public IKeyboardService KeyboardService { get; }
        public IStatsService StatsService { get; }
        public int _fps = 0;

        public Position WindowSize { get; set; }
        public Position GamePosition { get; set; }

        public GameUpdateService(IKeyboardService keyboardService, IStatsService statsService)
        {
            KeyboardService = keyboardService;
            StatsService = statsService;
        }

        public bool IsPressed(Key key)
        {
            return KeyboardService.IsPressed(key);
        }

        public int GetFPS()
        {
            return StatsService.GetFPS();
        }
    }
}
