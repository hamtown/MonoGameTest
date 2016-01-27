using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoShooterDX
{
    abstract class Death:SpriteBase
    {
        protected Explosion _explosion;
        protected ExplosionFactory _explosionFactory;

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                _explosion.Position = value;
            }
        }

        public override int Width
        {
            get { return _explosion.Width; }
        }

        public override int Height
        {
            get { return _explosion.Width; }
        }

        public virtual bool Dying { get; protected set; }

        public virtual bool ShouldVanish { get; set; }

        public virtual void Initialize(ExplosionFactory explosionFactory)
        {
            _explosionFactory = explosionFactory;
        }

        public override void Update(GameTime gameTime)
        {
            if (Dying)
            {
                if (_explosion.Active)
                {
                    _explosion.Update(gameTime);
                }
                else
                {
                    Dying = false;
                }                
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Dying)
            {
                _explosion.Draw(spriteBatch); 
            }
        }

        public virtual void StartDying()
        {
            Dying = true;

            _explosion = _explosionFactory.GetExplosion();

            _explosion.Explode();
        }

        public virtual void Center(Vector2 centerOn)
        {
            _explosion.Center(centerOn);
        }
    }
}