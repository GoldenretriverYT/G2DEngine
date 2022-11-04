using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime {
    public class GameObject {
        public Transform Transform { get; set; }
        public List<GameObject> Children { get; set; } = new();
        public GameObject Parent { get; set; }

        private bool hasBeenStarted = false;

        /// <summary>
        /// Warning! This is not persistent across game restart and such.
        /// </summary>
        [JsonIgnore]
        public int ObjectId { get; set; }

        private static int objectIdCounter = 0;

        [JsonConstructor]
        private GameObject() {
            objectIdCounter++;
            ObjectId = objectIdCounter;
        }

        public static GameObject Instantiate() {
            var obj = new GameObject();
            obj.Transform = new(obj);

            return obj;
        }

        /*[JsonConstructor]
        public GameObject(Transform transform) {
            Transform = transform;
            Transform.GameObject = this;
            //Transform.Parent = transform
        }*/

        [JsonProperty]
        private List<G2DScript> components = new();
        //[JsonProperty]
        //private List<Type> componentTypes = new();

        public void EarlyUpdate()
        {
            foreach (var component in components) component.EarlyUpdate();
        }

        public void Update() {
            foreach (var component in components) component.Update();
        }

        public void LateUpdate()
        {
            foreach(var component in components) component.LateUpdate();
        }

        public void Start() {
            hasBeenStarted = true;
            foreach (var component in components) component.Start(this);
        }

        public T GetComponent<T>() where T : G2DScript {
            //if (!componentTypes.Contains(typeof(T))) return default(T);

            foreach (G2DScript script in components) {
                if (script is T val) return val;
            }

            return default(T);
        }

        public bool TryGetComponent<T>(out T component) where T : G2DScript {
            component = GetComponent<T>();

            if (component == null) return false;
            return true;
        }

        public void AddComponent(G2DScript component) {
            components.Add(component);
            //componentTypes.Add(component.GetType());
            component.GameObject = this;
            if(hasBeenStarted) component.Start(); // Only call .Start if gameobject already has been started (so only if the component was added in runtime)
        }

        public void AddChild(GameObject obj) {
            Children.Add(obj);
            obj.Parent = this;
        }

        public void SetParent(GameObject newParent) {
            Parent?.Children.Remove(this);
            newParent.AddChild(this);
        }
    }
}
