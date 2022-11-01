using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime {
    public abstract class G2DScript {
        [JsonIgnore]
        public GameObject GameObject { get; internal set; }
        [JsonIgnore]
        public Transform Transform => GameObject.Transform;

        public virtual void Update() {
            
        }

        public virtual void Start() {
        }

        internal void Start(GameObject obj) {
            GameObject = obj;
            Start();
        }
    }
}
