using System.Diagnostics;

namespace Engine.Core.DebugGUI
{
    public class PerformanceCounter(string name)
    {
        public string Name = name;

        private Stopwatch _stopwatch = new Stopwatch();
        public double AverageTime => SampleCount > 0 ? TotalTime / SampleCount : 0f;
        public double TotalTime = 0;
        public int SampleCount = 0;

        public void Start()
        {
            _stopwatch.Start();
        }
        public void Stop()
        {
            _stopwatch.Stop();
            TotalTime += _stopwatch.Elapsed.TotalMilliseconds;
            SampleCount++;
        }
        public void Reset()
        {
            _stopwatch.Reset();
            TotalTime = 0;
            SampleCount = 0;
        }
    }
}
