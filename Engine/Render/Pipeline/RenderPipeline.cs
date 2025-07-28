using System.Diagnostics;
using Engine.Core;
using Engine.Core.Interfaces;

namespace Engine.Render.Pipeline
{
    public class RenderPipeline
    {
        private List<IRenderPass>? _passes;

        public void AddPass(IRenderPass pass)
        {
            if (_passes == null)
            {
                _passes = new List<IRenderPass>();
            }
            _passes.Add(pass);

            if (_passes.Count == 1)
            {
                // If this is the first pass, no need to sort
                return;
            }

            _passes.Sort((a, b) =>
            {
                if (a.Dependencies.Contains(b.Name))
                {
                    return 1;
                }

                if (b.Dependencies.Contains(a.Name))
                {
                    return -1;
                }

                return 0;
            });
        }

        public void Execute(List<GameObject> gameObjects, float aspectRatio)
        {
            if (_passes == null) return;
            // Execute each pass
            foreach (var pass in _passes)
            { 
                if (!pass.Enabled) continue;
                pass.Execute(gameObjects, aspectRatio);
            }
        }
    }
}
