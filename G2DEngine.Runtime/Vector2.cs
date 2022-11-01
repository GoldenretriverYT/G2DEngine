using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime {
    public class Vector2 {
        public Vector2(float x, float y) {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }

        public static implicit operator SkiaSharp.SKPoint(Vector2 vec) => new(vec.X, vec.Y);
        public static Vector2 operator +(Vector2 l, Vector2 r) => new(l.X + r.X, l.Y + r.Y);
    }
}
