using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FlipBook
{
    public enum MouseButtonState
    {
        Pressed,
        Released,
        Held,
        Inactive
    }
    public static class Input
    {
        //private static KeyboardState currentKeyboardState;
        //private static KeyboardState previousKeyboardState;

        private static MouseState currentMouseState;
        private static MouseState previousMouseState;

        public static KeyboardState CurrentKeyboardState { get; set; }
        public static KeyboardState PreviousKeyboardState { get; set; }
        public static MouseButtonState MouseLeftButtonState {get; set;}

        public static MouseState CurrentMouseState
        {
            get { return currentMouseState; }
            set 
            {
                currentMouseState = value;
                CurrentMousePosition = new Point(currentMouseState.X, currentMouseState.Y);
            }
        }

        public static MouseState PreviousMouseState
        {
            get { return previousMouseState; }
            set
            {
                previousMouseState = value;
                PreviousMousePosition = new Point(previousMouseState.X, previousMouseState.Y);
            }
        }

        

        public static Point CurrentMousePosition { get; private set; }
        public static Point PreviousMousePosition { get; private set; }
    }
}
