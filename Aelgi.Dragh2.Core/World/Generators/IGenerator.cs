using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public interface IGenerator
    {
        string GeneratorName { get; }

        Chunk GenerateChunk(Position gamePosition);
    }
}
