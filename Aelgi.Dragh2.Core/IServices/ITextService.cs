using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface ITextService
    {
        void DrawToScreen(uint x, uint y, uint size, string text);
    }
}
