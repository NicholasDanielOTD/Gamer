using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyGame {
    public class Entity : IClickable {

        public Texture2D defaultTexture {get; set;}
        public Texture2D texture {get; set;}
        public Vector2 pos {get; set;}
        public float speed = 100f;
        public Tile destination {get; set;}
        public string objectKey {get; set;}
        public Pathfinding.Path path {get; set;}
        public bool IsMoving = false;
        private Tile tile  {get; set;}

        public void SetTile(Tile tile)
        {
            if (tile.UpdateEntity(this))
            {
                this.tile?.UpdateEntity(null);
                this.tile = tile;
            } 
            return;
        }

        public Tile GetTile()
        {
            return this.tile;
        }

        public void Move(double elapsedTime)
        {
            if (IsMoving && (destination == null) && (path.path.Count > 0)) destination = path.path[0];
            if (destination == null || !IsMoving) return;
            
            if (pos != destination.pos) // Entity needs to move
            {
                if (pos.X != destination.pos.X && Math.Abs(pos.X - destination.pos.X) < 4) pos = new Vector2(destination.pos.X, pos.Y);
                else if (pos.Y != destination.pos.Y &&Math.Abs(pos.Y - destination.pos.Y) < 4) pos = new Vector2(pos.X, destination.pos.Y);
                else if (pos.X > destination.pos.X) pos = new Vector2(pos.X - (float)elapsedTime * speed, pos.Y);
                else if (pos.X < destination.pos.X) pos = new Vector2(pos.X + (float)elapsedTime * speed, pos.Y);
                else if (pos.Y > destination.pos.Y) pos = new Vector2(pos.X, pos.Y - (float)elapsedTime * speed);
                else if (pos.Y < destination.pos.Y) pos = new Vector2(pos.X, pos.Y +  (float)elapsedTime * speed);
            }
            else // Entity is done moving
            {
                tile = destination;
                destination = null;
                path.path.RemoveAt(0);
                if (path.path.Count == 0) {this.path = null; IsMoving = false;}
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(this.texture, this.pos, Color.White);
        }

        public void onLeftClick(World myWorld)
        {
            myWorld.selectedEntity = this;
        }

        public void onRightClick()
        {

        }

        public void onHover()
        {

        }
    }
}