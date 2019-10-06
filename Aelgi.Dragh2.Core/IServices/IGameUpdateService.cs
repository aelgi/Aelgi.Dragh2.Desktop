using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IGameUpdateService
    {
        IWorldController WorldController { get; }

        Position WindowSize { get; set; }
        Position GamePosition { get; set; }

        bool IsPressed(Key key);

        int GetFPS();
    }
}
