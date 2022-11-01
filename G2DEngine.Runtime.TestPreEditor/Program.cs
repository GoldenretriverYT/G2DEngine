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
            obj.Transform.Scale = new Vector2(0.3f, 0.2f);
            obj.Transform.Position = new Vector2(200, 200);

            var obj2 = GameObject.Instantiate();
            var textureObj2 = new Texture2D("wall.png");
            var spriteRendererObj2 = new SpriteRenderer();
            spriteRendererObj2.Sprite = textureObj2;
            obj2.AddComponent(spriteRendererObj2);
            obj2.AddComponent(new BoxCollider());
            obj2.Transform.Scale = new Vector2(0.2f, 0.2f);

            scene.GameObjects.Add(obj);
            scene.GameObjects.Add(obj2);

            File.WriteAllText("Content/test.scene", scene.Serialize());

            var loadedScene = Scene.FromContentFile("test.scene");
            Console.WriteLine(loadedScene.GameObjects[0].Transform.Scale.X);

            Game.Start(loadedScene);
        }
    }
}