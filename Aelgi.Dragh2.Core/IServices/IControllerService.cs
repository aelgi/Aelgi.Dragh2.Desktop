using Aelgi.Dragh2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.IServices
{
    public interface IControllerService
    {
        void RegisterDrawable(string name, Drawable obj);
        Drawable GetDrawable(string name);
        void RemoveDrawable(string name);
        void Draw();
    }
}
