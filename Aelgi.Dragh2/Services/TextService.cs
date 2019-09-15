using Aelgi.Dragh2.Core.IServices;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class TextService : ITextService
    {
        public SKCanvas Canvas { get; }

        public TextService(SKCanvas canvas)
        {
            Canvas = canvas;
        }

        public void DrawToScreen(uint x, uint y, uint size, string text)
        {
            throw new NotImplementedException();
        }
    }
}
