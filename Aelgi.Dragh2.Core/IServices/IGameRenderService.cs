using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IGameRenderService
    {
        ITextService Text { get; }

        void DrawImage(Position pos, string imageName);

        int GetWindowWidth();
        int GetWindowHeight();
    }
}
