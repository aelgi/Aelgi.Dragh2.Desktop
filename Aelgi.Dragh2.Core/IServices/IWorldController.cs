using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.Models;
using Aelgi.Dragh2.Core.World;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IWorldController : IDrawable
    {
        void LoadChunks();

        bool IsGrounded(Position pos);
        Block GetBlock(Position pos);
        Chunk GetChunk(Position pos);
    }
}
