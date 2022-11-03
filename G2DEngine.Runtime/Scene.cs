using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace G2DEngine.Runtime {
    public class Scene {
        public List<GameObject> GameObjects { get; init; } = new();

        public Scene() {

        }

        public static Scene FromContentFile(string path) {
            if (!File.Exists("Content/" + path)) {
                throw new FileNotFoundException("Resource " + path + " was not found!");
            }

            Scene s = JsonConvert.DeserializeObject<Scene>(File.ReadAllText("Content/" + path), new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
            });
            return s;
        }

        public string Serialize() => JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
        });
        public static string Serialize(Scene s) => s.Serialize();
    }
}
