using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyGame {
    public class Entity {

        public Texture2D defaultTexture;
        public Texture2D texture;
        public Vector2 pos;
        public float speed = 100f;
        public Tile destination;
        public string objectKey;
        public Pathfinding.Path path;
        public bool IsMoving = false;
        private Tile tile;

        public void SetTile(Tile tile)
        {
            if (tile.UpdateEntity(this)) this.tile = tile;
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
                if (pos.X != destination.pos.X && Math.Abs(pos.X - destination.pos.X) < 4) pos.X = destination.pos.X;
                else if (pos.Y != destination.pos.Y &&Math.Abs(pos.Y - destination.pos.Y) < 4) pos.Y = destination.pos.Y;
                else if (pos.X > destination.pos.X) pos.X -= (float)elapsedTime * speed;
                else if (pos.X < destination.pos.X) pos.X += (float)elapsedTime * speed;
                else if (pos.Y > destination.pos.Y) pos.Y -= (float)elapsedTime * speed;
                else if (pos.Y < destination.pos.Y) pos.Y += (float)elapsedTime * speed;
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