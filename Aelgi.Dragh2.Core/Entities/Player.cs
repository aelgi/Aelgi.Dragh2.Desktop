using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using System;

namespace Aelgi.Dragh2.Core.Entities
{
    public class Player : IDrawable
    {
        public static double MoveSpeed = 0.25;

        protected Position _drawPosition;
        protected Position _playerPosition = new Position(0, 0);
        protected double _jumpPosition = 0;

        protected double GetJumpHeight()
        {
            var x = _jumpPosition / 60 + 1;
            return -(x * x) + 1;
        }

        protected string _imageName => "Player";

        public void Update(IGameUpdateService gameService)
        {
            var worldPosition = _playerPosition + gameService.GamePosition;
            var bottomLeft = (worldPosition + new Position(-0.5, 0)).Floor(true, false);
            var bottomRight = (worldPosition + new Position(0.5, 0)).Ceil(true, false);
            var middleLeft = (worldPosition + new Position(-0.5, -1)).Floor(true, false);
            var middleRight = (worldPosition + new Position(0.5, -1)).Ceil(true, false);

            var isLeft = gameService.WorldController.IsGrounded(bottomLeft);
            var isRight = gameService.WorldController.IsGrounded(bottomRight);
            var isGrounded = isLeft || isRight;
            var isMiddleLeft = gameService.WorldController.IsGrounded(middleLeft);
            var isMiddleRight = gameService.WorldController.IsGrounded(middleRight);

            if (gameService.IsPressed(Key.LEFT) && !isMiddleLeft) gameService.GamePosition.X -= MoveSpeed;
            if (gameService.IsPressed(Key.RIGHT) && !isMiddleRight) gameService.GamePosition.X += MoveSpeed;
            if (isGrounded)
            {
                if (_jumpPosition != 0)
                    _jumpPosition = 0;
                if (gameService.IsPressed(Key.UP))
                {
                    _jumpPosition = 0.2;
                }
            }
            else
            {
                if (_jumpPosition == 0)
                {
                    _jumpPosition = 1;
                }
                _jumpPosition += 0.2;
            }

            Console.WriteLine($"Jump: {_jumpPosition}");

            _playerPosition.Y = -GetJumpHeight();
            _drawPosition = new Position(0, -2);
        }

        public void Render(IGameRenderService gameService)
        {
            gameService.DrawImage(_drawPosition + _playerPosition, _imageName);
        }
    }
}
