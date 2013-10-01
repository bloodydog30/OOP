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
    class Small : Asteroids
    {
        Random randomNum = new Random();
        int num;
        float speed;
        float rot;
        public Small()
        {
            rot = 1f;
            speed = 0f;
            Hp = 1;
            score = 10;
        }

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("small");
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            rot += 2.5f * timeDelta;

            if (speed < 0.25)
            {
                speed += 0.05f;
            }

            position += velocity * timeDelta * speed;

            //Move asteroid to opposite side if go to edge
            if (position.X > width)
            {
                position.X = 0;
            }

            if (position.Y > height)
            {
                position.Y = 0;
            }

            if (position.X < 0)
            {
                position.X =width;
            }

            if (position.Y < 0)
            {
                position.Y = height;
            }

            if (Hp <= 0)
            {
                //power ups
                num = randomNum.Next(0, 2);

                if (num > 0)
                {
                    Speed l = new Speed();
                    l.position = position;
                    l.LoadContent();
                    Gameplay.powers.Add(l);
                }
                Alive = false;
                Gameplay.playerScore = Gameplay.playerScore + score;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 cent = new Vector2();
            cent.X = Sprite.Width / 2;
            cent.Y = Sprite.Height / 2;

            Game1.Instance.spriteBatch.Draw(Sprite, position, null, Color.White, rot, cent, 1, SpriteEffects.None, 0.5f);
        }
    }
}
