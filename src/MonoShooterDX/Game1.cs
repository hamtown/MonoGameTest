using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;

namespace MonoShooterDX
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        Input input = new KeyboardInput();
        
        GameBackground gameBackground;
        ParallaxingBackground fogForeground;

        EnemyManager enemyManager;
        ProjectileManager _projectileManager;
        
        LaserCannon laserCannon;

        Song gameMusic;

        Texture2D _hitboxTexture;

        int _screenWidth;
        int _screenHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _screenHeight = GraphicsDevice.Viewport.Height;
            _screenWidth = GraphicsDevice.Viewport.Width;

            input = InputFactory.GetInput();

            player = new Player();

            gameBackground = new GameBackground();

            fogForeground = new ParallaxingBackground();
            
            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            enemyManager = new EnemyManager();
            _projectileManager = new ProjectileManager();
            laserCannon = new LaserCannon();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _hitboxTexture = Content.Load<Texture2D>("Graphics/HitboxDim");

            SoundEffect explosionSound = Content.Load<SoundEffect>("Sounds/explosion");
            Texture2D explosionStrip = Content.Load<Texture2D>("Graphics/explosion");
            ExplosionFactory explosionFactory = new ExplosionFactory(explosionStrip, explosionSound);

            //Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
            //    GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            Vector2 playerPosition = new Vector2(0, _screenHeight / 2);

            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Graphics/shipAnimation");

            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 35, Color.White, .75f, true);

            Death playerDeath = new PlayerDeath();
            playerDeath.Initialize(explosionFactory);

            //player.Initialize(playerAnimation, playerPosition, input, _projectileManager, _screenHeight, _screenWidth, _hitboxTexture);
            player.Initialize(playerAnimation, playerDeath, playerPosition, input, _projectileManager, _screenHeight, _screenWidth);

            Texture2D enemyTexture = Content.Load<Texture2D>("Graphics/mineAnimation");

            _projectileManager.Initialize(_screenWidth, _screenHeight);

            //enemyManager.Initialize(enemyTexture, explosionStrip, explosionSound, _screenWidth, _screenHeight, _hitboxTexture);
            enemyManager.Initialize(enemyTexture, explosionFactory, _screenWidth, _screenHeight);

            ParallaxingBackground bgLayer1 = new ParallaxingBackground(); 
            ParallaxingBackground bgLayer2 = new ParallaxingBackground();

            bgLayer1.Initialize(Content.Load<Texture2D>("Graphics/bgLayer1"), _screenWidth, _screenHeight, -1);
            bgLayer2.Initialize(Content.Load<Texture2D>("Graphics/bgLayer2"), _screenWidth, _screenHeight, -2);

            Texture2D mainBackground;
            mainBackground = Content.Load<Texture2D>("Graphics/mainbackground");

            gameBackground.Initialize(mainBackground, new ParallaxingBackground[] { bgLayer1, bgLayer2 }, _screenWidth, _screenHeight);

            fogForeground.Initialize(Content.Load<Texture2D>("Graphics/WispyClouds2"), _screenWidth, _screenHeight, -2);

            SoundEffect laserSound = Content.Load<SoundEffect>("Sounds/LaserFire");
            Texture2D laserTexture = Content.Load<Texture2D>("Graphics/laser");
            laserCannon.Initialize(laserTexture, laserSound);
            
            player.Weapon = laserCannon;

            gameMusic = Content.Load<Song>("Music/gameMusic");

            MediaPlayer.Volume = .75f;
            MediaPlayer.Play(gameMusic);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (input.Exit)
            {
                Exit();
            }

            input.Update(gameTime);

            _projectileManager.Update(gameTime);

            player.Update(gameTime);

            enemyManager.Update(gameTime);
            
            UpdateCollision();

            gameBackground.Update(gameTime);
            fogForeground.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            gameBackground.Draw(spriteBatch);

            player.Draw(spriteBatch);

            _projectileManager.Draw(spriteBatch);

            enemyManager.Draw(spriteBatch);

            fogForeground.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateCollision()
        {
            Enemy enemy;

            Projectile projectile;

            for (int i = 0; i < enemyManager.Enemies.Count; i++)
            {
                enemy = enemyManager.Enemies[i];

                if (!enemy.Active)
                {
                    continue;
                }
                
                for (int j = 0; j < _projectileManager.Projectiles.Count; j++)
                {
                    projectile = _projectileManager.Projectiles[j];
                    
                    if (projectile.Hitbox.Intersects(enemy.Hitbox))
                    {
                        enemy.Health -= projectile.Damage;
                        enemy.Active = false;
                        projectile.Active = false;
                    }
                }

                if (enemy.Active && player.Active && player.Hitbox.Intersects(enemy.Hitbox))
                {
                    player.Health -= enemy.Damage;

                    enemy.Health = 0;
                }
            }
        }

        //private bool PixelCollisionOccurs(Player player, Enemy enemy)
        //{
        //    Rectangle playerRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Width, player.Height);
        //    Rectangle enemyRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Width, enemy.Height);

        //    Rectangle collisionRectangle = GetCollisionRectange(playerRectangle, enemyRectangle);

        //    Color[] playerPixels = player.Animation.GetBits(collisionRectangle);
        //    Color[] enemyPixels = enemy.Animation.GetBits(collisionRectangle);

        //    //Check for pixel collisions

        //    return true;
        //}

        //private Rectangle GetCollisionRectange(Rectangle rectangle1, Rectangle rectangle2)
        //{
        //    int x1 = Math.Max(rectangle1.X, rectangle2.X);
        //    int x2 = Math.Min(rectangle1.X + rectangle1.Width, rectangle2.X + rectangle2.Width);

        //    int y1 = Math.Max(rectangle1.Y, rectangle2.Y);
        //    int y2 = Math.Max(rectangle1.Y + rectangle1.Height, rectangle2.Y + rectangle2.Height);

        //    return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        //}
    }
}