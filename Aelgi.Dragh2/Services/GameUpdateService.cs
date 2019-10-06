using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Services
{
    public class GameUpdateService : IGameUpdateService
    {
        public IKeyboardService KeyboardService { get; }
        public IStatsService StatsService { get; }
        public int _fps = 0;

        public IWorldController WorldController { get; set; }

        public Position WindowSize { get; set; }
        public Position GamePosition { get; set; }

        public GameUpdateService(IKeyboardService keyboardService, IStatsService statsService, IWorldController worldController)
        {
            KeyboardService = keyboardService;
            StatsService = statsService;
            WorldController = worldController;
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
