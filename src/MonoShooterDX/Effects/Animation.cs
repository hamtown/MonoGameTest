using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoShooterDX
{
    class Animation
    {
        Texture2D _spriteStrip;

        float _scale;

        int _elapsedTime;

        int _frameTime;

        int _frameCount;

        int _currentFrame;

        Color _color;

        Rectangle _sourceRect = new Rectangle();

        Rectangle _destinationRect = new Rectangle();

        public int FrameWidth { get; set; }

        public int FrameHeight { get; set; }

        public int SpriteWidth
        {
            get
            {
                return (int)(FrameWidth * _scale);
            }
        }

        public int SpriteHeight
        {
            get
            {
                return (int)(FrameHeight * _scale);
            }
        }

        public bool Active { get; set; }

        public bool Looping { get; set; }

        public Vector2 Position { get; set; }

        public void Initialize(Texture2D spriteStrip, Vector2 position, int frameWidth, int frameHeight,
            int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            _color = color;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            _frameCount = frameCount;
            _frameTime = frameTime;
            _scale = scale;

            Looping = looping;
            Position = position;
            _spriteStrip = spriteStrip;

            _elapsedTime = 0;
            _currentFrame = 0;

            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

            _elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsedTime > _frameTime)
            {
                _currentFrame++;

                if (_currentFrame == _frameCount)
                {
                    _currentFrame = 0;

                    if (!Looping)
                    {
                        Active = false;
                    }
                }

                _elapsedTime = 0;
            }

            _sourceRect = new Rectangle(_currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            _destinationRect = new Rectangle(
                (int)Position.X,// - (int)(FrameWidth * _scale) / 2,
                (int)Position.Y,// - (int)(FrameHeight * _scale / 2),
                (int)(FrameWidth * _scale),
                (int)(FrameHeight * _scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(_spriteStrip, _destinationRect, _sourceRect, _color);
            }
        }

        //public Color[] GetBits(Rectangle rectangle)
        //{
        //    //Implement this correctly

        //    Rectangle scaledRectangle = new Rectangle(rectangle.X - Position.X, rectangle.Y-Position.Y, 

        //    Color[] bits = new Color[0];
        //    _spriteStrip.GetData(0, rectangle, bits, 0, rectangle.Width * rectangle.Height);

        //    return bits;
        //}
    }
}
