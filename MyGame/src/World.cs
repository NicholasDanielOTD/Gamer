using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {

    public class World {

        public Tile[,] TileArray = new Tile[16, 16];
        public Entity[] entities = new Entity[16];
        public Entity selectedEntity = null;

        public Tile GetTileAtPoint(Point point)
        {
            int x = point.X / 64;
            int y = point.Y / 64;
            if ((x < 0) || (y < 0)) return null;
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
                    newTile.WorldX = x;
                    newTile.WorldY = y;
                }
            }
        }

        public Entity GetEntityAtTile(Tile tile)
        {
            foreach (Entity ent in entities)
            {
                if (ent == null) continue;
                if (ent.tile == tile) return ent;
            }

            return null;
        }

        public bool CanEntMoveToTile(Entity ent, Tile tile)
        {
            if(GetEntityAtTile(tile) != null) return false;
            if(tile.entityOccupier != null) return false;
            return true;
        }

        public (Entity ent, Tile tile) GetThingAtPoint(Point pos)
        {
            foreach (Entity ent in this.entities)
            {
                if (ent == null) continue;
                if (LinearAlgebraHelpers.IsPointInEntity(pos, ent)) return (ent, ent.tile);
            }
            return (null, GetTileAtPoint(pos));
        }

    }

    public class Tile {

        public Vector2 pos;
        public int width = 64;
        public int height = 64;
        public Texture2D texture;
        public string textureKey = "mytile";
        public Entity entityOccupier = null;
        public int WorldX;
        public int WorldY;

        public bool ShouldDraw()
        {
            return true;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(this.texture, this.pos, Color.White);
        }

        public void onLeftClick(World myWorld)
        {
            myWorld.selectedEntity = null;
        }

        public void onRightClick(World myWorld)
        {
            Entity selectedEntity = myWorld.selectedEntity;
            if (selectedEntity != null && selectedEntity.destination == null && 
            (selectedEntity.tile != this) &&
            (myWorld.CanEntMoveToTile(selectedEntity, this)))
            {
                selectedEntity.path = Pathfinding.AStar(selectedEntity.tile, this, myWorld);
                this.entityOccupier = selectedEntity;
                selectedEntity.tile.entityOccupier = null;
            }
        }

        public Tile[] GetNeighbors(World world)
        {
            Tile[] neighbors = new Tile[4];
            if ((WorldX + 1)*WorldY < world.TileArray.Length) neighbors[0] = world.TileArray[WorldX+1,WorldY]; 
            if ((WorldX - 1)*WorldY > 0) neighbors[1] = world.TileArray[WorldX-1,WorldY]; 
            if ((WorldY + 1)*WorldX < world.TileArray.Length) neighbors[2] = world.TileArray[WorldX,WorldY+1]; 
            if ((WorldY - 1)*WorldX > 0) neighbors[3] = world.TileArray[WorldX,WorldY-1]; 

            return neighbors;
        }

        public bool CanBeMovedTo()
        {
            return (entityOccupier == null);
        }
    }
}
