using Engine.Core;
using Engine.Core.Interfaces;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Render.passes
{
    public class WireframePass : IRenderPass
    {
        private bool _enabled;

        public WireframePass()
        {
            _enabled = true;
        }
        public string Name => "Wireframe";
        public bool Enabled
        {
            get { return _enabled;}
            set { _enabled = value; }
        }
        public IReadOnlyList<string> Dependencies => [];
        public void Execute(List<GameObject> gameObjects, float aspectRatio)
        {
            GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
            GL.LineWidth(1.0f);
        }
    }
}
