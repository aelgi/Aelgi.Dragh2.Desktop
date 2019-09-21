using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IGameRenderService
    {
        ITextService Text { get; }

        void DrawImage(int x, int y, string imageName);

        int GetWindowWidth();
        int GetWindowHeight();
    }
}
