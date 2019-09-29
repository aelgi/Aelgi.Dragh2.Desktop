using System;
using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;

namespace Aelgi.Dragh2.Core.Entities
{
    public class Player : IDrawable
    {
        public static int MoveSpeed = 1;

        public void Update(IGameUpdateService gameService)
        {
            if (gameService.IsPressed(Key.UP)) gameService.GamePosition.Y -= MoveSpeed;
            if (gameService.IsPressed(Key.DOWN)) gameService.GamePosition.Y += MoveSpeed;
            if (gameService.IsPressed(Key.LEFT)) gameService.GamePosition.X -= MoveSpeed;
            if (gameService.IsPressed(Key.RIGHT)) gameService.GamePosition.X += MoveSpeed;
        }

        public void Render(IGameRenderService gameService)
        {
        }
    }
}
