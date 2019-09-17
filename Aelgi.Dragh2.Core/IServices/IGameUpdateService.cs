using Aelgi.Dragh2.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IGameUpdateService
    {
        bool IsPressed(Key key);
    }
}
