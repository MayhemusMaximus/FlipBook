using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class BaseScreen
    {
        public String Name { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Rectangle Bounds { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); } }


        public virtual void Load()
        {

        }

        public virtual void Unload()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
