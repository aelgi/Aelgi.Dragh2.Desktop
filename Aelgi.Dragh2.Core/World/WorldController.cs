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

            var width = Chunk.ChunkWidth * Block.BlockSize;
            var height = Chunk.ChunkHeight * Block.BlockSize;

            var topLeft = gamePosition - (windowSize / 2) - new Position(width, height);
            var topLeftX = topLeft.X % Chunk.ChunkWidth;
            var topLeftY = topLeft.Y % Chunk.ChunkHeight;
            var bottomRight = gamePosition + (windowSize / 2) + new Position(width, height);
            var maxX = bottomRight.X;
            var maxY = bottomRight.Y;


            for (var i = topLeftX; i <= maxX; i += width)
                for (var j = topLeftY; j <= maxY; j += height)
                {
                    positions.Add(new Position(i, j));
                }

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
