using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime.CoreComponents
{
    public class PhysicsObject : G2DScript
    {
        public void ResolveCollision()
        {
            Console.WriteLine("Resolving collision");

            if (!GameObject.TryGetComponent<Collider>(out var collider))
            {
                Console.WriteLine("No collider");
                return;
            };

            var collision = collider.GetCollision();

            if (collision == null)
            {
                Console.WriteLine("No collision");
                return;
            }

            Console.WriteLine("Calculating center point");
            var centerPoint = new Vector2(collision.Transform.Position.X + (collision.Transform.ComputedSize.X / 2), collision.Transform.Position.Y + (collision.Transform.ComputedSize.Y / 2));
            var paint = new SKPaint();
            paint.Color = SKColors.Yellow;
            Game.Canvas.DrawCircle(centerPoint, GameObject.ObjectId, paint);

            if (Transform.CenterPoint.X >= centerPoint.X)
            {
                var currentObjOffset = Math.Abs(Transform.Position.X - centerPoint.X);
                var offX = new Vector2((collision.Transform.ComputedSize.X / 2) - currentObjOffset + 1, 0);
                Console.WriteLine(offX.X);
                this.Transform.Position += offX;
            }
            else if (Transform.CenterPoint.X <= centerPoint.X)
            {
                var currentObjOffset = Math.Abs(Transform.Position.X - centerPoint.X);
                var offX = new Vector2(-((collision.Transform.ComputedSize.X / 2) - currentObjOffset + 1), 0);
                Console.WriteLine(offX.X);
                this.Transform.Position -= offX;
            }
            
            if (Transform.CenterPoint.Y >= centerPoint.Y)
            {
                var currentObjOffset = Math.Abs(Transform.Position.Y - centerPoint.Y);
                var offY = new Vector2(0, (collision.Transform.ComputedSize.Y / 2) - currentObjOffset + 1);
                Console.WriteLine(offY.Y);
                this.Transform.Position += offY;
            }
            else if (Transform.CenterPoint.Y <= centerPoint.Y)
            {
                var currentObjOffset = Math.Abs(Transform.Position.Y - centerPoint.Y);
                var offY = new Vector2(0, -((collision.Transform.ComputedSize.Y / 2) - currentObjOffset + 1));
                Console.WriteLine(offY.Y);
                this.Transform.Position -= offY;
            }
        }
        public override void Update()
        {
            base.Update();
            Console.WriteLine("Update");
            ResolveCollision();
        }
    }
}
