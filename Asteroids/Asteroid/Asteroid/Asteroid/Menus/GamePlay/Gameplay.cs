using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid
{
    public class Gameplay : DrawableEntity
    {
        public static List<PowerUps> powers;
        Generator a;
        Shield shield;
        PlayerEntity player;
        public bool justStarted;
        public static bool paused;
        public static int playerScore;
        public static bool viewScore = false;
        public static int playerLives;
        private bool pauseKeyDown;
        private bool gameover;
        SpriteFont font;
        SpriteFont font2;
        Button btnQuit;
        Button btnAgain;
        Button btnScore;
        public static bool tryAgain = false;

        public Gameplay()
        {
            paused = true;
            pauseKeyDown = false;
            powers = new List<PowerUps>();
            gameover = false;
            shield = new Shield();
            shield.LoadContent();
            playerLives = 2;
            a = new Generator();
            player = new PlayerEntity();
            player.LoadContent();
            Game1.Instance.Entities.Add(player);
            playerScore = 0;
            justStarted = true;
            font = Game1.Instance.Content.Load<SpriteFont>("GameFont");
            font2 = Game1.Instance.Content.Load<SpriteFont>("Paused");
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!gameover)
            {
                checkPauseKey(Keyboard.GetState());
            }

            if (paused || gameover)
            {
                if (gameover)
                {
                    for (int i = 0; i < powers.Count; i++)
                    {
                        powers.Remove(powers[i]);
                    }

                    for (int i = 0; i < Generator.asteroidsentities.Count; i++)
                    {
                        Generator.asteroidsentities.Remove(Generator.asteroidsentities[i]);
                    }

                    Game1.Instance.Entities.Remove(player);

                    btnScore.Update(gameTime);
                    btnAgain.Update(gameTime);
                    btnQuit.Update(gameTime);

                    if (btnQuit.isClicked)
                    {
                        Game1.Instance.Exit();
                    }

                    if (btnScore.isClicked)
                    {
                        viewScore = true;
                    }

                    if (btnAgain.isClicked)
                    {
                        tryAgain = true;
                    }
                }
            }
            else
            {
                //Infinite asteroids
                if (Generator.asteroidsentities.Count == 0)
                {
                    a = new Generator();
                }

                if (player.move == true)
                {
                    justStarted = false;
                }

                //Collisioin Detection, not working smoothly as I thought it would have
                foreach (var asteroids in Generator.asteroidsentities)
                {
                    //A shield that protects player once started, it will disappear if player move or fire
                    if (justStarted)
                    {
                        //shield colision
                        asteroids.ShieldCollision(shield.GetBoundingBox());
                    }

                    if (justStarted == false)
                    {
                        //Player colision with asteroids
                        if (player.AsteroidCollision(asteroids.GetBoundingBox()))
                        {
                            if (playerLives > 0)
                            {
                                playerLives--;
                                justStarted = true;
                                paused = true;
                                Game1.Instance.Entities.Remove(player);
                                player = new PlayerEntity();
                                player.LoadContent();
                                Game1.Instance.Entities.Add(player);
                            }
                            else
                            {
                                Score score = new Score();
                                btnAgain = new Button(Game1.Instance.Content.Load<Texture2D>("again"), Game1.Instance.graphics.GraphicsDevice, new Vector2(350, 280));
                                btnQuit = new Button(Game1.Instance.Content.Load<Texture2D>("exit"), Game1.Instance.graphics.GraphicsDevice, new Vector2(350, 340));
                                btnScore = new Button(Game1.Instance.Content.Load<Texture2D>("score"), Game1.Instance.graphics.GraphicsDevice, new Vector2(350, 310));
                                gameover = true;
                            }
                        }
                    }

                    //Power ups colision
                    for (int i = 0; i < powers.Count; i++)
                    {
                        powers[i].PlayerCollision(player.GetBoundingBox());

                        if (!powers[i].Alive)
                        {
                            powers.Remove(powers[i]);
                        }
                    }

                    //Laser colision with asteroids
                    for (int i = 0; i < player.bList.Count; i++)
                    {
                        asteroids.BulletCollision(player.bList[i].GetBoundingBox());
                        player.bList[i].BulletCollision(asteroids.GetBoundingBox());
                    }
                    //asteroid colision with one another
                    for (int i = 0; i < Generator.asteroidsentities.Count; i++)
                    {
                        if (asteroids != Generator.asteroidsentities[i])
                        {
                            if (Generator.asteroidsentities[i].AsteroidCollision(asteroids.GetBoundingBox()))
                            {
                                asteroids.Speed = 0f;
                                Generator.asteroidsentities[i].Speed = 0f;
                            }
                        }
                    }
                }

                //update everything else, mainly the player
                for (int i = 0; i < Game1.Instance.Entities.Count; i++)
                {
                    Game1.Instance.Entities[i].Update(gameTime);
                    if (!Game1.Instance.Entities[i].Alive)
                    {
                        Game1.Instance.Entities.Remove(Game1.Instance.Entities[i]);
                    }
                }

                //update asteroids
                for (int i = 0; i < Generator.asteroidsentities.Count; i++)
                {
                    Generator.asteroidsentities[i].Update(gameTime);
                    if (!Generator.asteroidsentities[i].Alive)
                    {
                        Generator.asteroidsentities.Remove(Generator.asteroidsentities[i]);
                    }
                }
            }
        }

        void checkPauseKey(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.P))
            {
                pauseKeyDown = true;
            }
            else if (pauseKeyDown)
            {
                pauseKeyDown = false;
                paused = !paused;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (paused)
            {
                //Display instruction if game is paused
                Game1.Instance.spriteBatch.DrawString(font2, "Power Ups", new Vector2(340, 100), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Game1.Instance.spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("speed"), new Vector2(260, 140), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
                Game1.Instance.spriteBatch.DrawString(font2, "Increase ship speed", new Vector2(290, 130), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Game1.Instance.spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("firerate"), new Vector2(260, 170), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
                Game1.Instance.spriteBatch.DrawString(font2, "Increase ship fire rate", new Vector2(290, 160), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Game1.Instance.spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("life"), new Vector2(260, 200), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
                Game1.Instance.spriteBatch.DrawString(font2, "Increase ship life", new Vector2(290, 190), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Game1.Instance.spriteBatch.DrawString(font2, "Paused", new Vector2(350, 250), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Game1.Instance.spriteBatch.DrawString(font2, "Controls are : ", new Vector2(320, 290), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Game1.Instance.spriteBatch.DrawString(font2, "W, A, S, D to Move", new Vector2(320, 330), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Game1.Instance.spriteBatch.DrawString(font2, "Space to Shoot", new Vector2(320, 370), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                Game1.Instance.spriteBatch.DrawString(font2, "P to Pause", new Vector2(320, 410), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            if (gameover)
            {
                Game1.Instance.spriteBatch.DrawString(font2, "GameOver", new Vector2(350, 250), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                btnQuit.Draw(gameTime);
                btnAgain.Draw(gameTime);
                btnScore.Draw(gameTime);
            }

            if(justStarted)
            {
                shield.Draw(gameTime);
            }

            for (int i = 0; i < powers.Count; i++)
            {
                if (powers[i].Alive)
                {
                    powers[i].Draw(gameTime);
                }
            }

            for (int i = 0; i < Game1.Instance.Entities.Count; i++)
            {
                if (Game1.Instance.Entities[i].Alive)
                {
                    Game1.Instance.Entities[i].Draw(gameTime);
                }
            }

            for (int i = 0; i < Generator.asteroidsentities.Count; i++)
            {
                if (Generator.asteroidsentities[i].Alive)
                {
                    Generator.asteroidsentities[i].Draw(gameTime);
                }
            }

            Game1.Instance.spriteBatch.DrawString(font, "Score : " + playerScore.ToString(), new Vector2(10, 10), Color.White);
            Game1.Instance.spriteBatch.DrawString(font, "Lives : " + playerLives.ToString(), new Vector2(width - 100, 10), Color.White);
        
        }
    }
}
