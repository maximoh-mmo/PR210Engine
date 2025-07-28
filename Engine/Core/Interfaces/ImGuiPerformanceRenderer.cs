using Engine.Core.Components;
using ImGuiNET;

namespace Engine.Core.Interfaces
{
    public class ImGuiPerformanceRenderer : IDebugRenderer
    {
        private readonly IPerformanceService _performanceService;

        public ImGuiPerformanceRenderer(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
        }

        public void Render()
        {
            ImGui.Begin("Gameplay Statistics");
            if (ImGui.CollapsingHeader("performance"))
            {
                ImGui.Text($"Average FPS: {_performanceService.AverageFPS}");
                ImGui.Text($"Average Frame Time: {_performanceService.AverageFrameTime} ms");
                ImGui.Text($"Current FPS: {_performanceService.CurrentFPS}");
            }

            if (ImGui.CollapsingHeader("Game Objects"))
            {
                foreach (var gameObject in _performanceService.GameObjects)
                {
                    ImGui.Text($"GameObject: {gameObject.Name}");
                    var transform = gameObject.GetComponent<TransformComponent>();
                    if (transform != null)
                    {
                        ImGui.Text($"Position: {transform.Position}");
                        ImGui.Text($"Rotation: {transform.Rotation}");
                        ImGui.Text($"Scale: {transform.Scale}");
                    }
                    else
                    {
                        ImGui.Text("No Transform Component");
                    }
                }
            }
            ImGui.End();
        }
    }
}
