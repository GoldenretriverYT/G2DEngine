using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime {
    public class Transform {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        [JsonIgnore]
        public SKRect Bounds => new(Position.X, Position.Y, Position.X + ComputedSize.X, Position.Y + ComputedSize.Y);

        /// <summary>
        /// Exact size of rendered object in pixels.
        /// 
        /// <para>Computed size should only be set by one source. One of those would be SpriteRenderer.
        /// If you have a sprite renderer on your object and you try to set the computed size,
        /// it will conflict.</para>
        /// 
        /// <para>The computed size is important for the calculation of the object bounds, collisions, etc.</para>
        /// </summary>
        [JsonIgnore]
        public Vector2 ComputedSize { get; set; } = new(0, 0);

        [JsonIgnore] public GameObject GameObject { get; set; } 

        public Transform(GameObject gameObj) {
            GameObject = gameObj;

            Position = new(0, 0);
            Scale = new(1, 1);

            //Console.WriteLine("wrong constructor :(");
        }

        //[JsonConstructor]
        //public Transform(Vector2 position, Vector2 scale) {
        //    Position = position;
        //    Scale = scale;
        //
        //    Console.WriteLine("correct constructor!!");
        //}

    }
}
