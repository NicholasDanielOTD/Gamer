using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MyGame.Menu;

public class GameMenu : World {

    private bool isOpen = false;
    public Texture2D texture;
    private List<Option> options;
    public GameMenu()
    {

    }

    public override void ClickClickablesAtPoint(Point pos, MouseState mstate, KeyboardState kstate)
    {
        foreach (Option opt in options) if (LinearAlgebraHelpers.IsPointInClickable(pos, opt)) foreach (Action<World, MouseState, KeyboardState> onClick in opt.onClick) onClick(this, mstate, kstate);
        
    }
    public void AddOption(params Option[] options)
    {
        this.options = new List<Option>();
        this.options.AddRange(options);
    }
    public void ToggleOpen()
    {
        isOpen = !isOpen;
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public void DrawMenu(SpriteBatch _spriteBatch)
    {
        if (!isOpen) return;
        Color[] data = new Color[1024*1024];
        for (int i=0; i < data.Length; i++) data[i] = Color.Black;
        this.texture.SetData(data);
        _spriteBatch.Draw(this.texture, new Vector2(0, 0), Color.Black * .5f);

        foreach(Option opt in options)
        {
            opt.Draw(_spriteBatch, new Vector2(100,100));
        }
    }

}

public class Option : IClickable {
    
    public Texture2D texture {get; set;}
    public Vector2 pos {get; set;}
    public Action<World, MouseState, KeyboardState>[] onClick {get; set;}

    public Option(Texture2D texture, Action<World, MouseState, KeyboardState> action)
    {
        this.texture = texture;
        Color[] data = new Color[64*64];
        for (int i=0; i < data.Length; i++) data[i] = Color.White;
        this.texture.SetData(data);
        this.onClick = new Action<World, MouseState, KeyboardState>[] {action};
    }

    public void Draw(SpriteBatch _spriteBatch, Vector2 loc)
    {
        this.pos = loc;
        _spriteBatch.Draw(this.texture, loc, Color.Black * .5f);
    }

}