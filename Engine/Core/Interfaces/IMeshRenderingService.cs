using Engine.Core;
using Engine.Core.Camera;

namespace Engine.Core.Interfaces
{
    public interface IMeshRenderingService
    {
        public void Render(GameObject gameObject, CameraComponent camera);
    }
}
