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
    public abstract class DrawableEntity
    {
        public int width = Game1.Instance.Width;
        public int height = Game1.Instance.Height;
        protected Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        private Texture2D sprite;
        private bool alive = true;
        private float speed;
        private int hp;

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                Sprite.Width,
                Sprite.Height);
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }



}
