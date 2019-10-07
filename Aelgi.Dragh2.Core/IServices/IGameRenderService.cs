using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IGameRenderService
    {
        void DrawToScreen(Position pos, uint size, Colors color, string text, bool rightAlign = false);
        void DrawToScreen(Position pos, uint size, string text, bool rightAlign = false);

        void DrawImage(Position pos, string imageName);

        void DrawDot(Position pos, Colors color);

        void DrawRect(Position topLeft, Position bottomRight, Colors color, bool overlay = false);
    }
}
