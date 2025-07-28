using Engine.Core.DebugGUI;
using System.Diagnostics;

namespace Engine.Core.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly Stopwatch _frameTimer;
        private readonly Dictionary<string, PerformanceCounter> _counters;
        
        private double _currentFrameTime;
        private double _previousFrameTime;
        private int _frameCount;
        private float AverageFrameTimeInSeconds => (float)(AverageFrameTime / 1000f);
        private float CurrentFrameTimeInSeconds => (float) _currentFrameTime / 1000;
        public List<GameObject> GameObjects { get; } = new();

        public float AverageFrameTime => _frameCount > 0 ?(float) _frameTimer.Elapsed.TotalMilliseconds / _frameCount : 0f;
        public float AverageFPS => AverageFrameTime > 0 ? 1.0f / AverageFrameTimeInSeconds : 0f;
        public float CurrentFPS => _currentFrameTime > 0 ? (float) (1.0f / CurrentFrameTimeInSeconds) : 0f;
        public PerformanceService()
        {
            _frameTimer = new Stopwatch();
            _frameTimer.Start();
            _counters = new Dictionary<string, PerformanceCounter>();
            _frameCount = 0;
        }

        public void BeginFrame()
        {
        }

        public void EndFrame()
        {
            _currentFrameTime = _frameTimer.Elapsed.TotalMilliseconds - _previousFrameTime;
            _previousFrameTime = _frameTimer.Elapsed.TotalMilliseconds;
            _frameCount++;
        }

        public PerformanceCounter GetCounter(string name)
        {
            foreach (var counter in _counters)
            {
                if (counter.Key == name)
                {
                    return counter.Value;
                }
            }
            return _counters[name] = new PerformanceCounter(name);
        }

        public string[] GetCounterNames()
        {
            return _counters.Keys.ToArray();
        }
    }
}
