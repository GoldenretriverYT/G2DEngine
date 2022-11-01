using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime.CoreComponents {
    public class SpriteRenderer : G2DScript {
        public Texture2D Sprite { get; set; }

        public override void Update() {
            GameObject.Transform.ComputedSize = new Vector2(Sprite.SkImage.Width * Transform.Scale.X, Sprite.SkImage.Height * Transform.Scale.Y);
            
            Game.Canvas.DrawImage(Sprite.SkImage, GameObject.Transform.Bounds);
        }
    }
}
