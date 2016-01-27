using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoShooterDX
{
    abstract class SpriteBase
    {
        protected Vector2 _position;

        public Animation Animation { get; set; }

        public virtual Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                Animation.Position = value;
            }
        }

        //public LifeLevel LifeLevel { get; set; }

        public bool Active { get; set; }

        protected float moveSpeed;

        public virtual int Width
        {
            get { return Animation.FrameWidth; }
        }

        public virtual int Height
        {
            get { return Animation.FrameHeight; }
        }

        //public abstract void Initialize(Animation animation, Vector2 position);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
