using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class GameRenderService : IGameRenderService
    {
        public ITextService Text { get; set; }
        protected SKCanvas _canvas;
        protected Dictionary<string, SKBitmap> _imageCache;

        public GameRenderService(SKCanvas canvas, IUtilityService utility)
        {
            _canvas = canvas;
            Text = new TextService(canvas, utility);
            _imageCache = new Dictionary<string, SKBitmap>();
        }

        public int GetWindowWidth()
        {
            return 800;
        }

        public int GetWindowHeight()
        {
            return 600;
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

        public void DrawImage(int x, int y, string imageName)
        {
            var bitmap = LoadFromCache(imageName);
            _canvas.DrawBitmap(bitmap, x, y);
        }
    }
}
