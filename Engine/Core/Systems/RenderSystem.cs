using Engine.Core.Services;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Core.Systems
{
    public class RenderSystem
    {
        private readonly IMeshRenderingService meshRenderer;

        public RenderSystem(IMeshRenderingService meshRenderer)
        {
            this.meshRenderer = meshRenderer;
        }

        public void Render(List<GameObject> gameObjects, float aspectRatio)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            gameObjects.ForEach(gameObject => meshRenderer.Render(gameObject, aspectRatio));
        }
    }
}