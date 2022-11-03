using G2DEngine.Runtime.CoreComponents;

namespace G2DEngine.Runtime.TestPreEditor {
    internal class Program {
        static void Main(string[] args) {
            var scene = new Scene();

            var texture = new Texture2D("test.png");
            var spriteRenderer = new SpriteRenderer();
            spriteRenderer.Sprite = texture;

            var obj = GameObject.Instantiate();
            obj.AddComponent(spriteRenderer);
            obj.AddComponent(new PlayerMovementScript());
            obj.AddComponent(new BoxCollider());
            obj.AddComponent(new PhysicsObject());
            obj.Transform.Scale = new Vector2(0.5f, 0.5f);
            obj.Transform.Position = new Vector2(0, 0);

            for(var i = 0; i < 15; i++)
            {
                for (var j = 0; j < 1; j++)
                {
                    var obj2 = GameObject.Instantiate();
                    var textureObj2 = new Texture2D("wall.png");
                    var spriteRendererObj2 = new SpriteRenderer();
                    spriteRendererObj2.Sprite = textureObj2;
                    obj2.AddComponent(spriteRendererObj2);
                    obj2.AddComponent(new BoxCollider());
                    obj2.AddComponent(new PhysicsObject() { Mass=0.1f});
                    obj2.Transform.Scale = new Vector2(0.05f, 0.05f);
                    obj2.Transform.Position = new Vector2(300+ (26 * i), 0+ 26 * j);

                    scene.GameObjects.Add(obj2);
                }
            }

            scene.GameObjects.Add(obj);
            File.WriteAllText("Content/test.scene", scene.Serialize());

            var loadedScene = Scene.FromContentFile("test.scene");
            Console.WriteLine(loadedScene.GameObjects[0].Transform.Scale.X);

            Game.Start(loadedScene);
        }
    }
}