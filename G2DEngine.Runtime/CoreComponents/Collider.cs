using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime.CoreComponents
{
    public abstract class Collider : G2DScript
    {
        public abstract GameObject GetCollision();
        public float Bounciness { get; set; } = 0.01f;
    }
}
