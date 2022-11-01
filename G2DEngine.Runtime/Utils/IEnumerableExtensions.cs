using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Runtime.Utils {
    public static class IEnumerableExtensions {
        public static IEnumerable<GameObject> Flatten(this IEnumerable<GameObject> objs) {
            foreach(GameObject obj in objs) {
                yield return obj;
                var flattened = obj.Children.Flatten();

                foreach(GameObject flattenedObj in objs) {
                    yield return flattenedObj;
                }
            }
        }
    }
}
