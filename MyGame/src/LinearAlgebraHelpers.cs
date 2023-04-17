using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
    public class LinearAlgebraHelpers 
    {
        static public bool IsPointInEntity(Point point, Entity entity)
        {
            bool xIn = ((point.X >= entity.pos.X) && (point.X <= (entity.pos.X + entity.texture.Width)));
            bool yIn = ((point.Y >= entity.pos.Y) && (point.Y <= (entity.pos.Y + entity.texture.Height)));

            return xIn && yIn;
        }
    }
    
}