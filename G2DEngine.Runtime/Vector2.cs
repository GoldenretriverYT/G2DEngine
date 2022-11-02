using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime {
    public class Vector2 {
        public static Vector2 Zero = new(0, 0);

        public Vector2(float x, float y) {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }

        public static implicit operator SkiaSharp.SKPoint(Vector2 vec) => new(vec.X, vec.Y);
        public static Vector2 operator +(Vector2 l, Vector2 r) => new(l.X + r.X, l.Y + r.Y);
        public static Vector2 operator -(Vector2 l, Vector2 r) => new(l.X - r.X, l.Y - r.Y);
        public static Vector2 operator *(Vector2 l, Vector2 r) => new(l.X * r.X, l.Y * r.Y);
        public static Vector2 operator /(Vector2 l, Vector2 r) => new(l.X / r.X, l.Y / r.Y);
        public static Vector2 operator *(Vector2 l, float r) => new(l.X * r, l.Y * r);

        public override string ToString()
        {
            return "<Vector2[x=" + X + ",y=" + Y + "]>";
        }

    }
}
