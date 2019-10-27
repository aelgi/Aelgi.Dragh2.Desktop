using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using Aelgi.Dragh2.Core.World.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aelgi.Dragh2.Core.World
{
    public class WorldController : IWorldController
    {
        protected Dictionary<Position, Chunk> _loadedChunks;
        public IGenerator ChunkGenerator { get; }

        public WorldController(IGenerator chunkGenerator)
        {
            ChunkGenerator = chunkGenerator;
        }

        public void LoadChunks()
        {
            _loadedChunks = new Dictionary<Position, Chunk>();
        }

        protected Position RoundToChunk(Position pos)
        {
            var x = Math.Floor(pos.X / Chunk.ChunkWidth) * Chunk.ChunkWidth;
            var y = Math.Floor(pos.Y / Chunk.ChunkHeight) * Chunk.ChunkHeight;
            return new Position(x, y);
        }

        protected List<Position> GetChunksPositionsOnScreen(Position gamePosition)
        {
            var positions = new List<Position>();

            var dev = 1;

            var topLeft = gamePosition - new Position(Chunk.ChunkWidth * dev, Chunk.ChunkHeight * dev);
            var bottomRight = gamePosition + new Position(Chunk.ChunkWidth * dev, Chunk.ChunkHeight * dev);

            var steps = Math.Min(Chunk.ChunkWidth, Chunk.ChunkHeight) / 2;

            for (var i = topLeft.X; i <= bottomRight.X; i += steps)
                for (var j = topLeft.Y; j <= bottomRight.Y; j += steps)
                {
                    var pos = RoundToChunk(new Position(i, j));
                    if (positions.Contains(pos)) continue;
                    positions.Add(pos);
                }

            return positions;
        }

        protected Chunk GetChunkExact(Position pos)
        {
            if (!_loadedChunks.ContainsKey(pos))
            {
                var chunk = ChunkGenerator.GenerateChunk(pos);
                _loadedChunks[pos] = chunk;
            }
            return _loadedChunks[pos];
        }

        public bool IsGrounded(Position pos)
        {
            var chunkPos = RoundToChunk(pos);
            var chunk = GetChunkExact(chunkPos);

            var block = chunk.GetBlock(pos);
            if (block != null) return block.IsCollidable;
            return false;
        }

        public Chunk GetChunk(Position pos)
        {
            return GetChunkExact(RoundToChunk(pos));
        }

        public Block GetBlock(Position pos)
        {
            var chunk = GetChunkExact(RoundToChunk(pos));
            return chunk.GetBlock(pos);
        }

        protected List<Chunk> _selectedChunks;

        public void Update(IGameUpdateService gameService)
        {
            var positions = GetChunksPositionsOnScreen(gameService.GamePosition);
            _selectedChunks = positions.Select(x => GetChunkExact(x)).ToList();

            foreach (var chunk in _selectedChunks) chunk.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            foreach (var chunk in _selectedChunks) chunk.Render(gameService);
        }
    }
}
