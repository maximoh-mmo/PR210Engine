
using Engine.Core.Interfaces;
using OpenTK.Mathematics;

namespace Engine.Core.Camera
{
    internal class PerspectiveFrustrum : IFrustrum
    {
        private Vector4[] _planes = new Vector4[6];
        private int LEFT=0;
        private int RIGHT=1;
        private int BOTTOM = 2;
        private int TOP=3;
        private int NEAR=4;
        private int FAR=5;
        public void BuildFrustrum(Matrix4 viewProjectionMatrix)
        {
            // Extract planes from the view-projection matrix
            _planes[LEFT] = new Vector4(viewProjectionMatrix.M14 + viewProjectionMatrix.M11, 
                                        viewProjectionMatrix.M24 + viewProjectionMatrix.M21, 
                                        viewProjectionMatrix.M34 + viewProjectionMatrix.M31, 
                                        viewProjectionMatrix.M44 + viewProjectionMatrix.M41);
            _planes[RIGHT] = new Vector4(viewProjectionMatrix.M14 - viewProjectionMatrix.M11, 
                                         viewProjectionMatrix.M24 - viewProjectionMatrix.M21, 
                                         viewProjectionMatrix.M34 - viewProjectionMatrix.M31, 
                                         viewProjectionMatrix.M44 - viewProjectionMatrix.M41);
            _planes[BOTTOM] = new Vector4(viewProjectionMatrix.M14 + viewProjectionMatrix.M12, 
                                          viewProjectionMatrix.M24 + viewProjectionMatrix.M22, 
                                          viewProjectionMatrix.M34 + viewProjectionMatrix.M32, 
                                          viewProjectionMatrix.M44 + viewProjectionMatrix.M42);
            _planes[TOP] = new Vector4(viewProjectionMatrix.M14 - viewProjectionMatrix.M12,  
                                       viewProjectionMatrix.M24 - viewProjectionMatrix.M22, 
                                       viewProjectionMatrix.M34 - viewProjectionMatrix.M32, 
                                       viewProjectionMatrix.M44 - viewProjectionMatrix.M42);
            _planes[NEAR] = new Vector4(viewProjectionMatrix.M14 + viewProjectionMatrix.M13, 
                                        viewProjectionMatrix.M24 + viewProjectionMatrix.M23, 
                                        viewProjectionMatrix.M34 + viewProjectionMatrix.M33, 
                                        viewProjectionMatrix.M44 + viewProjectionMatrix.M43);
            _planes[FAR] = new Vector4(viewProjectionMatrix.M14 - viewProjectionMatrix.M13,
                                        viewProjectionMatrix.M24 - viewProjectionMatrix.M23, 
                                        viewProjectionMatrix.M34 - viewProjectionMatrix.M33, 
                                        viewProjectionMatrix.M44 - viewProjectionMatrix.M43);
        }

        public bool IntersectsSphere(Vector3 center, float radius)
        {
            foreach (var plane in _planes)
            {
                float distance = Vector3.Dot(new Vector3(plane.X, plane.Y, plane.Z), center) + plane.W;
                if (distance < -radius)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ContainsPoint(Vector3 point)
        {
            return IntersectsSphere(point, 0);
        }
    }
}
