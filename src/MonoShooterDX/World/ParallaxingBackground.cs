using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MonoShooterDX
{
    class ParallaxingBackground
    {
        int _bgWidth;
        int _bgHeight;        

        Texture2D _texture;

        Vector2[] _positions;

        int _speed;

        public void Initialize(Texture2D background, int screenWidth, 
            int screenHeight, int speed)
        {
            _bgWidth = screenWidth;
            _bgHeight = screenHeight;

            _texture = background;

            _speed = speed;

            _positions = new Vector2[screenWidth / _texture.Width + 1];

            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i] = new Vector2(i * _texture.Width, 0);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i].X += _speed;
            }

            for (int i = 0; i < _positions.Length; i++)
            {
                if (_speed <= 0) // Background Scrolls Left
                {
                    if (_positions[i].X <= -_texture.Width)
                    {
                        //If this is the first texture, move it behind the last texture
                        int wrapBehindTextureIndex = i == 0 ? _positions.Length - 1 : i - 1;

                        Wrap(i, wrapBehindTextureIndex);
                    }
                }
                else // Background Scrolls Right
                {
                    if (_positions[i].X >= _texture.Width)
                    {
                        //If this is the last texture, move it behind the first texture
                        int wrapBehindTextureIndex = i == _positions.Length ? 0 : i + 1;

                        Wrap(i, wrapBehindTextureIndex);
                    }
                }
            }
        }

        private void Wrap(int wrapTextureIndex, int textureIndexToWrapBehind)
        {
            _positions[wrapTextureIndex].X = _positions[textureIndexToWrapBehind].X + _texture.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _positions.Length; i++)
            {
                Rectangle rectBg = new Rectangle((int)_positions[i].X, (int)_positions[i].Y, _bgWidth, _bgHeight);

                spriteBatch.Draw(_texture, rectBg, Color.White);
            }
        }
    }
}