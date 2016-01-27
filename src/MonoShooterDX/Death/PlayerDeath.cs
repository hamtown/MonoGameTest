using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoShooterDX
{
    class PlayerDeath:Death
    {
        TimeSpan previousExplosion;
        TimeSpan explosionDelay = TimeSpan.FromSeconds(.1f);
        List<Explosion> _explosions = new List<Explosion>();

        public override void Initialize(ExplosionFactory explosionFactory)
        {
            base.Initialize(explosionFactory);

            for (int i = 0; i < 5; i++)
            {
                _explosions.Add(_explosionFactory.GetExplosion());
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Dying)
            {
                if (_explosions.Any(e=>!e.Completed))
                {
                    if (gameTime.TotalGameTime - previousExplosion > explosionDelay)
                    {
                        previousExplosion = gameTime.TotalGameTime;

                        _explosions.First(e => !e.Completed).Explode();
                    }

                    for (int i = 0; i < _explosions.Count; i++)
                    {
                        _explosions[i].Update(gameTime);
                    }

                    if (_explosions.Count(e=>e.Completed) == 4)
                    {
                        ShouldVanish = true;
                    }
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
                for (int i = 0; i < _explosions.Count; i++)
                {
                    _explosions[i].Draw(spriteBatch);
                }
            }
        }

        public override void StartDying()
        {
            Dying = true;

            //Cheap way of adding Wobble
            _explosions[0].Position = AddWobble(_explosions[0].Position, -20, -20);
            _explosions[1].Position = AddWobble(_explosions[1].Position, 30, -20);
            _explosions[2].Position = AddWobble(_explosions[2].Position, -20, 10);            
            _explosions[3].Position = AddWobble(_explosions[3].Position, 20, 30);
            _explosions[4].Position = AddWobble(_explosions[4].Position, 0, 0);
            
        }

        public override void Center(Vector2 centerOn)
        {
            for (int i = 0; i < _explosions.Count; i++)
            {
                _explosions[i].Center(centerOn);
            }
        }

        private Vector2 AddWobble(Vector2 position, int addX, int addY)
        {
            return new Vector2(position.X + addX, position.Y + addY);
        }
    }
}