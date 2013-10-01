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
    class PlayerEntity : DrawableEntity
    {
        public static float speed;
        float rot;
        float scale;
        public bool move;
        public static float fireRate;
        float elapsedTime;
        public List<Bullet> bList = new List<Bullet>();
        KeyboardState key;

        public PlayerEntity()
        {
            //set player at centre of screen
            speed = 100f;
            fireRate = 1.5f;
            rot = 0f;
            scale = 1f;
            position = new Vector2(350, 250);
            velocity = new Vector2(0, -1);
            elapsedTime = 10f;
            move = false;
        }

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("ship");
        }

        public override void Update(GameTime gameTime)
        {
            //player fire rate
            float hasToPass = 1.0f / fireRate;

            key = Keyboard.GetState();

            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if player move to edge of screen it will move it to other side
            if(position.X > width)
            {
                position.X = 0;
            }

            if(position.Y > height)
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

            //player controls
            if (key.IsKeyDown(Keys.Space) && (elapsedTime > hasToPass))
            {
                move = true;
                Bullet b = new Bullet();
                float shipgunDist = 20.0f;
                b.rotationAngle = rot;
                b.position = position + velocity * shipgunDist;
                b.velocity = velocity;
                b.LoadContent();
                bList.Add(b);
                elapsedTime = 0.0f;
            }
            elapsedTime += timeDelta;

            if (key.IsKeyDown(Keys.W))
            {
                position += speed * timeDelta * velocity;
                move = true;
            }

            if (key.IsKeyDown(Keys.S))
            {
                position -= speed * timeDelta * velocity;
                move = true;
            }

            if (key.IsKeyDown(Keys.A))
            {
                rot -= 2*timeDelta;
                move = true;
            }

            if (key.IsKeyDown(Keys.D))
            {
                rot += 2*timeDelta;
                move = true;
            }

            //update bullets
            for (int i = 0; i < bList.Count; i++)
            {
                bList[i].Update(gameTime);
                if (bList[i].Alive == false)
                {
                    bList.Remove(bList[i]);
                }
            }

            velocity.X = (float)Math.Sin(rot);
            velocity.Y = -(float)Math.Cos(rot);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 cent = new Vector2();
            cent.X = Sprite.Width / 8;
            cent.Y = Sprite.Height / 8;

            Game1.Instance.spriteBatch.Draw(Sprite, position, null, Color.White, rot, cent, scale, SpriteEffects.None, 0.5f);

            //Draw bullets
            for (int i = 0; i < bList.Count; i++)
            {
                bList[i].Draw(gameTime);
            }
        }

        public bool AsteroidCollision(Rectangle asteroidRect)
        {
            if (asteroidRect.Intersects(GetBoundingBox()))
            {
                Alive = false;
                return true;
            }
            return false;
        }
    }
}
