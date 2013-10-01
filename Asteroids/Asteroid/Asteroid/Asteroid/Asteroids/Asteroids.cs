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
    abstract class Asteroids : DrawableEntity
    {
        public int score;

        public void BulletCollision(Rectangle bulletRect)
        {
            if (bulletRect.Intersects(GetBoundingBox()))
            {
                Hp -= 1;
            }
        }

        public void ShieldCollision(Rectangle shieldRect)
        {
            Rectangle top = new Rectangle(shieldRect.X, shieldRect.Y, shieldRect.Width, 1);
            Rectangle right = new Rectangle(shieldRect.X + shieldRect.Width, shieldRect.Y, 1, shieldRect.Height);
            Rectangle bottom = new Rectangle(shieldRect.X, shieldRect.Y + shieldRect.Height, shieldRect.Width, 1);
            Rectangle left = new Rectangle(shieldRect.X, shieldRect.Y, 1, shieldRect.Height);

            if (top.Intersects(GetBoundingBox()))
            {
                position.Y = shieldRect.Y - GetBoundingBox().Height - 1;
                velocity.Y *= -1;
            }
            else if (right.Intersects(GetBoundingBox()))
            {
                position.X = shieldRect.X + shieldRect.Width + 1;
                velocity.X *= -1;
            }
            else if (bottom.Intersects(GetBoundingBox()))
            {
                position.Y = shieldRect.Y + shieldRect.Height + 1;
                velocity.Y *= -1;
            }
            else if (left.Intersects(GetBoundingBox()))
            {
                position.X = shieldRect.X - GetBoundingBox().Width - 1;
                velocity.X *= -1;
            }
        }

        public bool AsteroidCollision(Rectangle asteroidRect)
        {
            Rectangle top = new Rectangle(asteroidRect.X, asteroidRect.Y, asteroidRect.Width, 1);
            Rectangle right = new Rectangle(asteroidRect.X + asteroidRect.Width, asteroidRect.Y, 1, asteroidRect.Height);
            Rectangle bottom = new Rectangle(asteroidRect.X, asteroidRect.Y + asteroidRect.Height, asteroidRect.Width, 1);
            Rectangle left = new Rectangle(asteroidRect.X, asteroidRect.Y, 1, asteroidRect.Height);

            if (top.Intersects(GetBoundingBox()))
            {
                position.Y = asteroidRect.Y - GetBoundingBox().Height - 1;
                velocity.Y *= -1;
                return true;
            }
            else if (right.Intersects(GetBoundingBox()))
            {
                position.X = asteroidRect.X + asteroidRect.Width + 1;
                velocity.X *= -1;
                return true;
            }
            else if (bottom.Intersects(GetBoundingBox()))
            {
                position.Y = asteroidRect.Y + asteroidRect.Height + 1;
                velocity.Y *= -1;
                return true;
            }
            else if (left.Intersects(GetBoundingBox()))
            {
                position.X = asteroidRect.X - GetBoundingBox().Width - 1;
                velocity.X *= -1;
                return true;
            }
            return false;
        }
    }
}
