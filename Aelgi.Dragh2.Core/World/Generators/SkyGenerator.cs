using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public class SkyGenerator : IGenerator
    {
        public string GeneratorName => "Sky";

        public Chunk GenerateChunk(Position gamePosition)
        {
            var chunk = new Chunk(gamePosition);

            return chunk;
        }
    }
}
