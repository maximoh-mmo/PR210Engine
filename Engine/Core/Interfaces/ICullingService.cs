using Engine.Core.Camera;

namespace Engine.Core.Interfaces
{
    public interface ICullingService
    {
        public IReadOnlyList<GameObject> Cull(IEnumerable<GameObject> gameObjects, CameraComponent camera);
    }
}
