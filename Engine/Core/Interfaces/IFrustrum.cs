using OpenTK.Mathematics;

namespace Engine.Core.Interfaces
{
    public interface IFrustrum
    {
        public void BuildFrustrum(Matrix4 viewProjectionMatrix);
        public bool IntersectsSphere(Vector3 center, float radius);
        public bool ContainsPoint(Vector3 point);
    }
}
