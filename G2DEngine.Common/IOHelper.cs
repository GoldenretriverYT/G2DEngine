using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2DEngine.Common {
    public class IOHelper {
        public static void CopyRecursively(DirectoryInfo source, DirectoryInfo target) {
            foreach (DirectoryInfo dir in source.GetDirectories()) {
                CopyRecursively(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach (FileInfo file in source.GetFiles()) {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
        }
    }
}
