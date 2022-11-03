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

        public static long CurrentFrame { get; private set; } = 0;

        public static SKCanvas Canvas { get; private set; }

        public static SKColor BackgroundColor { get; set; } = SKColors.CornflowerBlue;
        public static Scene ActiveScene { get; private set; }

        public static List<Key> CurrentlyPressedKeys { get; private set; } = new();

        public static void Start(Scene startScene, Vector2 resolution) {
            Start(startScene, (int)resolution.X, (int)resolution.Y);
        }

        public static void Start(Scene startScene, int resW = 800, int resH = 600)
        {
            ActiveScene = startScene;

            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(resW, resH);
            options.Title = "Game";
            options.PreferredStencilBufferBits = 8;
            options.PreferredBitDepth = new Vector4D<int>(8, 8, 8, 8);
            options.VSync = true;
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
            renderTarget = new GRBackendRenderTarget(resW, resH, 0, 8, new GRGlFramebufferInfo(0, 0x8058)); // 0x8058 = GL_RGBA8`
            surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Rgba8888);

            Canvas = surface.Canvas;

            foreach (var obj in ActiveScene.GameObjects)
            {
                obj.Start();
            }

            window.Render += GameUpdate;
            window.Run();
        }

        private static void GameUpdate(double something)
        {
            grContext.ResetContext();
            Canvas.Clear(BackgroundColor);

            CallObjectUpdates();

            Canvas.Flush();

            CalculateDeltaTime();
        }

        private static void CallObjectUpdates()
        {
            foreach (var obj in ActiveScene.GameObjects)
            {
                CurrentFrame++;
                obj.EarlyUpdate();
            }

            foreach (var obj in ActiveScene.GameObjects)
            {
                obj.Update();
            }

            foreach (var obj in ActiveScene.GameObjects)
            {
                obj.LateUpdate();
            }
        }

        private static void CalculateDeltaTime()
        {
            stopwatch.Stop();

            lastFrameTimes.Add(stopwatch.Elapsed.TotalMilliseconds);
            if (lastFrameTimes.Count > 100) lastFrameTimes.RemoveAt(0);

            double avgFrametime = (lastFrameTimes.Sum() / lastFrameTimes.Count);

            window.Title = $"Current Frame: {stopwatch.Elapsed.TotalMilliseconds.ToString("0.0")}ms | Average (last 100 frames): {avgFrametime.ToString("0.0")} | FPS: {(1000 / avgFrametime).ToString("0")}";

            Time.deltaTime = (float)(stopwatch.Elapsed.TotalMilliseconds / 1000);
            stopwatch.Restart();
        }

        private static void OnKeyDownHandler(IKeyboard kb, Key key, int arg3) {
            if (!CurrentlyPressedKeys.Contains(key)) CurrentlyPressedKeys.Add(key);
        }

        private static void OnKeyUpHandler(IKeyboard arg1, Key key, int arg3) {
            if(CurrentlyPressedKeys.Contains(key)) CurrentlyPressedKeys.Remove(key);
        }
    }
}