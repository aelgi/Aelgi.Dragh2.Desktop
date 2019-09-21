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
        public IUtilityService UtilityService { get; }
        protected SKCanvas _canvas;

        public TextService(SKCanvas canvas, IUtilityService utilityService)
        {
            _canvas = canvas;
            UtilityService = utilityService;
        }

        public void DrawToScreen(uint x, uint y, uint size, Colors color, string text, bool rightAlign = false)
        {
            var convertedColor = UtilityService.ColorToSkia(color);
            var paint = new SKPaint
            {
                Color = convertedColor,
                TextSize = size,
                IsAntialias = true,
                TextAlign = rightAlign ? SKTextAlign.Right : SKTextAlign.Left,
            };
            _canvas.DrawText(text, x, y, paint);
        }

        public void DrawToScreen(uint x, uint y, uint size, string text, bool rightAlign = false)
        {
            DrawToScreen(x, y, size, Colors.Black, text, rightAlign);
        }
    }
}
