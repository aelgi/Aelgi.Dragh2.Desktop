using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IStatsService
    {
        void SetFPS(int fps);
        int GetFPS();
    }
}
