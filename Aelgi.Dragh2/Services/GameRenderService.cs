using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class GameRenderService : IGameRenderService
    {
        public ITextService TextService { get; set; }

        public GameRenderService(ITextService textService)
        {
            TextService = textService;
        }

        public void DrawToScreen(uint x, uint y, uint size, Colors color, string text)
        {
            TextService.DrawToScreen(x, y, size, color, text);
        }

        public void DrawToScreen(uint x, uint y, uint size, string text)
        {
            TextService.DrawToScreen(x, y, size, text);
        }
    }
}
