using System;
using Aelgi.Dragh2.Core.Entities;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;

namespace Aelgi.Dragh2.Core.Screens
{
    public enum Screen
    {
        NONE,
        INVENTORY,
    }

    public class ScreensController : IDrawable
    {
        protected InventoryScreen _inventroyScreen;
        protected Screen _visibleScreen = Screen.NONE;

        public ScreensController(EntityController entities)
        {
            _inventroyScreen = new InventoryScreen(entities);
        }

        public void Update(IGameUpdateService gameService)
        {
            if (gameService.IsPressed(Enums.Key.INVENTORY)) _visibleScreen = Screen.INVENTORY;
            else _visibleScreen = Screen.NONE;

            switch (_visibleScreen)
            {
                case Screen.INVENTORY:
                    _inventroyScreen.Update(gameService);
                    break;

                case Screen.NONE:
                    break;
            }
        }

        public void Render(IGameRenderService gameService)
        {
            switch (_visibleScreen)
            {
                case Screen.INVENTORY:
                    _inventroyScreen.Render(gameService);
                    break;

                case Screen.NONE:
                    break;
            }
        }
    }
}
