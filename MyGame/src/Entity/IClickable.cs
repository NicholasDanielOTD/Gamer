using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

namespace MyGame {

public interface IClickable {
    Vector2 pos {get; set;}
    Texture2D texture {get; set;}
    Action<World, MouseState, KeyboardState>[] onClick {get; set;}

}


}