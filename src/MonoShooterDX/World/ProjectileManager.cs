using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoShooterDX
{
    class ProjectileManager
    {
        public List<Projectile> Projectiles { get; set; }

        int _width;
        int _height;

        public void Initialize(int screenWidth, int screenHeight)
        {
            _width = screenWidth;
            _height = screenHeight;

            Projectiles = new List<Projectile>();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                Projectiles[i].Update(gameTime);

                if (!Projectiles[i].Active || Projectiles[i].Position.X > _width)
                {
                    Projectiles.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Draw(sb);
            }
        }

        public void Add(Projectile projectile)
        {
            Projectiles.Add(projectile);
        }
    }
}
