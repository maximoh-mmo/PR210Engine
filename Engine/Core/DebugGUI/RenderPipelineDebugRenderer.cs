using Engine.Core.Interfaces;
using Engine.Core.Systems;
using ImGuiNET;

namespace Engine.Core.DebugGUI
{
    public class RenderPipelineDebugRenderer : IDebugRenderer
    {
        private readonly RenderSystem _renderSystem;
        public RenderPipelineDebugRenderer(RenderSystem renderSystem)
        {
            _renderSystem = renderSystem;
        }
        public void Render()
        {
            ImGui.Begin("Render Pipeline Debug");
            ImGui.SeparatorText("Render Passes");
            
            var passes = _renderSystem.RenderPipeline.GetAllPasses;

            if (passes == null || passes.Count == 0)
            {
                ImGui.Text("No render passes available.");
                ImGui.End();
                return;
            }
            ImGui.Text($"Total Passes: {passes.Count}");
            ImGui.Separator();

            foreach (var pass in passes)
            {
                bool isEnabled = pass.Enabled;
                if (ImGui.Checkbox($"{pass.Name}##pass_{pass.Name}", ref isEnabled))
                {
                    pass.Enabled = isEnabled;
                    ImGui.SameLine();
                    ImGui.Text($"Changed to: {isEnabled}");
                }

                if (pass.Dependencies.Count > 0)
                {
                    ImGui.Text($"Depends on: {string.Join(", ", pass.Dependencies)}");
                }
            }
            ImGui.End();
        }
    }
}
