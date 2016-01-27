using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoShooterDX
{
    class Explosion : SpriteBase
    {
        SoundEffectInstance _soundEffect;

        bool _startedExploding;

        public bool Completed
        {
            get{return _startedExploding && !Active;}
        }

        public Explosion(Animation animation, SoundEffectInstance soundEffect)
        {
            Animation = animation;
            _soundEffect = soundEffect;
        }
        
        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Animation.Update(gameTime);

                if (!Animation.Active)
                {
                    Active = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Animation.Draw(spriteBatch);
            }
        }

        public void Center(Vector2 centerOn)
        {
            Vector2 centeredPosition = new Vector2(
                    centerOn.X - (Width / 2),
                    centerOn.Y - (Height / 2)
                    );

            Position = centeredPosition;
        }

        public void Explode()
        {
            if (!Active)
            {
                Active = true;

                _startedExploding = true;

                if (_soundEffect.State != SoundState.Playing)
                {
                    _soundEffect.Play();
                }
            }
        }
    }
}