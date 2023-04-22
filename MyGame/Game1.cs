using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Testing;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Entity selectedEntity = null;
    World myWorld = null;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        (myWorld) = Levels.InitializeMainDebug();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        foreach (Entity ent in myWorld.entities)
        {
            if (ent == null) continue;
            ent.texture = Content.Load<Texture2D>(ent.objectKey);
        }

        foreach(Tile tile in myWorld.TileArray)
        {
            tile.texture = Content.Load<Texture2D>(tile.textureKey);
        }

    }

    protected override void Update(GameTime gameTime)
    {
        var kstate = Keyboard.GetState();
        var mstate = Mouse.GetState();

        if (mstate.LeftButton == ButtonState.Pressed)
        {
            bool changedEntity = false;
            foreach (Entity ent in myWorld.entities)
            {
                if (ent == null) continue;
                if (!changedEntity && LinearAlgebraHelpers.IsPointInEntity(mstate.Position, ent)) 
                {
                    selectedEntity = ent;
                    changedEntity = true;
                }
                else if (!changedEntity) selectedEntity = null;
            }
        
        }

        if (mstate.RightButton == ButtonState.Pressed)
        {
            Tile clickedTile = myWorld.GetTileAtPoint(mstate.Position);
            if (selectedEntity != null && selectedEntity.destination == null && 
            (selectedEntity.tile != clickedTile) &&
            (myWorld.CanEntMoveToTile(selectedEntity, clickedTile)))
            {
                selectedEntity.destination = clickedTile;
            }
        }
        
        foreach (Entity ent in myWorld.entities)
        {
            if (ent == null) continue;
            if (ent.destination != null) ent.Move(gameTime.ElapsedGameTime.TotalSeconds);
        } 

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Color backgroundColor = Color.Beige;
        GraphicsDevice.Clear(backgroundColor);

        
        // Draw a world of tiles

        _spriteBatch.Begin();
        foreach(Tile tile in myWorld.TileArray)
        {
            _spriteBatch.Draw(tile.texture, tile.pos, Color.White);
        }

        // Draw entities

        foreach (Entity ent in myWorld.entities)
        {
            if (ent == null) continue;
            if(ent == selectedEntity) {
                Texture2D highlight = new Texture2D(GraphicsDevice, 64, 64);
                Color[] data = new Color[64*64];
                for (int i=0; i < data.Length; i++) data[i] = Color.Crimson;
                highlight.SetData(data);
                _spriteBatch.Draw(highlight, new Vector2((int)ent.pos.X, (int)ent.pos.Y), Color.Red * .5f);
            }
            _spriteBatch.Draw(ent.texture, ent.pos, Color.White);
        }
        _spriteBatch.End();
        
        

        base.Draw(gameTime);
    }
}
