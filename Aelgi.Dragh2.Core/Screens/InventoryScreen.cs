using System;
using Aelgi.Dragh2.Core.Entities;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.Screens
{
    public class InventoryScreen : IDrawable
    {
        protected EntityController _entities;
        protected Position _topLeft;

        public InventoryScreen(EntityController entities)
        {
            _entities = entities;
        }

        public void Update(IGameUpdateService gameService)
        {
            _topLeft = gameService.WindowSize / 2;
            _topLeft *= -1;
            _topLeft += new Position(1, 1);
        }

        public void Render(IGameRenderService gameService)
        {
            gameService.DrawRect(_topLeft, _topLeft.Add(10, 10), Enums.Colors.White, true);
            gameService.DrawToScreen(_topLeft, 20, "Inventory:");
        }
    }
}
