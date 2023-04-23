namespace MyGame.Testing {

    public class Levels {
        
        public static World InitializeMainDebug()
        {
        World world = new World();
        world.xbound = 16;
        world.ybound = 16;
        world.GenerateTileArray();

        world.entities = new Entity[16];

        Entity ball = new Entity();
        ball.SetTile(world.TileArray[2,5]);
        ball.speed = 100f;
        ball.objectKey = "ball";
        ball.pos = ball.GetTile().pos;
        world.entities[0] = ball;

        Entity ball2 = new Entity();
        ball2.SetTile(world.TileArray[3,3]);
        ball2.speed = 100f;
        ball2.objectKey = "ball";
        ball2.pos = ball2.GetTile().pos;
        world.entities[1] = ball2;

        return world;
        }

    }
}