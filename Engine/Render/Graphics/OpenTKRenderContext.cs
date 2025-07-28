using Engine.Core.Interfaces;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Render.Graphics
{
    public class OpenTKRenderContext : IRenderContext
    {
        public int DefaultFboId => 0;

        public void BindFramebuffer(int fboId)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboId);
        }
    }
}
