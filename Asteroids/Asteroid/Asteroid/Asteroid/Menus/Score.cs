using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid
{
    class Score : DrawableEntity
    {
        //High-score system
        public static int maxHighScore = 11;
        SpriteFont font;
        public int[] scores;
        public float x = 300f;
        public float y = 100f;
        public static bool play = false;
        Button btnQuit;
        Button btnPlay;

        public Score()
        {
            //open file
            font = Game1.Instance.Content.Load<SpriteFont>("GameFont");
            StreamReader reader = new StreamReader("score.txt");

            string line;
            scores = new int[maxHighScore];

            //read in file
            for (int i = maxHighScore - 1; i != 0; --i)
            {
                line = reader.ReadLine();
                scores[i] = int.Parse(line);
            }
            //sort array
            Array.Sort(scores);
            //replace smallest score if playerscore is higher and sort the array again
            if(Gameplay.playerScore > scores[0])
            {
                scores[0] = Gameplay.playerScore;
                Array.Sort(scores);
            }
            //close the file for reading
            reader.Close();

            //open file for writing
            StreamWriter writer = new StreamWriter("score.txt");
            //write to file
            for (int i = maxHighScore - 1; i != 0; --i)
            {
                writer.WriteLine(scores[i]);
            }

            writer.Close();
        }

        public override void LoadContent()
        {
            btnPlay = new Button(Game1.Instance.Content.Load<Texture2D>("start"), Game1.Instance.graphics.GraphicsDevice, new Vector2(650f, 330f));
            btnQuit = new Button(Game1.Instance.Content.Load<Texture2D>("exit"), Game1.Instance.graphics.GraphicsDevice, new Vector2(650f, 400f));
        }

        public override void Update(GameTime gameTime)
        {
            btnPlay.Update(gameTime);
            btnQuit.Update(gameTime);
            if(btnPlay.isClicked)
            {
                play = true;
            }

            if(btnQuit.isClicked)
            {
                Game1.Instance.Exit();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            btnPlay.Draw(gameTime);
            btnQuit.Draw(gameTime);
            Game1.Instance.spriteBatch.DrawString(font, "Your Score : " + Gameplay.playerScore.ToString(), new Vector2(x, y), Color.White);
            Game1.Instance.spriteBatch.DrawString(font, "HighScore : ", new Vector2(x, y+=30), Color.White);
            for (int i = maxHighScore - 1; i != 0; --i)
            {
                Game1.Instance.spriteBatch.DrawString(font, scores[i].ToString(), new Vector2(x, y+=30), Color.White);
            }
            y = 100;
        }
    }
}
