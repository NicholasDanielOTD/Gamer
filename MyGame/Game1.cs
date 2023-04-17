using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Entity ball;

    World myWorld;

    bool ballSelected;

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

        ball = new Entity();
        ball.tile = myWorld.TileArray[0,0];
        ball.speed = 100f;
        ballSelected = false;


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);


        ball.texture = Content.Load<Texture2D>("ball");
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
        if (LinearAlgebraHelpers.IsPointInEntity(mstate.Position, ball)) ballSelected = true;
        else ballSelected = false;
        }

        if (mstate.RightButton == ButtonState.Pressed)
        {
            if (ballSelected && (ball.tile != myWorld.GetTileAtPoint(mstate.Position)))
            {
                ball.destination = myWorld.GetTileAtPoint(mstate.Position);
            }
        }
        
        if(ball.destination != null) ball.Move(gameTime.ElapsedGameTime.TotalSeconds);

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
        if(ballSelected) _spriteBatch.Draw(ball.texture, ball.pos, Color.Red);
        else _spriteBatch.Draw(ball.texture, ball.pos, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
