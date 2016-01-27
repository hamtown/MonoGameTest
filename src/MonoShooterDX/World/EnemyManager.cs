using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoShooterDX
{
    class EnemyManager
    {
        public List<Enemy> Enemies { get; set; }

        Texture2D _hitboxTexture;
        Texture2D _enemyTexture;

        ExplosionFactory _explosionFactory;
        
        TimeSpan _enemySpawnTime;
        TimeSpan _previousSpawnTime;
        Random _random;
        int _screenWidth;
        int _screenHeight;

        public void Initialize(Texture2D enemyTexture, ExplosionFactory explosionFactory, int screenWidth, int screenHeight)
        {
            _enemyTexture = enemyTexture;
            _explosionFactory = explosionFactory;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            Enemies = new List<Enemy>();

            _previousSpawnTime = TimeSpan.Zero;

            _enemySpawnTime = TimeSpan.FromSeconds(.25f);

            _random = new Random();
        }

        public void Initialize(Texture2D enemyTexture, ExplosionFactory explosionFactory, int screenWidth, int screenHeight, Texture2D hitboxTexture)
        {
            Initialize(enemyTexture, explosionFactory, screenWidth, screenHeight);

            _hitboxTexture = hitboxTexture;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - _previousSpawnTime > _enemySpawnTime)
            {
                _previousSpawnTime = gameTime.TotalGameTime;

                AddEnemy();
            }

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemies[i].Update(gameTime);

                if (Enemies[i].Dead)
                {
                    Enemies.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Draw(sb);
            }
        }

        private void AddEnemy()
        {
            Enemy enemy = new Enemy();

            Animation enemyAnimation = new Animation();
            enemyAnimation.Initialize(_enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            Vector2 position = new Vector2(_screenWidth + _enemyTexture.Width / 2,
                _random.Next(10, _screenHeight - 50));

            Death death = new EnemyDeath();
            death.Initialize(_explosionFactory);

            if (_hitboxTexture != null)
            {
                enemy.Initialize(enemyAnimation, death, position, _hitboxTexture); 
            }
            else
            {
                enemy.Initialize(enemyAnimation, death, position);
            }

            Enemies.Add(enemy);
        }
    }
}