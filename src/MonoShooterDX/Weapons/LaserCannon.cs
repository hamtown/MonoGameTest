using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoShooterDX
{
    class LaserCannon
    {
        Texture2D _laserTexture;
        SoundEffect _laserSound;
        SoundEffectInstance _laserSoundInstance;
        TimeSpan _laserSpawnTime;
        TimeSpan _previousLaserSpawnTime;

        public void Initialize(Texture2D laserTexture, SoundEffect laserSound)
        {
            _laserTexture = laserTexture;
            _laserSound = laserSound;
            _laserSoundInstance = _laserSound.CreateInstance();

            const float RATE_OF_FIRE_PER_SECOND = 3f;

            _laserSpawnTime = TimeSpan.FromSeconds(1 / RATE_OF_FIRE_PER_SECOND);
        }

        public void Fire(GameTime gameTime, ProjectileManager manager, Vector2 position)
        {
            if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
            {
                _previousLaserSpawnTime = gameTime.TotalGameTime;

                AddLaser(manager, position);
                                
                _laserSoundInstance.Play();
            }
        }

        private void AddLaser(ProjectileManager manager, Vector2 position)
        {
            Animation laserAnimation = new Animation();
            laserAnimation.Initialize(_laserTexture, Vector2.Zero, 46, 16, 1, 30, Color.White, .75f, true);

            Laser laser = new Laser();

            var laserPosition = position;
            laserPosition.X += 40;
            laserPosition.Y += 24;

            laser.Initialize(laserAnimation, laserPosition);
            manager.Add(laser);
        }
    }
}