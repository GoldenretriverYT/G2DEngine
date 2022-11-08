using G2DEngine.Runtime;
using G2DEngine.Runtime.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Physics
{
    public abstract class Collider : G2DScript
    {
        public abstract GameObject GetCollision();
        public float Bounciness { get; set; } = 0.01f;

        private static IEnumerable<GameObject> flattenedGameObjects = new List<GameObject>();
        private static long lastFlattenedUpdateFrame = 0;

        public static IEnumerable<GameObject> GetFlattenedGameObjects()
        {
            if(Game.CurrentFrame != lastFlattenedUpdateFrame)
            {
                flattenedGameObjects = Game.ActiveScene.GameObjects.Flatten();
                lastFlattenedUpdateFrame = Game.CurrentFrame;
            }

            return flattenedGameObjects;
        }
    }
}
