using Engine.Core.DebugGUI;
using Engine.Core.Services;
using OpenTK.Mathematics;

namespace Engine.Core.Interfaces
{
    public interface IPerformanceService
    {
        public List<GameObject> GameObjects { get; }
        float AverageFrameTime { get; }
        float AverageFPS { get; }
        float CurrentFPS { get; }
        void BeginFrame();
        void EndFrame();
        PerformanceCounter GetCounter(string name);
        string[] GetCounterNames();
    }
}
