using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IGameUpdateService
    {
        Position WindowSize { get; set; }
        Position GamePosition { get; set; }

        bool IsPressed(Key key);

        int GetFPS();
    }
}
