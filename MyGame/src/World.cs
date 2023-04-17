using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {

    public class World {

        public Tile[,] TileArray = new Tile[16, 16];

        public Tile? GetTileAtPoint(Point point)
        {
            int x = point.X / 64;
            int y = point.Y / 64;
            if (x < 16 && y < 16) return TileArray[x,y];
            return null;
        }

        public void GenerateTileArray()
        {
            for(int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    Tile newTile = new Tile();
                    newTile.pos = new Vector2(x*64,y*64);
                    TileArray[x,y] = newTile;
                }
            }
        }

    }

    public class Tile {

        public Vector2 pos;
        public int width = 64;
        public int height = 64;
        public Texture2D texture;

        public bool ShouldDraw()
        {
            return true;
        }

    }

}
