using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.Entities
{
    public class Player : IDrawable
    {
        public static double MoveSpeed = 0.1;
        public static double Width = 0.4;

        protected Position _drawPosition = new Position(-0.5, -2);
        protected Position _playerPosition = new Position(0, 0);
        protected double _jumpPosition = 0;

        protected double GetJumpHeight()
        {
            var x = _jumpPosition / 60;
            // y = x^2 - 1
            return ((x * x) - 1) / 20;
        }

        protected string _imageName => "Player";

        public void Update(IGameUpdateService gameService)
        {
            var worldPosition = _playerPosition + gameService.GamePosition;
            var bottomLeft = worldPosition + new Position(-Width, -0.1);
            var bottomRight = worldPosition + new Position(Width, -0.1);

            var isLeft = gameService.WorldController.IsGrounded(bottomLeft);
            var isRight = gameService.WorldController.IsGrounded(bottomRight);
            var isGrounded = gameService.WorldController.IsGrounded(worldPosition);

            if (!isGrounded)
            {
                if (_jumpPosition == 0) _jumpPosition = 60;
                _jumpPosition++;
            }
            else
            {
                _jumpPosition = 0;
                if (gameService.IsPressed(Key.UP)) _jumpPosition = 0.2;
            }

            if (gameService.IsPressed(Key.LEFT) && !isLeft) gameService.GamePosition.X -= MoveSpeed;
            if (gameService.IsPressed(Key.RIGHT) && !isRight) gameService.GamePosition.X += MoveSpeed;

            if (_jumpPosition != 0) gameService.GamePosition.Y += GetJumpHeight();
        }

        public void Render(IGameRenderService gameService)
        {
            var currentPos = _drawPosition + _playerPosition;
            gameService.DrawImage(currentPos, _imageName);
        }
    }
}
