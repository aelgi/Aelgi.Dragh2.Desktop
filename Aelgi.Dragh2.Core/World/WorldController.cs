using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using Aelgi.Dragh2.Core.World.Generators;
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

        protected List<Position> GetChunksPositionsOnScreen(Position gamePosition, Position windowSize)
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

        protected List<Chunk> _selectedChunks;

        public void Update(IGameUpdateService gameService)
        {
            var positions = GetChunksPositionsOnScreen(gameService.GamePosition, gameService.WindowSize);
            _selectedChunks = positions.Select(x => GetChunk(x)).ToList();

            foreach (var chunk in _selectedChunks) chunk.Update(gameService);
        }

        public void Render(IGameRenderService gameService)
        {
            foreach (var chunk in _selectedChunks) chunk.Render(gameService);
        }
    }
}
