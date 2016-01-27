using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MonoShooterDX
{
    class Player : SpriteBase
    {
        Death _death = new PlayerDeath();

        Texture2D _hitbox;
        Input _input;
        ProjectileManager _projectileManager;

        int _heightBoundry;
        int _widthBoundry;

        public float PlayerMoveSpeed { get { return moveSpeed; } }

        public int Health { get; set; }

        public LaserCannon Weapon { get; set; }

        public bool Visible
        {
            get { return !Dead; }
        }

        public bool Dying
        {
            get { return _death.Dying; }
        }

        public bool Dead
        {
            get { return !Active && !_death.Dying; }
        }
        
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Position.X+20, (int)Position.Y+5, Width-55, Height-30);
            }
        }

        public void Initialize(Animation animation, Death death, Vector2 position, Input input, ProjectileManager projectileManager, int heightBoundry, int widthBoundry)
        {
            Animation = animation;

            Position = position;

            _input = input;
            _projectileManager = projectileManager;

            _heightBoundry = heightBoundry;
            _widthBoundry = widthBoundry;
                        
            _death = death;

            Active = true;

            //Health = 20;
            Health = 100;

            moveSpeed = 8f;
        }

        public void Initialize(Animation animation, Death death, Vector2 position, Input input, ProjectileManager projectileManager, int heightBoundry, int widthBoundry, Texture2D hitbox)
        {
            Initialize(animation, death, position, input, projectileManager, heightBoundry, widthBoundry);

            _hitbox = hitbox;
        }

        public void Initialize(Animation animation, Vector2 position)
        {
            Animation = animation;
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            if (Health <= 0 && Active)
            {
                _death.Center(Hitbox.Center.ToVector2());
                _death.StartDying();

                Active = false;
            }

            if(Active)
            {
                Position = new Vector2(Position.X + _input.XDirection * PlayerMoveSpeed, Position.Y + _input.YDirection * PlayerMoveSpeed);

                // Keep Play Inbounds
                Position = new Vector2(MathHelper.Clamp(Position.X, 0, _widthBoundry - Hitbox.Width), MathHelper.Clamp(Position.Y, 0, _heightBoundry - Hitbox.Height));

                if (_input.Fire)
                {
                    Weapon.Fire(gameTime, _projectileManager, Position);
                }

                Animation.Update(gameTime);
            }
            else if (Dying)
            {
                _death.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible && !_death.ShouldVanish)
            {
                Animation.Draw(spriteBatch);

                if (_hitbox != null)
                {
                    spriteBatch.Draw(_hitbox, Hitbox, Color.White);
                } 
            }

            if (Dying)
            {
                _death.Draw(spriteBatch);
            }
        }
    }
}