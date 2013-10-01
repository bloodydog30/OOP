using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private static Game1 instance;
        private Gameplay game;
        private Score highScores;

        private List<DrawableEntity> entities = new List<DrawableEntity>();

        public List<DrawableEntity> Entities
        {
            get { return entities; }
        }

        enum GameState
        {
            MainMenu,
            Score,
            Playing,
        }
        GameState CurrentGameState = GameState.MainMenu;

        public int Width
        {
            get
            {
                return GraphicsDevice.Viewport.Width;
            }
        }

        public int Height
        {
            get
            {
                return GraphicsDevice.Viewport.Height;
            }
        }

        Button btnPlay;
        Button btnExit;
        Button btnScore;

        public static Game1 Instance
        {
            get
            {
                return instance;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            btnPlay = new Button(Content.Load<Texture2D>("start"), graphics.GraphicsDevice, new Vector2(650f, 350f));
            btnScore = new Button(Content.Load<Texture2D>("score"), graphics.GraphicsDevice, new Vector2(650f, 380f));
            btnExit = new Button(Content.Load<Texture2D>("exit"), graphics.GraphicsDevice, new Vector2(650f, 410f));

            entities.Add(btnPlay);
            entities.Add(btnExit);
            entities.Add(btnScore);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].LoadContent();
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            //Different game state to update
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    {
                        for (int i = 0; i < entities.Count; i++)
                        {
                            entities[i].Update(gameTime);

                            if (entities[i].Alive == false)
                            {
                                entities.Remove(entities[i]);
                            }
                        }

                        if (btnPlay.isClicked == true)
                        {
                            game = new Gameplay();
                            btnExit.Alive = false;
                            btnPlay.Alive = false;
                            btnScore.Alive = false;
                            CurrentGameState = GameState.Playing;
                        }

                        if (btnScore.isClicked == true)
                        {
                            highScores = new Score();
                            highScores.LoadContent();
                            btnExit.Alive = false;
                            btnPlay.Alive = false;
                            btnScore.Alive = false;
                            CurrentGameState = GameState.Score;
                        }

                        if (btnExit.isClicked == true)
                        {
                            Exit();
                        }

                        break;
                    }
                case GameState.Playing:
                    {
                        if (Gameplay.viewScore)
                        {
                            Gameplay.viewScore = false;
                            highScores = new Score();
                            highScores.LoadContent();
                            CurrentGameState = GameState.Score;
                        }

                        if(Gameplay.tryAgain)
                        {
                            Gameplay.tryAgain = false;
                            game = new Gameplay();
                        }

                        game.Update(gameTime);
                        break;
                    }

                case GameState.Score:
                    {
                        highScores.Update(gameTime);
                        if(Score.play)
                        {
                            Score.play = !Score.play;

                            for (int i = 0; i < Game1.Instance.Entities.Count; i++)
                            {
                                Game1.Instance.Entities.Remove(Game1.Instance.Entities[i]);
                            }

                            for (int i = 0; i < Generator.asteroidsentities.Count; i++)
                            {
                                Generator.asteroidsentities.Remove(Generator.asteroidsentities[i]);
                            }

                            game = new Gameplay();
                            CurrentGameState = GameState.Playing;
                        }
                        break;
                    }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            //Different game state to draw
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("mainmenu"), new Rectangle(0, 0, Width, Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                        for (int i = 0; i < entities.Count; i++)
                        {
                            entities[i].Draw(gameTime);
                        }
                        break;
                    }
                case GameState.Playing:
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("gamebackground"), new Rectangle(0, 0, Width, Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                        game.Draw(gameTime);
                        break;
                    }
                case GameState.Score:
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("gamebackground"), new Rectangle(0, 0, Width, Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                        highScores.Draw(gameTime);
                        break;
                    }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
