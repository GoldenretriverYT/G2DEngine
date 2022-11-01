using G2DEngine.Common;
using G2DEngine.Runtime;
using System.Diagnostics;
using System.Xml;
using System.IO.Compression;

namespace G2DEngine.Editor {
    public partial class MainScreen : Form {
        public MainScreen() {
            InitializeComponent();

            if(!Directory.Exists("buildprj") || Directory.GetFiles("buildprj").Length < 2) {
                Builder.InitiliazeProject();
            }

            Builder.Build();
        }

        private void MainScreen_Load(object sender, EventArgs e) {

        }
    }
}