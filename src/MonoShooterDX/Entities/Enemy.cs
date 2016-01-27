using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MonoShooterDX
{
    class Enemy : SpriteBase
    {
        private bool _showHitbox = false;
        private Texture2D _hitbox;

        private Death _death;

        public int Health { get; set; }

        public int Damage { get; set; }

        public int Value { get; set; }

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
                return new Rectangle((int)Position.X+7, (int)Position.Y+2, Width-19, Height-10);
            }
        }

        public void Initialize(Animation animation, Death death, Vector2 position)
        {
            Animation = animation;
            Position = position;            
            
            _death = death;

            Active = true;

            Health = 10;

            Damage = 10;

            moveSpeed = 6f;

            Value = 100;
        }

        public void Initialize(Animation animation, Death death, Vector2 position, Texture2D hitbox)
        {
            Initialize(animation, death, position);

            _hitbox = hitbox;
            _showHitbox = true;
        }

        public override void Update(GameTime gameTime)
        {
            //Destroy Enemy
            if (Position.X < -Width || Health <= 0)
            {
                if (Health <= 0 && !Dying)
                {
                    _death.StartDying();
                }

                Active = false;
            }

            if (Active)
            {
                Position = new Vector2(Position.X - moveSpeed, Position.Y);
                Animation.Position = Position;

                Animation.Update(gameTime);
            }
            else if (Dying)
            {
                _death.Center(Hitbox.Center.ToVector2());

                _death.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Animation.Draw(spriteBatch);
                if (_showHitbox)
                {
                    spriteBatch.Draw(_hitbox, Hitbox, Color.White); 
                }
            }
            else if (Dying)
            {
                _death.Draw(spriteBatch);
            }
        }
    }
}