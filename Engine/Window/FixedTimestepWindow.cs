using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Engine.Window
{
    public class FixedTimestepWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : GameWindow(gameWindowSettings, nativeWindowSettings)
    {
        [DllImport("winmm")]
        private static extern uint timeBeginPeriod(uint uPeriod);

        private const double MS_PER_UPDATE = 1.0 / 60.0;

        private double _previousTime;
        private double _deltaTime;
        private double _lag;

        private readonly Stopwatch _gameClock = new Stopwatch();

        public override unsafe void Run()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    timeBeginPeriod(1u);
                }

                Context?.MakeCurrent();
                OnLoad();
                OnResize(new ResizeEventArgs(ClientSize));

                _gameClock.Start();
                _previousTime = _gameClock.Elapsed.TotalSeconds;
                _lag = 0.0;

                while (GLFW.WindowShouldClose(WindowPtr) == false)
                {
                    _deltaTime = _gameClock.Elapsed.TotalSeconds - _previousTime;
                    _previousTime = _gameClock.Elapsed.TotalSeconds;
                    
                    _lag += _deltaTime;

                    ProcessInput();

                    while (_lag >= MS_PER_UPDATE)
                    {
                        OnUpdateFrame(new FrameEventArgs(MS_PER_UPDATE));
                        _lag -= MS_PER_UPDATE;
                    }

                    OnRenderFrame(new FrameEventArgs(_lag / MS_PER_UPDATE));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void ProcessInput()
        {
            NewInputFrame();
            ProcessWindowEvents(IsEventDriven);
        }
    }
}