using Aelgi.Dragh2.Core.Models;
using System;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public class WorldGenerator : IGenerator
    {
        protected IGenerator _plainsGenerator = new PlainsGenerator();
        protected IGenerator _minesGenerator = new MineGenerator();
        public static int SurfaceHeight => 300;

        public bool IsInRange(int val, int test, int deviation)
        {
            if (val <= (test + deviation) && val >= (test - deviation)) return true;
            return false;
        }

        public Chunk GenerateChunk(Position gamePosition)
        {
            if (IsInRange(gamePosition.Y, SurfaceHeight, Chunk.ChunkHeight * Block.BlockSize))
            {
                Console.WriteLine($"Creating Plains Chunk: {gamePosition}");
                return _plainsGenerator.GenerateChunk(gamePosition);
            }

            Console.WriteLine($"Creating Mines Chunk: {gamePosition}");
            return _minesGenerator.GenerateChunk(gamePosition);
        }
    }
}
