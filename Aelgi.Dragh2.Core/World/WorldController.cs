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
            var minX = gamePosition.X - (gamePosition.X % Chunk.ChunkWidth) - (Chunk.ChunkWidth * dev);
            var maxX = gamePosition.X - (gamePosition.X % Chunk.ChunkWidth) + (Chunk.ChunkWidth * dev);
            var minY = gamePosition.Y - (gamePosition.Y % Chunk.ChunkHeight) - (Chunk.ChunkHeight * dev);
            var maxY = gamePosition.Y - (gamePosition.Y % Chunk.ChunkHeight) + (Chunk.ChunkHeight * dev);

            for (var i = minX; i <= maxX; i += Chunk.ChunkWidth)
                for (var j = minY; j <= maxY; j += Chunk.ChunkHeight)
                    positions.Add(new Position(i, j));

            return positions;
        }

        protected Chunk GetChunk(Position pos)
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
            //var chunkX = pos.X - (pos.X % Chunk.ChunkWidth);
            //var chunkY = pos.Y - (pos.Y % Chunk.ChunkHeight);
            //var chunkPos = new Position(chunkX, chunkY);
            var chunkPos = RoundToChunk(pos);
            var chunk = GetChunk(chunkPos);

            return chunk.GetBlock(pos) != null;
        }

        protected List<Chunk> _selectedChunks;

        public void Update(IGameUpdateService gameService)
        {
            var positions = GetChunksPositionsOnScreen(gameService.GamePosition);
            _selectedChunks = positions.Select(x => GetChunk(x)).ToList();

            foreach (var chunk in _selectedChunks) chunk.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            foreach (var chunk in _selectedChunks) chunk.Render(gameService);
        }
    }
}
