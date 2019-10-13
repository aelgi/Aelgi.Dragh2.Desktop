using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;

namespace Aelgi.Dragh2.Core.Entities
{
    public enum PlayerDirection
    {
        LEFT,
        RIGHT
    }

    public class Player : IDrawable
    {
        public static double MoveSpeed = 0.1;
        public static double Width = 0.4;

        protected Position _drawPosition = new Position(-0.5, -2);
        protected Position _playerPosition = new Position(0, 0);
        protected double _jumpPosition = 0;

        protected PlayerDirection _playerDirection = PlayerDirection.RIGHT;

        protected double GetJumpHeight()
        {
            var x = _jumpPosition / 30;
            // y = x^2 - 1
            return ((x * x) - 1) / 10;
        }

        protected string _imageName => "Player";

        private void HandlePlayerMove(IGameUpdateService gameService, Position worldPosition)
        {
            var deepGroundedBlock = gameService.WorldController.GetBlock(worldPosition.Add(0, -0.1));
            if (deepGroundedBlock != null) gameService.GamePosition.Y = deepGroundedBlock.WorldPosition.Y;

            var bottomLeft = worldPosition + new Position(-Width, -0.1);
            var bottomRight = worldPosition + new Position(Width, -0.1);
            var topLeft = worldPosition + new Position(-Width, -1.9);
            var topRight = worldPosition + new Position(Width, -1.9);

            var isLeft = gameService.WorldController.IsGrounded(bottomLeft) || gameService.WorldController.IsGrounded(topLeft);
            var isRight = gameService.WorldController.IsGrounded(bottomRight) || gameService.WorldController.IsGrounded(topRight);
            var isGrounded = gameService.WorldController.IsGrounded(worldPosition);
            var isTop = gameService.WorldController.IsGrounded(worldPosition + new Position(0, -2));

            if (!isGrounded)
            {
                if (_jumpPosition == 0) _jumpPosition = 30;
                _jumpPosition++;
            }
            else
            {
                _jumpPosition = 0;
                if (gameService.IsPressed(Key.UP)) _jumpPosition = 0.2;
            }

            if (gameService.IsPressed(Key.LEFT) && !isLeft)
            {
                gameService.GamePosition.X -= MoveSpeed;
                _playerDirection = PlayerDirection.LEFT;
            }
            if (gameService.IsPressed(Key.RIGHT) && !isRight)
            {
                gameService.GamePosition.X += MoveSpeed;
                _playerDirection = PlayerDirection.RIGHT;
            }

            if (isTop && _jumpPosition < 30)
            {
                _jumpPosition = 30;
            }

            if (_jumpPosition != 0) gameService.GamePosition.Y += GetJumpHeight();
        }

        private void HandlePlayerHit(IGameUpdateService gameService, Position worldPosition)
        {
            if (gameService.IsPressed(Key.INVENTORY)) return;

            if (gameService.IsPressed(Key.DIG_DOWN))
            {
                var bottom = worldPosition + new Position(0, 0.1);
                var bottomBlock = gameService.WorldController.GetBlock(bottom);
                if (bottomBlock != null) bottomBlock.OnHit();
            }
            else if (gameService.IsPressed(Key.DIG_UP))
            {
                var top = worldPosition + new Position(0, -2.1);
                var topBlock = gameService.WorldController.GetBlock(top);
                if (topBlock != null) topBlock.OnHit();
            }
            else if (gameService.IsPressed(Key.DIG_LEFT))
            {
                var top = worldPosition + new Position(-Width, -1.1);
                var bottom = worldPosition + new Position(-Width, -0.1);

                var topBlock = gameService.WorldController.GetBlock(top);
                var bottomBlock = gameService.WorldController.GetBlock(bottom);

                if (topBlock != null) topBlock.OnHit();
                else if (bottomBlock != null) bottomBlock.OnHit();
            }
            else if (gameService.IsPressed(Key.DIG_RIGHT))
            {
                var top = worldPosition + new Position(Width, -1.1);
                var bottom = worldPosition + new Position(Width, -0.1);

                var topBlock = gameService.WorldController.GetBlock(top);
                var bottomBlock = gameService.WorldController.GetBlock(bottom);

                if (topBlock != null) topBlock.OnHit();
                else if (bottomBlock != null) bottomBlock.OnHit();
            }
        }

        public void Update(IGameUpdateService gameService)
        {
            var worldPosition = _playerPosition + gameService.GamePosition;

            HandlePlayerMove(gameService, worldPosition);
            HandlePlayerHit(gameService, worldPosition);
        }

        public void Render(IGameRenderService gameService)
        {
            var currentPos = _drawPosition + _playerPosition;
            gameService.DrawImage(currentPos, _imageName);
        }
    }
}
