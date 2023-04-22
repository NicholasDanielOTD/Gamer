using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public partial class Game1 : Game
{
    private KeyboardState lastKstate;
    private MouseState lastMstate;

    protected void CheckInputs(GameTime gameTime)
    {
        var kstate = Keyboard.GetState();
        var mstate = Mouse.GetState();

        if (KeyWasPressed(Keys.Escape, kstate)) myMenu.ToggleOpen();
        if (mstate.LeftButton == ButtonState.Pressed) HandleLeftClick(mstate);
        if (mstate.RightButton == ButtonState.Pressed) HandleRightClick(mstate);
        lastKstate = kstate;
        lastMstate = mstate;
    }

    protected void HandleLeftClick(MouseState mstate)
    {
        if (!this.myMenu.IsOpen())
        {
            (Entity, Tile) tup = myWorld.GetThingAtPoint(mstate.Position);
            if (tup.Item1 != null) tup.Item1.onLeftClick(myWorld);
            else if (tup.Item2 != null) tup.Item2.onLeftClick(myWorld);
        }
        
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
            if (ent.path != null) ent.Move(gameTime.ElapsedGameTime.TotalSeconds);
        } 
    }

    protected bool KeyWasPressed(Keys key, KeyboardState kstate)
    {
        if (kstate.IsKeyUp(key) && this.lastKstate.IsKeyDown(key)) return true;
        return false;
    }
}
