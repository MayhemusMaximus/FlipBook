using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{

    public class Toolbar : BaseScreen
    {

        public List<Button> Buttons = new List<Button>();

        public Toolbar()
        {
            this.Name = "Toolbar";
            this.Position = new Vector2(0, 0);
            this.Size = new Vector2(Globals.Graphics.PreferredBackBufferWidth, 30);
        }

        public override void Unload()
        {
            
        }

        public void Select(String button, Boolean select)
        {
            foreach(Button btn in Buttons)
            {
                if(button == btn.Name)
                {
                    btn.IsSelected = select;
                    return;
                }
            }
        }

        public override void Update()
        {
            foreach(Button button in Buttons)
            {
                button.Update();
            }

        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Textures.SimpleTexture, this.Bounds, Color.Wheat);

            foreach(Button button in Buttons)
            {
                button.Draw();
            }
        }
    }
}
