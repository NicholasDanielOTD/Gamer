using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
    public class LinearAlgebraHelpers 
    {
        static public bool IsPointInClickable(Point point, IClickable clickable)
        {
            if (clickable == null || clickable.pos == null || clickable.texture == null) return false;
            bool xIn = ((point.X >= clickable.pos.X) && (point.X <= (clickable.pos.X + clickable.texture.Width)));
            bool yIn = ((point.Y >= clickable.pos.Y) && (point.Y <= (clickable.pos.Y + clickable.texture.Height)));

            return xIn && yIn;
        }
    }
    
}