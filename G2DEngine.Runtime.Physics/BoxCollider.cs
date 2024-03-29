﻿using G2DEngine.Runtime;
using G2DEngine.Runtime.Utils;
using Silk.NET.Vulkan;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Physics
{
    public class BoxCollider : Collider {
        public BoxCollider()
        {

        }

        public BoxCollider(float bounciness)
        {
            this.Bounciness = bounciness;
        }

        public override GameObject GetCollision(Vector2 overridePos = null) {
            var currentObjectBounds = Transform.Bounds;

            if(overridePos != null) {
                currentObjectBounds = new SKRect(overridePos.X, overridePos.Y, overridePos.X + Transform.Bounds.Width, overridePos.Y + Transform.Bounds.Height);
            }

            IEnumerable<GameObject> gameObjects = Collider.GetFlattenedGameObjects();

            foreach(var obj in gameObjects) {
                if (obj.ObjectId == GameObject.ObjectId) continue;

                if (obj.TryGetComponent<BoxCollider>(out var otherCollider))
                {

                    if (CollisionChecker.IsColliding(currentObjectBounds, otherCollider.Transform.Bounds))
                    {
                        return otherCollider.GameObject;
                    }

                    /*var rect1 = Transform.Bounds;
                    var rect2 = otherCollider.Transform.Bounds;

                    if ((rect1.Right >= rect2.Left &&
                      rect1.Left <= rect2.Right &&
                      rect1.Bottom >= rect2.Top &&
                      rect1.Top <= rect2.Bottom)) return otherCollider.GameObject;*/
                }
            }

            return null;
        }

        public override void Update()
        {
            
        }

        public override void LateUpdate()
        {
            
            //var centerPoint = new Vector2(Transform.Position.X + (Transform.ComputedSize.X / 2), Transform.Position.Y + (Transform.ComputedSize.Y / 2));
            //var paint = new SKPaint();
            //paint.Color = SKColors.Yellow;
            //Game.Canvas.DrawCircle(centerPoint, 3, paint);
        }

        public void MoveIfFree(Vector2 movement) {
            Vector2 originalPos = Transform.Position;

            Transform.Position += movement;
            var collision = GetCollision();

            if (collision != null) Transform.Position = originalPos;
        }
    }
}
