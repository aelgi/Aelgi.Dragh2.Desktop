using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IModels
{
    public interface IDrawable
    {
        void Update(IGameUpdateService gameService);
        void Render(IGameRenderService gameService);
    }
}
