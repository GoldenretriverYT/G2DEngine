using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Physics
{
    public static class CollisionChecker {
        public static bool IsColliding(SKRect rect1, SKRect rect2) {
            return (rect1.Right >= rect2.Left &&
                      rect1.Left <= rect2.Right &&
                      rect1.Bottom >= rect2.Top &&
                      rect1.Top <= rect2.Bottom);
        }
    }
}
