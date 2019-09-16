using Aelgi.Dragh2.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface ITextService
    {
        void DrawToScreen(uint x, uint y, uint size, Colors color, string text);
        void DrawToScreen(uint x, uint y, uint size, string text);
    }
}
