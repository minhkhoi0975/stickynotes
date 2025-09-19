using System.Drawing;
using System.Numerics;

namespace stickynotes
{
    public class StickyNote
    {
        public Vector2 Position;
        public Color BackgroundColor;
        public Color ToolStripColor;
        public string RawText = "";   // The text before it is formatted.
    }
}
