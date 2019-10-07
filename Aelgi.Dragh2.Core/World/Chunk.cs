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

        public static Position RoundToBlock(Position pos)
        {
            return new Position(Math.Floor(pos.X), Math.Floor(pos.Y));
        }

        public Block GetBlock(Position pos)
        {
            var relativePosition = RoundToBlock(pos - _chunkOrigin);
            if (_blocks.ContainsKey(relativePosition)) return _blocks[relativePosition];
            return null;
        }

        public void SetBlock(Position pos, Block block)
        {
            if (pos < new Position(0, 0)) throw new ArgumentOutOfRangeException();
            if (pos >= new Position(ChunkWidth, ChunkHeight)) throw new ArgumentOutOfRangeException();

            var roundedPos = RoundToBlock(pos);
            block.Position = roundedPos;
            block.ChunkPosition = _chunkOrigin.Clone();

            _blocks[roundedPos] = block;
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
