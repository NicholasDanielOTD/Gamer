﻿using Microsoft.Xna.Framework;
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
        ball = new Entity();
        ball.pos = new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight/2);
        ball.speed = 100f;
        ballSelected = false;

        myWorld = new World();
        myWorld.GenerateTileArray();


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

        if (mstate.LeftButton == ButtonState.Pressed && LinearAlgebraHelpers.IsPointInEntity(mstate.Position, ball)) ballSelected = true;
        else if (mstate.LeftButton == ButtonState.Pressed) ballSelected = false;

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
