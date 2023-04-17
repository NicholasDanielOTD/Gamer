namespace MyGame.Testing {

    public class Levels {
        
        public static World InitializeMainDebug()
        {
        World world = new World();
        world.GenerateTileArray();

        world.entities = new Entity[16];

        Entity ball = new Entity();
        ball.tile = world.TileArray[2,5];
        ball.speed = 100f;
        ball.objectKey = "ball";
        ball.pos = ball.tile.pos;
        world.entities[0] = ball;

        Entity ball2 = new Entity();
        ball2.tile = world.TileArray[3,3];
        ball2.speed = 100f;
        ball2.objectKey = "ball";
        ball2.pos = ball2.tile.pos;
        world.entities[1] = ball2;

        return world;
        }

    }
}