using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Asteroid
{
    public class Bullet : DrawableEntity
    {
        public float rotationAngle;
        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("bullet");
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 500f;

            position += velocity * timeDelta * speed;

            //if goes out of screen edge it get remove from list
            if ((position.X > width) || (position.X < 0)
                || (position.Y > height) || (position.Y < 0))
            {
                Alive = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(Sprite, position, null, Color.White, rotationAngle, new Vector2(Sprite.Width/8, Sprite.Height/8), 1.0f, SpriteEffects.None, 0f);
        }

        public void BulletCollision(Rectangle bulletRect)
        {
            if (bulletRect.Intersects(GetBoundingBox()))
            {
                Alive = false;
            }
        }
    }
}
