using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using Aelgi.Dragh2.Core.World;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace Aelgi.Dragh2.Services
{
    public struct SkiaPositioning
    {
        public int X;
        public int Y;
    }

    public class GameRenderService : IGameRenderService
    {
        protected SKCanvas _canvas;
        protected Dictionary<string, SKBitmap> _imageCache;
        public IUtilityService UtilityService { get; set; }
        protected Position _windowSize;
        protected Position _center;

        protected SkiaPositioning GameToSkia(Position pos)
        {
            var final = (_center + pos) * Block.BlockSize;
            return new SkiaPositioning
            {
                X = (int)final.X,
                Y = (int)final.Y,
            };
        }

        public GameRenderService(SKCanvas canvas, IUtilityService utility, Position windowSize)
        {
            _canvas = canvas;
            UtilityService = utility;
            _imageCache = new Dictionary<string, SKBitmap>();
            _windowSize = windowSize;
            _center = _windowSize / 2;
        }

        private SKBitmap LoadFromCache(string imageName)
        {
            if (_imageCache.ContainsKey(imageName))
            {
                return _imageCache[imageName];
            }
            else
            {
                var fileName = Path.Combine(Environment.CurrentDirectory, "../../..", "Assets", imageName) + ".png";
                var bitmap = SKBitmap.Decode(fileName);
                _imageCache[imageName] = bitmap;
                return bitmap;
            }
        }

        public void DrawImage(Position pos, string imageName)
        {
            var bitmap = LoadFromCache(imageName);
            var dPos = GameToSkia(pos);
            _canvas.DrawBitmap(bitmap, dPos.X, dPos.Y);
        }

        public void DrawToScreen(Position pos, uint size, Colors color, string text, bool rightAlign = false)
        {
            var convertedColor = UtilityService.ColorToSkia(color);
            var paint = new SKPaint
            {
                Color = convertedColor,
                TextSize = size,
                IsAntialias = true,
                TextAlign = rightAlign ? SKTextAlign.Right : SKTextAlign.Left,
            };
            var dPos = GameToSkia(pos);
            _canvas.DrawText(text, dPos.X, dPos.Y, paint);
        }

        public void DrawToScreen(Position pos, uint size, string text, bool rightAlign = false)
        {
            DrawToScreen(pos, size, Colors.Black, text, rightAlign);
        }

        public void DrawDot(Position pos, Colors color)
        {
            var convertedColor = UtilityService.ColorToSkia(color);
            var paint = new SKPaint
            {
                Color = convertedColor,
                IsAntialias = true,
                Style = SKPaintStyle.StrokeAndFill,
                StrokeWidth = 4,
            };
            var dPos = GameToSkia(pos);
            _canvas.DrawCircle(new SKPoint(dPos.X, dPos.Y), 2f, paint);
        }

        public void DrawRect(Position topLeft, Position bottomRight, Colors color, bool overlay = false)
        {
            var convertedColor = UtilityService.ColorToSkia(color);
            if (overlay) convertedColor = convertedColor.WithAlpha(100);
            var paint = new SKPaint
            {
                Color = convertedColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
            };

            var dTopLeft = GameToSkia(topLeft);
            var dBottomRight = GameToSkia(bottomRight);
            _canvas.DrawRect(new SKRect(dTopLeft.X, dTopLeft.Y, dBottomRight.X, dBottomRight.Y), paint);
        }
    }
}
