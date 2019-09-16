using Aelgi.Dragh2.Core.Enums;
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
        public IUtilityService UtilityService { get; }

        public TextService(SKCanvas canvas, IUtilityService utilityService)
        {
            Canvas = canvas;
            UtilityService = utilityService;
        }

        public void DrawToScreen(uint x, uint y, uint size, Colors color, string text)
        {
            var convertedColor = UtilityService.ColorToSkia(color);
            var paint = new SKPaint
            {
                Color = convertedColor,
                TextSize = size,
                IsAntialias = true,
            };
            Canvas.DrawText(text, x, y, paint);
        }

        public void DrawToScreen(uint x, uint y, uint size, string text)
        {
            DrawToScreen(x, y, size, Colors.Black, text);
        }
    }
}
