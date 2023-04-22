using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Testing;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

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
        foreach (Tile tile in myWorld.TileArray)
        {
            tile.texture = Content.Load<Texture2D>(tile.textureKey);
        }

    }

    protected override void Update(GameTime gameTime)
    {
        CheckInputs(gameTime);
        MoveEntities(gameTime);
        base.Update(gameTime);
    }

    protected void CheckInputs(GameTime gameTime)
    {
        var kstate = Keyboard.GetState();
        var mstate = Mouse.GetState();

        if (mstate.LeftButton == ButtonState.Pressed) HandleLeftClick(mstate);
        if (mstate.RightButton == ButtonState.Pressed) HandleRightClick(mstate);
    }

    protected void HandleLeftClick(MouseState mstate)
    {
        (Entity, Tile) tup = myWorld.GetThingAtPoint(mstate.Position);
        if (tup.Item1 != null) tup.Item1.onLeftClick(myWorld);
        else if (tup.Item2 != null) tup.Item2.onLeftClick(myWorld);
    }

    protected void HandleRightClick(MouseState mstate)
    {
        (Entity, Tile) tup = myWorld.GetThingAtPoint(mstate.Position);
        if (tup.Item1 != null) tup.Item1.onRightClick();
        else if (tup.Item2 != null) tup.Item2.onRightClick(myWorld);
    }

    protected void MoveEntities(GameTime gameTime)
    {
        foreach (Entity ent in myWorld.entities)
        {
            if (ent == null) continue;
            if (ent.destination != null) ent.Move(gameTime.ElapsedGameTime.TotalSeconds);
        } 
    }

    protected override void Draw(GameTime gameTime)
    {
        Color backgroundColor = Color.Beige;
        GraphicsDevice.Clear(backgroundColor);
        _spriteBatch.Begin();
        
        // Draw a world of tiles
        foreach(Tile tile in myWorld.TileArray)
        {
            if (tile == null) continue;
            tile.Draw(_spriteBatch);
        }

        // Draw entities
        foreach (Entity ent in myWorld.entities)
        {
            if (ent == null) continue;
            if (ent == myWorld.selectedEntity) DrawHighlightedEntity(ent);
            ent.Draw(_spriteBatch);
        }


        _spriteBatch.End();
        base.Draw(gameTime);
    }

    protected void DrawHighlightedEntity(Entity ent)
    {
        Texture2D highlight = new Texture2D(GraphicsDevice, 64, 64);
        Color[] data = new Color[64*64];
        for (int i=0; i < data.Length; i++) data[i] = Color.Crimson;
        highlight.SetData(data);
        _spriteBatch.Draw(highlight, new Vector2((int)ent.pos.X, (int)ent.pos.Y), Color.Red * .5f);
    }
}
