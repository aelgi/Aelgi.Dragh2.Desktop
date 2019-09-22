using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public class PlainsGenerator : IGenerator
    {
        public Chunk GenerateChunk(Position gamePosition)
        {
            var chunk = new Chunk(gamePosition);

            return chunk;
        }
    }
}
