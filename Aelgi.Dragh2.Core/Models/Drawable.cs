using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.Models
{
    public class Drawable
    {
        public ICollection<Drawable> Children = new List<Drawable>();

        public virtual void Update(IGameUpdateService gameService)
        {
            foreach (var child in Children)
            {
                child.Update(gameService);
            }
        }

        public virtual void Render(IGameRenderService gameService)
        {
            foreach (var child in Children)
            {
                child.Render(gameService);
            }
        }
    }
}
