using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class StatsService : IStatsService
    {
        protected int _fps = 0;

        public int GetFPS()
        {
            return _fps;
        }

        public void SetFPS(int fps)
        {
            _fps = fps;
        }
    }
}
