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
    class Shield : DrawableEntity
    {
        public Shield()
        {
            position = new Vector2(340, 242);

        }

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("shield");
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException(); 
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(Sprite, position, null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0.4f);
        }
    }
}
