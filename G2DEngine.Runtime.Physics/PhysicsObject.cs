using G2DEngine.Runtime;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Physics
{
    public class PhysicsObject : G2DScript
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public float Mass { get; set; } = 1f;

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

            var collisionCollider = collision.GetComponent<Collider>();
            collision.TryGetComponent<PhysicsObject>(out var collisionPhysic);

            var dx = (collision.Transform.CenterPoint.X - Transform.CenterPoint.X) / (collision.Transform.ComputedSize.X / 2);
            var dy = (collision.Transform.CenterPoint.Y - Transform.CenterPoint.Y) / (collision.Transform.ComputedSize.Y / 2);

            var absDx = Math.Abs(dx);
            var absDy = Math.Abs(dy);

            // todo: impact on corner
            if(Math.Abs(absDx-absDy) < 0.001f)
            {
                //Console.WriteLine("Corner hit");
                if(dx < 0)
                {
                    Transform.Position = new Vector2(collision.Transform.Bounds.Right + 1, Transform.Position.Y);
                }else
                {
                    Transform.Position = new Vector2(collision.Transform.Bounds.Left - Transform.ComputedSize.X - 1, Transform.Position.Y);
                }

                if(dy < 0)
                {
                    Transform.Position = new Vector2(Transform.Position.X, collision.Transform.Bounds.Bottom + 1);
                }else
                {
                    Transform.Position = new Vector2(Transform.Position.X, collision.Transform.Bounds.Top - Transform.ComputedSize.Y - 1);
                }

                if(rnd.NextDouble() < 0.5d)
                {
                    Velocity = new(-Velocity.X * collisionCollider.Bounciness, Velocity.Y);
                }else
                {
                    Velocity = new(Velocity.X, -Velocity.Y * collisionCollider.Bounciness);
                }
            }else if(absDx > absDy)
            {
                //Console.WriteLine("side hit");
                if(dx < 0)
                {
                    Transform.Position = new Vector2(collision.Transform.Bounds.Right + 1, Transform.Position.Y);
                }else
                {
                    Transform.Position = new Vector2(collision.Transform.Bounds.Left - Transform.ComputedSize.X - 1, Transform.Position.Y);
                }

                collisionPhysic?.AddForce(new Vector2(Velocity.X * ((Mass / collisionPhysic.Mass)), 0));
                Velocity = new(-Velocity.X * collisionCollider.Bounciness, Velocity.Y);
            }else
            {
                //Console.WriteLine("top/bottom hit");
                if (dy < 0)
                {
                    Transform.Position = new Vector2(Transform.Position.X, collision.Transform.Bounds.Bottom + 1);
                }else
                {
                    Transform.Position = new Vector2(Transform.Position.X, collision.Transform.Bounds.Top - Transform.ComputedSize.Y - 1);
                }

                collisionPhysic?.AddForce(new Vector2(0, Velocity.Y * ((Mass / collisionPhysic.Mass))));
                Velocity = new(Velocity.X, -Velocity.Y * collisionCollider.Bounciness);
            }
        }
        public override void EarlyUpdate()
        {
            base.EarlyUpdate();
            ApplyForces();
            ResolveCollision();

        }

        private void ApplyForces()
        {
            Transform.Position += Velocity * (Time.deltaTime * 100);

            float dividend = Math.Max(1f, (1.001f * (Time.deltaTime * 100)));
            Velocity = new Vector2(Velocity.X / dividend, Velocity.Y / dividend);
        }

        public void AddForce(Vector2 force)
        {
            Velocity += force;
        }
    }
}
