using G2DEngine.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace G2DEngine.Editor {
    public class Builder {
        public static void Build() {
            Debug.WriteLine(CommandHelper.RunCommand("dotnet build --configuration Release", "buildprj"));
            IOHelper.CopyRecursively(new DirectoryInfo("runtimes/win-x64/native"), new DirectoryInfo("buildprj/bin/Release/net6.0"));
        }

        public static void InitiliazeProject() {
            Directory.CreateDirectory("buildprj");
            CommandHelper.RunCommand("dotnet new console -lang \"C#\"", "buildprj");

            XmlDocument doc = new XmlDocument();
            doc.Load("buildprj/buildprj.csproj");

            XmlNode root = doc.DocumentElement;

            XmlNode itmgroup = doc.CreateNode(XmlNodeType.Element, "ItemGroup", null);
            XmlNode refRuntime = doc.CreateNode(XmlNodeType.Element, "Reference", null);
            (refRuntime as XmlElement).SetAttribute("Include", "../G2DEngine.Runtime.dll");
            XmlNode refCommon = doc.CreateNode(XmlNodeType.Element, "Reference", null);
            (refCommon as XmlElement).SetAttribute("Include", "../G2DEngine.Common.dll");

            itmgroup.AppendChild(refRuntime);
            itmgroup.AppendChild(refCommon);

            root.AppendChild(itmgroup);

            doc.Save("buildprj/buildprj.csproj");

            File.WriteAllText("buildprj/Program.cs", @"using G2DEngine.Common;
using G2DEngine.Runtime;

namespace MyGame;

public class Program {
    public static void Main(string[] args) {
        //WindowHelper.HideConsole();
        Game.Start();
    }
}");

        }
    }
}
