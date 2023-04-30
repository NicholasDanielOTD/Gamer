using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MyGame.Menu;

public class GameMenu {

    private bool isOpen = false;
    public Texture2D texture;
    private List<Option> options;
    public GameMenu()
    {

    }

    public void AddOption(Option[] options)
    {
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

        }
    }

}

public class Option {
    
    private Action _onClick;
    private Texture2D texture;
    
    public Option(Action onClick)
    {
        _onClick = onClick;
    }

    public void SetClick(Action onClick)
    {
        _onClick = onClick;
    }

    public void Draw(SpriteBatch _spriteBatch, Vector2 loc)
    {
        _spriteBatch.Draw(this.texture, loc, Color.Black * .5f);
    }

}