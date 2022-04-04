using Microsoft.Xna.Framework;

namespace DeveBlockStacker.Core.Helpers
{
    public static class RectangleExtensions
    {
        public static Rectangle AddXYPos(this Rectangle cur, Rectangle other)
        {
            return new Rectangle(cur.X + other.X, cur.Y + other.Y, cur.Width, cur.Height);
        }
    }
}
