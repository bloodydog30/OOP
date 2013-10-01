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
    class Large : Asteroids
    {
        Random randomNum = new Random();
        int num;
        float speed;
        float rot;
        public Large()
        {
            rot = 1f;
            speed = 0f;
            Hp = 5;
            score = 1000;
        }

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("large");
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            rot += timeDelta;
            
            if(speed < 0.10)
            {
                speed += 0.01f;
            }

            position += velocity * timeDelta * speed;

            //Move asteroid to opposite side if go to edge
            if (position.X > Game1.Instance.Width)
            {
                position.X = 0;
            }

            if (position.Y > Game1.Instance.Height)
            {
                position.Y = 0;
            }

            if (position.X < 0)
            {
                position.X = Game1.Instance.Width;
            }

            if (position.Y < 0)
            {
                position.Y = Game1.Instance.Height;
            }
            //if a large asteroid gets destory it will create two medium one in its place
            if (Hp <= 0)
            {
                //power ups
                num = randomNum.Next(0, 2);

                if (num > 0)
                {
                    Life l = new Life();
                    l.position = position;
                    l.LoadContent();
                    Gameplay.powers.Add(l);
                }
                Gameplay.playerScore = Gameplay.playerScore + score;
                Alive = false;
                Medium a = new Medium();
                a.position = position;
                a.velocity = velocity;
                Medium b = new Medium();
                b.position = position + new Vector2(20, 20);
                b.velocity = velocity + new Vector2(20, 20);
                a.LoadContent();
                b.LoadContent();
                Generator.asteroidsentities.Add(a);
                Generator.asteroidsentities.Add(b);
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
