using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.World.Generators
{
    public interface IGenerator
    {
        Chunk GenerateChunk(Position gamePosition);
    }
}
