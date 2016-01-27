using Microsoft.Xna.Framework;

namespace MonoShooterDX
{
    abstract class Projectile : SpriteBase
    {
        public virtual Rectangle Hitbox { 
            get { 
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); 
            } }

        public int Damage { get; protected set; }

        public int Range { get; protected set; }
    }
}