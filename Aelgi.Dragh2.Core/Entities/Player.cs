using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.Entities
{
    public class Player : IDrawable
    {
        public static double MoveSpeed = 0.25;

        protected Position _drawPosition;

        protected string _imageName => "Player";

        public void Update(IGameUpdateService gameService)
        {
            if (gameService.IsPressed(Key.UP)) gameService.GamePosition.Y -= MoveSpeed;
            if (gameService.IsPressed(Key.DOWN)) gameService.GamePosition.Y += MoveSpeed;
            if (gameService.IsPressed(Key.LEFT)) gameService.GamePosition.X -= MoveSpeed;
            if (gameService.IsPressed(Key.RIGHT)) gameService.GamePosition.X += MoveSpeed;

            _drawPosition = new Position(0, 0);
        }

        public void Render(IGameRenderService gameService)
        {
            gameService.DrawImage(_drawPosition, _imageName);
        }
    }
}
