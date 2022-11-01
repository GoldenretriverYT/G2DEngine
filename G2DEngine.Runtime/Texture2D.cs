using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace G2DEngine.Runtime {
    public class Texture2D {
        [JsonIgnore]
        public SKImage SkImage { get; set; }

        [JsonProperty]
        private string path;

        [JsonConstructor]
        public Texture2D(string path) {
            this.path = path;

            if(!File.Exists("Content/" + path)) {
                throw new FileNotFoundException("Resource " + path + " was not found!");
            }

            SkImage = SKImage.FromEncodedData(File.ReadAllBytes("Content/" + path));
        }
    }
}
