using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IGameRenderService
    {
        ITextService Text { get; }

        int GetWindowWidth();
        int GetWindowHeight();
    }
}
