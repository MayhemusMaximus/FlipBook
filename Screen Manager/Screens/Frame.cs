using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class Frame
    {
        public int Sequence { get; set; }
        public String Name { get; set; }
        public Boolean IsActive { get; set; }
        public int Time { get; set; }

        public Grid Grid = new Grid(new Vector2(0, 0), Globals.ImageSize);
    }
}
