using Engine.Core;
using Engine.Core.Interfaces;

namespace Engine.Render.passes
{
    public class ScenePass : IRenderPass
    {
        public ScenePass(IMeshRenderingService meshRenderer, IRenderContext renderContext)
        {
            this.meshRenderer = meshRenderer;
            this.renderContext = renderContext;
            _enabled = true;
        }
        private IMeshRenderingService meshRenderer;
        private IRenderContext renderContext;
        private bool _enabled;
        public string Name => "Scene";
        public bool Enabled
        {
            get => _enabled;
            set => _enabled = value;
        }
        public IReadOnlyList<string> Dependencies => ["WireFrame"];

        public void Execute(List<GameObject> gameObjects, float aspectRatio)
        {
            if (!Enabled) return;
            // Render each game object in the scene
            renderContext.BindFramebuffer(renderContext.DefaultFboId);
            foreach (var gameObject in gameObjects)
            {
                meshRenderer.Render(gameObject, aspectRatio);
            }
        }
    }
}
