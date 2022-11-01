using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime {
    public class Input {
        public static bool GetKey(Key key) {
            return Game.CurrentlyPressedKeys.Contains(key);
        }
    }
}
