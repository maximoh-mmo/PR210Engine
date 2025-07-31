using Engine.Core.Camera;
using Engine.Core.Interfaces;
using Engine.Render.Graphics;
using Engine.Render.passes;
using Engine.Render.Pipeline;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Core.Systems
{
    public class RenderSystem : IRenderSystem
    {
        private readonly IMeshRenderingService _meshRenderer;
        private readonly ICullingService?[]? _cullingService;

        private RenderPipeline _renderPipeline;

        public RenderPipeline RenderPipeline => _renderPipeline;
        public RenderSystem(IMeshRenderingService meshRenderer)
        {
            _meshRenderer = meshRenderer;
            _renderPipeline = new RenderPipeline();
            //_renderPipeline.AddPass(new WireframePass());
            _renderPipeline.AddPass(new ScenePass(_meshRenderer, new OpenTKRenderContext()));
        }

        public void Render(IReadOnlyList<GameObject> gameObjects, CameraComponent camera)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _renderPipeline.Execute(gameObjects, camera);
        }
    }
}