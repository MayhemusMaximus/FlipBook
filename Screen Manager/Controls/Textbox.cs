using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class Textbox : Control
    {

        public Textbox(String name, Vector2 position, Vector2 size)
        {
            Name = name;
            Position = position;
            Size = size;
        }

        public override void Load()
        {

        }

        public override void Unload()
        {
            //base.Unload();
        }

        public override void Update()
        {
            //if()
        }

        public override void Draw()
        {
            //base.Draw();
            Globals.SpriteBatch.Draw(Textures.TextBox, Bounds, Color.White);
        }
    }
}
