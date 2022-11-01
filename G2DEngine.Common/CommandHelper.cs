using System.Diagnostics;

namespace G2DEngine.Common {
    public static class CommandHelper {
        private static string defaultDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static string RunCommand(string command, string dir = null) {
            string output = "";

            if(dir == null) {
                dir = defaultDir;
            }

            if(!Path.IsPathRooted(dir)) {
                dir = Path.Join(defaultDir, dir);
            }

            var proc = new Process();
            proc.StartInfo = new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = "/C " + command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WorkingDirectory = dir
            };

            proc.Start();
            while (!proc.StandardOutput.EndOfStream) {
                output += proc.StandardOutput.ReadLine();
            }

            proc.WaitForExit();

            return output;
        }
    }
}