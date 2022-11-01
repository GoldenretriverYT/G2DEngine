using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Glfw;
using Silk.NET.Input;
using SkiaSharp;
using System.Diagnostics;

namespace G2DEngine.Runtime {
    public static class Game {
        private static IWindow window;
        private static GRGlInterface grGlInterface;
        private static GRContext grContext;
        private static GRBackendRenderTarget renderTarget;
        private static SKSurface surface;
        private static IInputContext inputContext;
        private static IKeyboard keyboard;
        private static Stopwatch stopwatch = new();
        private static List<double> lastFrameTimes = new();

        public static SKCanvas Canvas { get; private set; }

        public static SKColor BackgroundColor { get; set; } = SKColors.CornflowerBlue;
        public static Scene ActiveScene { get; private set; }

        public static List<Key> CurrentlyPressedKeys { get; private set; } = new();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        public static void Start(Scene startScene) {
            ActiveScene = startScene;

            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "Game";
            options.PreferredStencilBufferBits = 8;
            options.PreferredBitDepth = new Vector4D<int>(8, 8, 8, 8);
            options.VSync = false;
            GlfwWindowing.Use();

            window = Window.Create(options);
            window.Initialize();

            inputContext = window.CreateInput();
            keyboard = inputContext.Keyboards.FirstOrDefault();

            keyboard.KeyDown += OnKeyDownHandler;
            keyboard.KeyUp += OnKeyUpHandler;

            grGlInterface = GRGlInterface.Create((name => window.GLContext!.TryGetProcAddress(name, out var addr) ? addr : (IntPtr)0));
            grGlInterface.Validate();

            grContext = GRContext.CreateGl(grGlInterface);
            renderTarget = new GRBackendRenderTarget(800, 600, 0, 8, new GRGlFramebufferInfo(0, 0x8058)); // 0x8058 = GL_RGBA8`
            surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Rgba8888);

            Canvas = surface.Canvas;

            foreach (var obj in ActiveScene.GameObjects) {
                obj.Start();
            }

            window.Render += d =>
            {
                grContext.ResetContext();
                Canvas.Clear(BackgroundColor);

                foreach(var obj in ActiveScene.GameObjects) {
                    obj.Update();
                }

                Canvas.Flush();

                stopwatch.Stop();
                window.Title = stopwatch.Elapsed.TotalMilliseconds.ToString();
                Time.deltaTime = (float)(stopwatch.Elapsed.TotalMilliseconds / 1000);
                stopwatch.Restart();
            };

            window.Run();
        }

        private static void OnKeyDownHandler(IKeyboard kb, Key key, int arg3) {
            if (!CurrentlyPressedKeys.Contains(key)) CurrentlyPressedKeys.Add(key);
        }

        private static void OnKeyUpHandler(IKeyboard arg1, Key key, int arg3) {
            if(CurrentlyPressedKeys.Contains(key)) CurrentlyPressedKeys.Remove(key);
        }
    }
}