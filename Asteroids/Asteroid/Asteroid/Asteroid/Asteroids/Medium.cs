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
    class Medium : Asteroids
    {
        Random randomNum = new Random();
        int num;
        float speed;
        float rot;
        public Medium()
        {
            rot = 1f;
            speed = 0f;
            Hp = 3;
            score = 100;
        }

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("medium");
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            rot += 2 * timeDelta;

            if (speed < 0.20)
            {
                speed += 0.03f;
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
                position.X = width;
            }

            if (position.Y < 0)
            {
                position.Y = height;
            }
            //if a medium asteroid gets destory it will create two small one in its place
            if (Hp <= 0)
            {
                //power ups
                num = randomNum.Next(0, 2);

                if (num > 0)
                {
                    Firerate l = new Firerate();
                    l.position = position;
                    l.LoadContent();
                    Gameplay.powers.Add(l);
                }

                Gameplay.playerScore = Gameplay.playerScore + score;
                Alive = false;
                Small a = new Small();
                a.position = position;
                a.velocity = velocity;
                Small b = new Small();
                b.position = position + new Vector2(10, 10);
                b.velocity = velocity + new Vector2(10, 10);
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
