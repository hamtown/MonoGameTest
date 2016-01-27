using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoShooterDX
{
    class GameBackground
    {
        Texture2D _staticBackground;
        ParallaxingBackground[] _parallaxBackgrounds;

        Rectangle _staticRectangle;

        public void Initialize(Texture2D staticBackground, ParallaxingBackground[] parallaxBackgrounds, 
            int backgroundWidth, int backgroundHeight)
        {
            _staticBackground = staticBackground;
            _parallaxBackgrounds = parallaxBackgrounds;
            _staticRectangle = new Rectangle(0, 0, backgroundWidth, backgroundHeight);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _parallaxBackgrounds.Length; i++)
            {
                _parallaxBackgrounds[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_staticBackground, _staticRectangle, Color.White);

            for (int i = 0; i < _parallaxBackgrounds.Length; i++)
            {
                _parallaxBackgrounds[i].Draw(spriteBatch);
            }
        }
    }
}
