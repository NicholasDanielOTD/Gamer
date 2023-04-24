using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyGame {

    public class World {

        public Tile[,] TileArray = new Tile[16, 16];
        public Entity[] entities = new Entity[16];
        public Entity selectedEntity = null;
        public int xbound = 0;
        public int ybound = 0;

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
            for(int x = 0; x < xbound; x++)
            {
                for (int y = 0; y < ybound; y++)
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
                if (ent.GetTile() == tile) return ent;
            }

            return null;
        }

        public bool CanEntMoveToTile(Entity ent, Tile tile)
        {
            return tile.CanBeMovedTo();
        }

        public (Entity ent, Tile tile) GetThingAtPoint(Point pos)
        {
            foreach (Entity ent in this.entities)
            {
                if (ent == null) continue;
                if (LinearAlgebraHelpers.IsPointInEntity(pos, ent)) return (ent, ent.GetTile());
            }
            return (null, GetTileAtPoint(pos));
        }

    }

    public class Tile {

        public Vector2 pos;
        public int width = 64;
        public int height = 64;
        public Texture2D texture;
        public string TextureKey = "mytile";
        public int WorldX;
        public int WorldY;
        private Entity occupyingEntity;

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
            if (selectedEntity == null) return;
            Tile selEntTile = selectedEntity.GetTile();
            if (selectedEntity != null && 
                (!selectedEntity.IsMoving) &&
                (selectedEntity.destination == null) && 
                (selEntTile != this) &&
                (myWorld.CanEntMoveToTile(selectedEntity, this)))
            {
                selectedEntity.path = Pathfinding.AStar(selEntTile, this, myWorld);
                selectedEntity.IsMoving = true;
            }
        }

        public Tile[] GetNeighbors(World world)
        {
            Tile[] neighbors = new Tile[4];
            int upperYBound = world.ybound;
            int lowerYBound = 0;
            int upperXBound = world.xbound;
            int lowerXBound = 0;
            if (!(WorldX+1 >= upperXBound)) neighbors[0] = world.TileArray[WorldX+1,WorldY]; 
            if (!(WorldY+1 >= upperYBound)) neighbors[1] = world.TileArray[WorldX,WorldY+1]; 
            if (!(WorldX-1 < lowerXBound)) neighbors[2] = world.TileArray[WorldX-1,WorldY]; 
            if (!(WorldY-1 < lowerYBound)) neighbors[3] = world.TileArray[WorldX,WorldY-1]; 

            return neighbors;
        }

        public bool CanBeMovedTo()
        {
            return (this.occupyingEntity == null);
        }

        public void onHover(World world)
        {
            if ((world.selectedEntity != null) && ((world.selectedEntity.path == null) || (!world.selectedEntity.IsMoving && (world.selectedEntity.path.goal != this))))
            {
                world.selectedEntity.path = Pathfinding.AStar(world.selectedEntity.GetTile(), this, world);
            }
        }

        public bool UpdateEntity(Entity ent)
        {
            if (ent == null) this.occupyingEntity = null;
            else if (this.occupyingEntity != null) return false;
            else this.occupyingEntity = ent;
            return true;
        }
    }
}
