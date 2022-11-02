using G2DEngine.Runtime.Utils;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime.CoreComponents {
    public class BoxCollider : Collider {
        public override GameObject GetCollision() {
            IEnumerable<GameObject> gameObjects = Game.ActiveScene.GameObjects.Flatten();

            foreach(var obj in gameObjects) {
                if (obj.ObjectId == GameObject.ObjectId) continue;

                if(obj.TryGetComponent<BoxCollider>(out var otherCollider)) {
                    if(CollisionChecker.IsColliding(Transform.Bounds, otherCollider.Transform.Bounds)) {
                        return otherCollider.GameObject;
                    }
                }
            }

            return null;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            var centerPoint = new Vector2(Transform.Position.X + (Transform.ComputedSize.X / 2), Transform.Position.Y + (Transform.ComputedSize.Y / 2));
            var paint = new SKPaint();
            paint.Color = SKColors.Yellow;
            Game.Canvas.DrawCircle(centerPoint, 3, paint);
        }

        public void MoveIfFree(Vector2 movement) {
            Vector2 originalPos = Transform.Position;

            Transform.Position += movement;
            var collision = GetCollision();

            if (collision != null) Transform.Position = originalPos;
        }
    }
}
