using Engine.Core.Camera;
using Engine.Core.Components;
using Engine.Core.Interfaces;
using OpenTK.Mathematics;

namespace Engine.Core.Services
{
    public class FrustrumCullingService : ICullingService
    {
        private readonly IFrustrum _frustrum;

        public FrustrumCullingService(IFrustrum? frustrum = null)
        {
            if (frustrum == null)
            {
                _frustrum = new PerspectiveFrustrum(); // Default to PerspectiveFrustrum if none provided
            }
            else
            {
                _frustrum = frustrum;
            }
        }

        public IReadOnlyList<GameObject> Cull(IEnumerable<GameObject> gameObjects, CameraComponent camera)
        {
            _frustrum.BuildFrustrum(camera.ViewProjectionMatrix);
            List<GameObject> visibleObjects = [];
            foreach (var gameObject in gameObjects)
            {
                if (IsObjectVisible(gameObject, _frustrum)) 
                    visibleObjects.Add(gameObject);
            }
            return visibleObjects;
        }
        private bool IsObjectVisible(GameObject gameObject, IFrustrum frustrum)
        {
            var boundingSphere = gameObject.GetComponent<BoundingSphereComponent>();
            var transform = gameObject.GetComponent<TransformComponent>();
            if (boundingSphere == null || transform == null)
            {
                return false; // Skip objects without bounding sphere or transform
            }
            if (frustrum.IntersectsSphere(boundingSphere.GetWorldCenter(transform), boundingSphere.GetWorldRadius(transform)))
            {
                return true;
            }
            return false;
        }
    }
}
