using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoShooterDX
{
    class Laser : Projectile
    {
        public override Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Position.X + 4, (int)Position.Y + 6, 36, 4);
            }
        }

        public void Initialize(Animation animation, Vector2 position)
        {
            Damage = 10;
            Range = 100;

            Animation = animation;
            Position = position;
            
            moveSpeed = 30f;

            Active = true;
        }

        public override void Update(GameTime gameTime)
        {
            Position = new Vector2(Position.X + moveSpeed, Position.Y);
            Animation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch);
        }
    }
}