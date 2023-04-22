using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public partial class Game1 : Game
{
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
}
