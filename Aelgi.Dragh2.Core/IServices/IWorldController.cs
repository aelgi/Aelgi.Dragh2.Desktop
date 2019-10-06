using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IWorldController : IDrawable
    {
        void LoadChunks();

        bool IsGrounded(Position pos);
    }
}
