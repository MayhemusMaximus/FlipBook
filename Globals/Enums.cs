using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public enum MouseButtonState
    {
        Pressed,
        Released,
        Held,
        Inactive
    }

    public enum DrawMode
    {
        Pencil,
        Eraser,
        Line,
        Fill,
        Rectangle
    }
}
