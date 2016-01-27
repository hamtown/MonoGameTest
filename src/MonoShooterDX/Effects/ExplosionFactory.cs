using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MonoShooterDX
{
    class ExplosionFactory
    {
        private Texture2D _explosionStrip;
        private SoundEffect _explosionSound;

        public ExplosionFactory(Texture2D explosionStrip, SoundEffect explosionSound)
        {
            _explosionStrip = explosionStrip;
            _explosionSound = explosionSound;
        }

        public Explosion GetExplosion()
        {
            Animation explosionAnimation = new Animation();
            explosionAnimation.Initialize(_explosionStrip, Vector2.Zero, 133, 134, 12, 30, Color.White, 1f, false);

            SoundEffectInstance explosionSoundInstance = _explosionSound.CreateInstance();
            explosionSoundInstance.Volume = .1f;

            Explosion explosion = new Explosion(explosionAnimation, explosionSoundInstance);

            return explosion;
        }
    }
}