using Aelgi.Dragh2.Core.IModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IWorldController : IDrawable
    {
        void LoadChunks();
    }
}
