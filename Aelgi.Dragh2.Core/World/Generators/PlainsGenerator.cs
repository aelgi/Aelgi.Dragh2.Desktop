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

            for (var x = 0; x < Chunk.ChunkWidth; x++)
            {
                chunk.SetBlock(new Position(x, 19), new TopDirt());
            }

            //var lastX = Chunk.AverageHeight;
            //for (var x = 0; x < Chunk.ChunkHeight; x++)
            //{
            //    lastX += rnd.Next(-3, 3);
            //    lastX = CapChunkHeight(lastX);
            //    Console.WriteLine($"X: {lastX}");
            //    chunk.SetBlock(new Position(x, lastX), new Dirt());
            //    for (var y = 0; y < (lastX + 1); y++)
            //    {
            //        //chunk.SetBlock(new Position(x, y), new Dirt());
            //    }
            //}

            return chunk;
        }
    }
}
