﻿using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime.CoreComponents
{
    public class PhysicsObject : G2DScript
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;

        private List<Vector2> dbgVelocityHist = new();

        private static Random rnd = new();

        public void ResolveCollision()
        {
            if (!GameObject.TryGetComponent<Collider>(out var collider))
            {
                return;
            };

            var collision = collider.GetCollision();

            if (collision == null)
            {
                return;
            }

            /*var centerPoint = new Vector2(collision.Transform.Position.X + (collision.Transform.ComputedSize.X / 2), collision.Transform.Position.Y + (collision.Transform.ComputedSize.Y / 2));
            var paint = new SKPaint();
            paint.Color = SKColors.Yellow;
            Game.Canvas.DrawCircle(centerPoint, GameObject.ObjectId, paint);

            if (Transform.CenterPoint.X >= centerPoint.X)
            {
                var currentObjOffset = Math.Abs(Transform.Position.X - centerPoint.X);
                var offX = new Vector2((collision.Transform.ComputedSize.X / 2) - currentObjOffset + 1, 0);
                this.Transform.Position += offX;
            }
            else if (Transform.CenterPoint.X <= centerPoint.X)
            {
                var currentObjOffset = Math.Abs(Transform.Position.X - centerPoint.X);
                var offX = new Vector2(-((collision.Transform.ComputedSize.X / 2) - currentObjOffset + 1), 0);
                this.Transform.Position -= offX;
            }
            
            if (Transform.CenterPoint.Y >= centerPoint.Y)
            {
                var currentObjOffset = Math.Abs(Transform.Position.Y - centerPoint.Y);
                var offY = new Vector2(0, (collision.Transform.ComputedSize.Y / 2) - currentObjOffset + 1);
                this.Transform.Position += offY;
            }
            else if (Transform.CenterPoint.Y <= centerPoint.Y)
            {
                var currentObjOffset = Math.Abs(Transform.Position.Y - centerPoint.Y);
                var offY = new Vector2(0, -((collision.Transform.ComputedSize.Y / 2) - currentObjOffset + 1));
                this.Transform.Position -= offY;
            }*/

            var dx = (collision.Transform.CenterPoint.X - Transform.CenterPoint.X) / (collision.Transform.ComputedSize.X / 2);
            var dy = (collision.Transform.CenterPoint.Y - Transform.CenterPoint.Y) / (collision.Transform.ComputedSize.Y / 2);

            var absDx = Math.Abs(dx);
            var absDy = Math.Abs(dy);

            if(Math.Abs(absDy-absDy) < 0.001f)
            {
                Console.WriteLine("Corner hit");
                if(dx < 0)
                {
                    Transform.Position = new Vector2(collision.Transform.Bounds.Right, Transform.Position.Y);
                }else
                {
                    Transform.Position = new Vector2(collision.Transform.Bounds.Left - Transform.ComputedSize.X, Transform.Position.Y);
                }

                if(dy < 0)
                {
                    Transform.Position = new Vector2(Transform.Position.X, collision.Transform.Bounds.Bottom);
                }else
                {
                    Transform.Position = new Vector2(Transform.Position.X, collision.Transform.Bounds.Top - Transform.ComputedSize.Y);
                }

                if(rnd.NextDouble() < 0.5d)
                {
                    Velocity = new(-Velocity.X * collider.Bounciness, Velocity.Y);
                }else
                {
                    Velocity = new(Velocity.X, -Velocity.Y * collider.Bounciness);
                }
            }else if(absDx > absDy)
            {
                Console.WriteLine("side hit");
                if(dx < 0)
                {
                    Transform.Position = new Vector2(collision.Transform.Bounds.Left - Transform.ComputedSize.X, Transform.Position.Y);
                }else
                {
                    Transform.Position = new Vector2(collision.Transform.Bounds.Left - Transform.ComputedSize.X, Transform.Position.Y);
                }

                Velocity = new(-Velocity.X * collider.Bounciness, Velocity.Y);
            }else
            {
                Console.WriteLine("top/bottom hit");
                if (dy < 0)
                {
                    Transform.Position = new Vector2(Transform.Position.X, collision.Transform.Bounds.Bottom);
                }else
                {
                    Transform.Position = new Vector2(Transform.Position.X, collision.Transform.Bounds.Top - Transform.ComputedSize.Y);
                }

                Velocity = new(Velocity.X, -Velocity.Y * collider.Bounciness);
            }
        }
        public override void Update()
        {
            base.Update();
            //Console.WriteLine("Update");
            ResolveCollision();
            ApplyForces();
        }

        private void ApplyForces()
        {
            Transform.Position += Velocity * (Time.deltaTime * 100);
            float dividend = Math.Max(1f, (1.001f * (Time.deltaTime * 100)));
            //Console.WriteLine(dividend);
            Velocity = new Vector2(Velocity.X / dividend, Velocity.Y / dividend);
            //Console.WriteLine(Velocity);

            var typeface = SKTypeface.FromFamilyName("Arial");
            var font = new SKFont(typeface, 16);
            var paint = new SKPaint(font);
            paint.Color = SKColors.Red;

            dbgVelocityHist.Add(Velocity);

            if (dbgVelocityHist.Count > 150) dbgVelocityHist.RemoveAt(0);

            int idx = 0;

            foreach (var value in dbgVelocityHist)
            {
                Game.Canvas.DrawCircle(new SKPoint(idx*5, 100-value.X*2), 5, paint);
                idx++;
            }

            Game.Canvas.DrawText(Velocity.ToString(), new SKPoint(0, 16), paint);
        }

        public void AddForce(Vector2 force)
        {
            Velocity += force;
        }
    }
}
