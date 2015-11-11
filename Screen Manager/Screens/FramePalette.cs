using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FlipBook
{
    public class FramePalette : BaseScreen
    {
        public FramePalette(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Textures.texture, this.Bounds, Color.DarkGray);
        }
    }
}
