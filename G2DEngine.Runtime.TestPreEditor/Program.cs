﻿using G2DEngine.Runtime.CoreComponents;

namespace G2DEngine.Runtime.TestPreEditor
{
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
            obj.Transform.Scale = new Vector2(0.3f, 0.3f);
            obj.Transform.Position = new Vector2(100, 400);

            var wallTex = new Texture2D("wall.png");

            var wallLeft = GameObject.Instantiate();
            wallLeft.AddComponent(new SpriteRenderer() { Sprite = wallTex });
            wallLeft.AddComponent(new BoxCollider());
            wallLeft.Transform.Position = new(50, 200);
            wallLeft.Transform.Scale = new(0.05f, 0.8f);

            var wallRight = GameObject.Instantiate();
            wallRight.AddComponent(new SpriteRenderer() { Sprite = wallTex });
            wallRight.AddComponent(new BoxCollider());
            wallRight.Transform.Position = new(790, 200);
            wallRight.Transform.Scale = new(0.05f, 0.8f);

            var wallTop = GameObject.Instantiate();
            wallTop.AddComponent(new SpriteRenderer() { Sprite = wallTex });
            wallTop.AddComponent(new BoxCollider());
            wallTop.Transform.Position = new(50, 200);
            wallTop.Transform.Scale = new(1.5f, 0.05f);

            var wallBottom = GameObject.Instantiate();
            wallBottom.AddComponent(new SpriteRenderer() { Sprite = wallTex });
            wallBottom.AddComponent(new BoxCollider());
            wallBottom.Transform.Position = new(50, 590);
            wallBottom.Transform.Scale = new(1.5f, 0.05f);

            scene.GameObjects.Add(wallLeft);
            scene.GameObjects.Add(wallRight);
            scene.GameObjects.Add(wallTop);
            scene.GameObjects.Add(wallBottom);

            for (var i = 0; i < 12; i++)
            {
                for (var j = 0; j < 12; j++)
                {
                    var obj2 = GameObject.Instantiate();
                    var spriteRendererObj2 = new SpriteRenderer();
                    spriteRendererObj2.Sprite = wallTex;
                    obj2.AddComponent(spriteRendererObj2);
                    obj2.AddComponent(new BoxCollider());
                    obj2.AddComponent(new PhysicsObject() { Mass=0.2f});
                    obj2.Transform.Scale = new Vector2(0.05f, 0.05f);
                    obj2.Transform.Position = new Vector2(300+ (26 * i), 400 + 26 * j);

                    scene.GameObjects.Add(obj2);
                }
            }

            scene.GameObjects.Add(obj);
            File.WriteAllText("Content/test.scene", scene.Serialize());

            var loadedScene = Scene.FromContentFile("test.scene");
            Console.WriteLine(loadedScene.GameObjects[0].Transform.Scale.X);

            Game.Start(loadedScene, 1200, 1000);
        }
    }
}