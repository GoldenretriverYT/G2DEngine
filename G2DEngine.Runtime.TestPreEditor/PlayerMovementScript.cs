using G2DEngine.Runtime.CoreComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime.TestPreEditor {
    public class PlayerMovementScript : G2DScript {
        public float movementSpeed = 1000f;
        public BoxCollider boxCollider;

        public override void Start() {
            boxCollider = GameObject.GetComponent<BoxCollider>();
        }

        public override void Update() {
            base.Update();

            if (Input.GetKey(Silk.NET.Input.Key.A)) {
                boxCollider.MoveIfFree(new Vector2(-(movementSpeed * Time.deltaTime), 0));
            }

            if (Input.GetKey(Silk.NET.Input.Key.D)) {
                boxCollider.MoveIfFree(new Vector2((movementSpeed * Time.deltaTime), 0));
            }

            if (Input.GetKey(Silk.NET.Input.Key.W)) {
                boxCollider.MoveIfFree(new Vector2(0, -(movementSpeed * Time.deltaTime)));
            }

            if (Input.GetKey(Silk.NET.Input.Key.S)) {
                boxCollider.MoveIfFree(new Vector2(0, (movementSpeed * Time.deltaTime)));
            }
        }
    }
}
