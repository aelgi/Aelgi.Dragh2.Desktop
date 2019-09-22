using Aelgi.Dragh2.Core.Models;
using Aelgi.Dragh2.Core.World.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public class MineGenerator : IGenerator
    {
        public Chunk GenerateChunk(Position gamePosition)
        {
            var chunk = new Chunk(gamePosition);

            for (var x = 0; x < Chunk.ChunkWidth; x++)
                for (var y = 0; y < Chunk.ChunkHeight; y++)
                    chunk.SetBlock(new Position(x, y), new Dirt());

            return chunk;
        }
    }
}
