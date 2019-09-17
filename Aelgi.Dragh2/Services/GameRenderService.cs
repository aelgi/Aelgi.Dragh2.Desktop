using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class GameRenderService : IGameRenderService
    {
        public ITextService Text { get; set; }

        public GameRenderService(ITextService textService)
        {
            Text = textService;
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
