using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class GameRenderService : IGameRenderService
    {
        public ITextService Text { get; set; }
        protected SKCanvas _canvas;

        public GameRenderService(SKCanvas canvas, IUtilityService utility)
        {
            _canvas = canvas;
            Text = new TextService(canvas, utility);
        }

        public int GetWindowWidth()
        {
            return 800;
        }

        public int GetWindowHeight()
        {
            return 600;
        }
    }
}
