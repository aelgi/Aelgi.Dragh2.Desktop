using Aelgi.Dragh2.Core.Crafting;
using Aelgi.Dragh2.Core.Entities;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.Inv;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.Screens
{
    public class InventoryScreen : IDrawable
    {
        protected EntityController _entities;
        protected Position _topLeft;
        protected Inventory _inventory;
        protected RecipeManager _recipeManager;

        public InventoryScreen(EntityController entities)
        {
            _entities = entities;
            _recipeManager = new RecipeManager();
            _recipeManager.GetAllRecipes();
        }

        public void Update(IGameUpdateService gameService)
        {
            _topLeft = gameService.WindowSize / 2;
            _topLeft *= -1;
            _topLeft += new Position(0, 2);
            _inventory = _entities.FindPlayer()?.Inventory;
        }

        public void Render(IGameRenderService gameService)
        {
            gameService.DrawRect(_topLeft, _topLeft.Add(_inventory.MaxInventoryItems, 1), Enums.Colors.White, true);
            gameService.DrawToScreen(_topLeft.Add(0, -0.2), 20, "Inventory:");

            if (_inventory == null) return;

            for (var i = -1; i < _inventory.MaxInventoryItems; i++)
                _inventory.GetItem(i)?.Draw(gameService, _topLeft.Add(i + 1, 0));
        }
    }
}
