using Engine.Core.Camera;
using OpenTK.Mathematics;

namespace Engine.Core.Systems
{
    public class CameraSystem
    {
        public void Update(IEnumerable<GameObject> gameObjects, float aspectRatio)
        {
            foreach (var gameObject in gameObjects)
            {

                var cameraComponent = gameObject.GetComponent<CameraComponent>();
                if (cameraComponent == null || !cameraComponent.IsDirty) continue;
                // Mark the camera as clean after updating
                cameraComponent.ViewMatrix = Matrix4.LookAt(cameraComponent.Position, cameraComponent.Target, cameraComponent.Up);
                cameraComponent.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                    MathHelper.DegreesToRadians(cameraComponent.FieldOfView),
                    aspectRatio,
                    cameraComponent.NearPlane,
                    cameraComponent.FarPlane
                );
                cameraComponent.ViewProjectionMatrix = cameraComponent.ViewMatrix * cameraComponent.ProjectionMatrix;
                cameraComponent.IsDirty = false;
            }
        }
    }
}
