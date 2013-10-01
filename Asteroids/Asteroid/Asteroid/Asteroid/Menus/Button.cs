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
    class Button : DrawableEntity
    {
        Rectangle rectangle;

        Color colour = new Color(128, 128, 128, 255);

        public Vector2 size;

        public Button(Texture2D newTexture, GraphicsDevice graphics, Vector2 newPosition)
        {
            texture = newTexture;

            position = newPosition;

            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 20);
        }

        public override void LoadContent()
        {
            //throw new NotImplementedException();
        }

        bool hoverOver;
        public bool isClicked;
        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);
            //mouse rectangle goes into button
            if (mouseRec.Intersects(rectangle))
            {
                if (colour.A == 255)
                {
                    hoverOver = false;
                }
                if (colour.A == 0)
                {
                    hoverOver = true;
                }
                if (hoverOver)
                {
                    colour.A += 3;
                }
                else
                {
                    colour.A -= 3;
                }
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }
            }
            else if (colour.A < 255)
            {
                colour.A += 3;
                isClicked = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(texture, rectangle, colour);
        }
    }
}
