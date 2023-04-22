using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame.Menu;

public class GameMenu {

    private bool isOpen = false;
    public Texture2D texture;

    public GameMenu()
    {

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
    }

}