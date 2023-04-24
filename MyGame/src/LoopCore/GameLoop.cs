using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Menu;
using MyGame.Testing;

namespace MyGame;

public partial class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    World myWorld = null;
    GameMenu myMenu = null;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        (myWorld) = Levels.InitializeMainDebug();
        myMenu = new GameMenu();

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
            tile.texture = Content.Load<Texture2D>(tile.TextureKey);
        }

        myMenu.texture = new Texture2D(GraphicsDevice, 1024, 1024);

    }

    protected override void Update(GameTime gameTime)
    {
        CheckInputs(gameTime);

        if (!myMenu.IsOpen())
        {
            MoveEntities(gameTime);
        }

        base.Update(gameTime);
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
            if (ent == myWorld.selectedEntity) {
                DrawHighlightedEntity(ent);
                if (ent.path != null)
                {
                    foreach (Tile tile in ent.path.path)
                    {
                        if (tile == ent.GetTile()) continue;
                        Texture2D highlight = new Texture2D(GraphicsDevice, 64, 64);
                        Color[] data = new Color[64*64];
                        for (int i=0; i < data.Length; i++) data[i] = Color.Aquamarine;
                        highlight.SetData(data);
                        _spriteBatch.Draw(highlight, new Vector2((int)tile.pos.X, (int)tile.pos.Y), Color.Aquamarine * .5f);
                    }
                }
                }
            ent.Draw(_spriteBatch);
        }
        
        myMenu.DrawMenu(_spriteBatch);


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
