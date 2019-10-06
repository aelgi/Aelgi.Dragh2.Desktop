using Aelgi.Dragh2.Core.Models;
using Aelgi.Dragh2.Core.World.Blocks;
using System;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public class PlainsGenerator : IGenerator
    {
        public int CapRange(int currentVal, int minVal, int maxVal)
        {
            if (currentVal < minVal) return minVal;
            if (currentVal > maxVal) return maxVal;
            return currentVal;
        }

        public int CapChunkHeight(int currentVal)
        {
            var lineRange = 5;
            return CapRange(currentVal, Chunk.AverageHeight - lineRange, Chunk.AverageHeight + lineRange);
        }

        public Chunk GenerateChunk(Position gamePosition)
        {
            var rnd = new Random();

            var chunk = new Chunk(gamePosition);

            var lastX = Chunk.AverageHeight;
            for (var x = 0; x < Chunk.ChunkWidth; x++)
            {
                lastX += rnd.Next(-1, 2);
                lastX = CapChunkHeight(lastX);
                chunk.SetBlock(new Position(x, lastX), new TopDirt());
                for (var y = Chunk.ChunkHeight; y > lastX; y--)
                {
                    chunk.SetBlock(new Position(x, y), new Dirt());
                }
            }

            return chunk;
        }
    }
}
