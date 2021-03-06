﻿using Aelgi.Dragh2.Core.Models;
using System;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public class WorldGenerator : IGenerator
    {
        public string GeneratorName => "World";

        protected IGenerator _plainsGenerator = new PlainsGenerator();
        protected IGenerator _minesGenerator = new MineGenerator();
        protected IGenerator _skyGenerator = new SkyGenerator();
        public static int SurfaceHeight => 300;

        public bool IsInRange(double val, int test, int deviation)
        {
            if (val <= (test + deviation) && val >= (test - deviation)) return true;
            return false;
        }

        public Chunk GenerateChunk(Position gamePosition)
        {
            if (IsInRange(gamePosition.Y, 0, Chunk.ChunkHeight / 2))
            {
                Console.WriteLine($"Creating Plains Chunk: {gamePosition}");
                return _plainsGenerator.GenerateChunk(gamePosition);
            }

            if (gamePosition.Y < 0)
            {
                Console.WriteLine("Creating Sky Chunk: {gamePosition}");
                return _skyGenerator.GenerateChunk(gamePosition);
            }

            Console.WriteLine($"Creating Mines Chunk: {gamePosition}");
            return _minesGenerator.GenerateChunk(gamePosition);
        }
    }
}
