using Engine.Core.Camera;

namespace Engine.Core.Interfaces
{
    public interface IRenderSystem
    { 
        void Render(IReadOnlyList<GameObject> gameObjects, CameraComponent camera);
    }
}
