using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Entity[] entities = new Entity[16];

    Entity selectedEntity = null;
    World myWorld;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        
        // TODO: Add your initialization logic here
        myWorld = new World();
        myWorld.GenerateTileArray();

        Entity ball = new Entity();
        ball.tile = myWorld.TileArray[0,0];
        ball.speed = 100f;
        ball.objectKey = "ball";

        entities[0] = ball;


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        foreach (Entity ent in entities)
        {
            if (ent == null) continue;
            ent.texture = Content.Load<Texture2D>(ent.objectKey);
        }

        Texture2D tileTexture = Content.Load<Texture2D>("mytile");
        foreach(Tile tile in myWorld.TileArray)
        {
            tile.texture = tileTexture;
        }
        
    }

    protected override void Update(GameTime gameTime)
    {
        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //    Exit()
        var kstate = Keyboard.GetState();
        var mstate = Mouse.GetState();

        if (mstate.LeftButton == ButtonState.Pressed)
        {
            foreach (Entity ent in entities)
            {
                if (ent == null) continue;
                bool changedEntity = false;
                if (LinearAlgebraHelpers.IsPointInEntity(mstate.Position, ent)) 
                {
                    selectedEntity = ent;
                    changedEntity = true;
                }
                else if (!changedEntity) selectedEntity = null;
            }
        
        }

        if (mstate.RightButton == ButtonState.Pressed)
        {

            if (selectedEntity != null && (selectedEntity.tile != myWorld.GetTileAtPoint(mstate.Position)))
            {
                selectedEntity.destination = myWorld.GetTileAtPoint(mstate.Position);
            }
        }
        
        foreach (Entity ent in entities)
        {
            if (ent == null) continue;
            if (selectedEntity?.destination != null) selectedEntity.Move(gameTime.ElapsedGameTime.TotalSeconds);
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

        foreach (Entity ent in entities)
        {
            if (ent == null) continue;
            if(ent == selectedEntity) {
                _spriteBatch.Draw(ent.texture, color: Color.AntiqueWhite, position: ent.pos);
            }
            else _spriteBatch.Draw(ent.texture, ent.pos, Color.White);
        }
        _spriteBatch.End();
        

        base.Draw(gameTime);
    }
}
