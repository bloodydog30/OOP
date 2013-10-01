using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid
{
    class Generator
    {
        //asteroid generator and place it into a list
        Random randomNum = new Random();
        int num;
        public static List<Asteroids> asteroidsentities = new List<Asteroids>();
        
        public Generator()
        {
            for (int i = 0; i < 10; i++)
            {
                generate();
            }
        }

        public void generate()
        {
            num = randomNum.Next(0, 100);

            if(num < 50)
            {
                Small a = new Small();
                a.position.X = randomNum.Next(0, Game1.Instance.Width);
                a.position.Y = randomNum.Next(0, Game1.Instance.Height);
                a.velocity.X = randomNum.Next(0, Game1.Instance.Width);
                a.velocity.Y = randomNum.Next(0, Game1.Instance.Height);
                a.LoadContent();
                asteroidsentities.Add(a);
            }
            if (num > 50 && num < 75)
            {
                Medium a = new Medium();
                a.position.X = randomNum.Next(0, Game1.Instance.Width);
                a.position.Y = randomNum.Next(0, Game1.Instance.Height);
                a.velocity.X = randomNum.Next(0, Game1.Instance.Width);
                a.velocity.Y = randomNum.Next(0, Game1.Instance.Height);
                a.LoadContent();
                asteroidsentities.Add(a);
            }
            if(num > 75)
            {
                Large a = new Large();
                a.position.X = randomNum.Next(0, Game1.Instance.Width);
                a.position.Y = randomNum.Next(0, Game1.Instance.Height);
                a.velocity.X = randomNum.Next(0, Game1.Instance.Width);
                a.velocity.Y = randomNum.Next(0, Game1.Instance.Height);
                a.LoadContent();
                asteroidsentities.Add(a);
            }
        }
    }
}
