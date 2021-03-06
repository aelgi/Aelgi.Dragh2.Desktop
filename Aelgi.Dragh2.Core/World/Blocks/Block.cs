﻿using Aelgi.Dragh2.Core.IModels;
using Aelgi.Dragh2.Core.Inv.Items;
using Aelgi.Dragh2.Core.IServices;
using Aelgi.Dragh2.Core.Models;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.World
{
    public abstract class Block : IDrawable
    {
        public Position Position { get; set; }
        public Position ChunkPosition { get; set; }

        public abstract int MaxHealth { get; }
        protected int _health;
        public bool IsAlive => _health > 0;

        public virtual bool IsCollidable => true;

        protected abstract ICollection<InventoryItem> _dropsOnDeath { get; }

        protected Position _positionOnScreen;

        public static int BlockSize => 32;

        public Position WorldPosition => ChunkPosition + Position;

        public Block()
        {
            _health = MaxHealth;
        }

        public virtual ICollection<InventoryItem> OnHit(IWorldController worldController, InventoryItem activeItem)
        {
            _health -= activeItem.DamageAmount(this);

            if (_health <= 0) return _dropsOnDeath;
            return null;
        }

        protected void DrawImage(IGameRenderService gameService, string imageName)
        {
            gameService.DrawImage(_positionOnScreen, imageName);
            DrawHealth(gameService);
        }

        protected void DrawHealth(IGameRenderService gameService)
        {
            if (_health == MaxHealth) return;
            var center = _positionOnScreen + new Position(0.5, 0.5);
            var percent = 1.0 - ((double)_health / MaxHealth);
            var percentPos = percent / 2;
            var topLeft = center - new Position(percentPos, percentPos);
            var bottomRight = center + new Position(percentPos, percentPos);
            gameService.DrawRect(topLeft, bottomRight, Enums.Colors.White, true);
        }

        public abstract void Render(IGameRenderService gameService);
        public virtual void Update(IGameUpdateService gameService)
        {
            var realPosition = ChunkPosition + Position;
            _positionOnScreen = realPosition - gameService.GamePosition;
            //_positionOnScreen = (ChunkPosition - gameService.GamePosition) + Position;
            //_positionOnScreen = new Position(0, 0);
        }
    }
}
