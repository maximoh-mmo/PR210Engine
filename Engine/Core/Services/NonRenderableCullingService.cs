using Engine.Core.Camera;
using Engine.Core.Components;
using Engine.Core.Interfaces;

namespace Engine.Core.Services
{
    public class NonRenderableCullingService : ICullingService
    {
        public IReadOnlyList<GameObject> Cull(IEnumerable<GameObject> gameObjects, CameraComponent camera)
        {
            gameObjects = gameObjects.Where(go => go.GetComponent<SkinnedMeshComponent>() != null);
            List<GameObject> visibleObjects = [];
            foreach (var gameObject in gameObjects)
            {
                var boundingSphere = gameObject.GetComponent<BoundingSphereComponent>();
                var transform = gameObject.GetComponent<TransformComponent>();
                if (boundingSphere == null || transform == null)
                {
                    continue; // Skip objects without bounding sphere or transform
                }
                visibleObjects.Add(gameObject);
            }
            return visibleObjects;
        }
    }
}
