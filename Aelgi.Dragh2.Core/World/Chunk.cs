using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.World
{
    public class Chunk : IDrawable
    {
        private static int ChunkSize => 64;
        public static int ChunkWidth => ChunkSize;
        public static int ChunkHeight => ChunkSize;
        public static int AverageHeight => 32;

        protected Dictionary<Position, Block> _blocks;
        protected Position _chunkOrigin;

        public Chunk(Position chunkOrigin)
        {
            _chunkOrigin = chunkOrigin.Clone();
            _blocks = new Dictionary<Position, Block>();
        }

        public Block GetBlock(int x, int y)
        {
            if (x < 0 || x >= ChunkWidth) throw new ArgumentOutOfRangeException();
            if (y < 0 || y >= ChunkHeight) throw new ArgumentOutOfRangeException();

            return _blocks[new Position(x, y)];
        }

        public bool HasBlock(int x, int y) => GetBlock(x, y) != null;

        public void SetBlock(Position pos, Block block)
        {
            if (pos < new Position(0, 0)) throw new ArgumentOutOfRangeException();
            if (pos >= new Position(ChunkWidth, ChunkHeight)) throw new ArgumentOutOfRangeException();

            block.Position = pos.Clone();
            block.WorldPosition = _chunkOrigin.Clone();

            _blocks[pos] = block;
        }

        public void Update(IGameUpdateService gameService)
        {
            foreach (var block in _blocks)
                block.Value.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            foreach (var block in _blocks)
                block.Value.Render(gameService);
        }
    }
}
