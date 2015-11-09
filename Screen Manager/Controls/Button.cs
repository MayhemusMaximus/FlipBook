using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public class Button : BaseScreen
    {
        public String Text { get; set; }
        public Boolean Clicked { get; private set; }
        private Boolean isMouseOver;
        public Boolean IsMouseOver
        {
            get { return isMouseOver; }
            private set
            {
                isMouseOver = value;
            }
        }
        public Boolean IsSelected { get; set; }
        public Boolean HasImage { get; private set; }
        private Texture2D image;
        public Texture2D Image
        {
            get { return image; }
            set
            {
                if(value != null)
                {
                    image = value;
                    HasImage = true;
                }
                else
                {
                    image = null;
                    HasImage = false;
                }
            }
        }

        public Button()
        {
            this.Name = "Button";
            this.Position = new Vector2(0, 0);
            this.Size = new Vector2(20, 50);
            this.Text = this.Name;
        }

        public Button(Vector2 position, Vector2 size)
        {
            this.Name = "Button";
            this.Position = position;
            this.Size = size;
            this.Text = this.Name;
        }

        public override void Update()
        {
            HandleMouseHover();
            if(IsMouseOver)
                HandleMouseClick();
        }

        private void HandleMouseHover()
        {
            if (this.Bounds.Contains(Input.CurrentMousePosition))
                IsMouseOver = true;
            else
                IsMouseOver = false;
        }

        public void HandleMouseClick()
        {
            if (Clicked == true)
                Clicked = false;

            if (Input.CurrentMouseState.LeftButton == ButtonState.Released && Input.PreviousMouseState.LeftButton == ButtonState.Pressed)
                Clicked = true;
        }

        public override void Draw()
        {
            if (IsMouseOver)
                Globals.SpriteBatch.Draw(Textures.TextBox, this.Bounds, Color.LightGray);
            else if (IsSelected)
                Globals.SpriteBatch.Draw(Textures.TextBox, this.Bounds, Color.LightPink);
            else
                Globals.SpriteBatch.Draw(Textures.TextBox, this.Bounds, Color.White);
            if (HasImage)
            {
                Globals.SpriteBatch.Draw(image, new Rectangle((this.Bounds.Width / 2) - (this.image.Width / 2) + this.Bounds.X, (this.Bounds.Height / 2) - (this.image.Height) / 2 + this.Bounds.Y, this.image.Width, this.image.Height), Color.White);
            }
            else
            {
                Vector2 fontSize = SpriteFonts.Arial_8.MeasureString(this.Text);
                Vector2 fontPosition = new Vector2((this.Bounds.Width / 2) - (fontSize.X / 2) + this.Bounds.X, (this.Bounds.Height / 2) - (fontSize.Y / 2) + this.Bounds.Y);
                Globals.SpriteBatch.DrawString(SpriteFonts.Arial_8, this.Text, fontPosition, Color.Black);
            }
        }
    }
}
