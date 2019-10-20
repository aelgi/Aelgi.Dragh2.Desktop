using Aelgi.Dragh2.Core.Models;
using Aelgi.Dragh2.Core.World.Blocks;
using System;
using System.Collections.Generic;

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
            var lineHeights = new List<int>();

            var lastY = Chunk.AverageHeight;
            for (var x = 0; x < Chunk.ChunkWidth; x++)
            {
                lastY += rnd.Next(-1, 2);
                lastY = CapChunkHeight(lastY);
                lineHeights.Add(lastY);
                chunk.SetBlock(new Position(x, lastY), new TopDirt());
                for (var y = Chunk.ChunkHeight; y > lastY; y--)
                {
                    chunk.SetBlock(new Position(x, y), new Dirt());
                }
            }

            GenerateTrees(chunk, lineHeights);

            return chunk;
        }

        public void GenerateTrees(Chunk chunk, List<int> lineHeights)
        {
            var rnd = new Random();

            for (var i = 1; i < (Chunk.ChunkWidth - 1); i++)
            {
                if (lineHeights[i] == lineHeights[i-1] && lineHeights[i] == lineHeights[i+1])
                {
                    if (rnd.Next(1, 4) == 1) continue;
                    // Time to make a tree!
                    GenerateTree(chunk, new Position(i, lineHeights[i] + 1));
                }
            }
        }

        public void GenerateTree(Chunk chunk, Position basePosition)
        {
            var rnd = new Random();
            var height = rnd.Next(4, 8);

            for (var y = 0; y < height; y++)
            {
                chunk.SetBlock(basePosition.Add(0, -y - 2), new Tree());
            }

            PlaceLeaf(chunk, basePosition.Add(-1, -height));
            PlaceLeaf(chunk, basePosition.Add(1, -height));

            PlaceLeaf(chunk, basePosition.Add(-1, -height - 1));
            PlaceLeaf(chunk, basePosition.Add(-2, -height - 1));
            PlaceLeaf(chunk, basePosition.Add(1, -height - 1));
            PlaceLeaf(chunk, basePosition.Add(2, -height - 1));

            PlaceLeaf(chunk, basePosition.Add(-1, -height - 2));
            PlaceLeaf(chunk, basePosition.Add(0, -height - 2));
            PlaceLeaf(chunk, basePosition.Add(1, -height - 2));

            PlaceLeaf(chunk, basePosition.Add(0, -height - 3));
        }

        protected void PlaceLeaf(Chunk chunk, Position basePosition)
        {
            var originalBlock = chunk.GetBlock(basePosition);
            if (originalBlock == null)
            {
                chunk.SetBlock(basePosition, new TreeLeaf());
            }
        }
    }
}
