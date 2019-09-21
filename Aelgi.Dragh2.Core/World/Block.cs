﻿using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.World
{
    public abstract class Block : IDrawable
    {
        public abstract void Render(IGameRenderService gameService);
        public abstract void Update(IGameUpdateService gameService);
    }
}
