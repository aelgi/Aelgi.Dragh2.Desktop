using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public class WorldGenerator : IGenerator
    {
        protected IGenerator _plainsGenerator = new PlainsGenerator();
        protected IGenerator _minesGenerator = new MineGenerator();

        public Chunk GenerateChunk(Position gamePosition)
        {
            if (gamePosition.Y >= Chunk.AverageHeight)
            {
                Console.WriteLine($"Creating Mines Chunk: {gamePosition}");
                return _minesGenerator.GenerateChunk(gamePosition);
            }

            Console.WriteLine($"Creating Plains Chunk: {gamePosition}");
            return _plainsGenerator.GenerateChunk(gamePosition);
        }
    }
}
