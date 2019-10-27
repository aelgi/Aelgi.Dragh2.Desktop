using Aelgi.Dragh2.Core.Models;
using Aelgi.Dragh2.Core.World.Blocks;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public class MineGenerator : IGenerator
    {
        public string GeneratorName => "Mine";

        public Chunk GenerateChunk(Position gamePosition)
        {
            var chunk = new Chunk(gamePosition);

            for (var x = 0; x < Chunk.ChunkWidth; x += Block.BlockSize)
                for (var y = 0; y < Chunk.ChunkHeight; y += Block.BlockSize)
                    chunk.SetBlock(new Position(x, y), new Dirt());

            return chunk;
        }
    }
}
