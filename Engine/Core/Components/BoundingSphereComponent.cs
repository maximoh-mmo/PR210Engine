using OpenTK.Mathematics;

namespace Engine.Core.Components
{
    public class BoundingSphereComponent(Vector3? center = null, float radius = 1.0f) : IComponent
    {
        public Vector3 Center = center?? Vector3.Zero;
        public float Radius = radius;

        public Vector3 GetWorldCenter(TransformComponent transform)
        {
            return Vector3.TransformPosition(Center, transform.WorldMatrix);
        }

        public float GetWorldRadius(TransformComponent transform)
        {
            var scale = transform.WorldMatrix.ExtractScale();
            return float.Max(float.Max(scale.X, scale.Y), scale.Z) * Radius;
        }
    }
}
