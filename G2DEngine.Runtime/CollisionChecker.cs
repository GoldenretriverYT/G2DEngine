using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime {
    public static class CollisionChecker {
        public static bool IsColliding<T1, T2>(T1 l, T2 r) {
            if(l is SKRect rect1 && r is SKRect rect2) {
                return (rect1.Left + rect1.Width >= rect2.Left &&
                      rect1.Left <= rect2.Left + rect2.Width &&
                      rect1.Top + rect1.Height >= rect2.Top &&
                      rect1.Top <= rect2.Top + rect2.Height);
            }else {
                throw new Exception("Unsupported collision types! " + l.GetType() + " with " + r.GetType() + " has not been implemented");
            }
        }
    }
}
