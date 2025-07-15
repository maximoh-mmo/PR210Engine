using OpenTK.Mathematics;

namespace Engine.Core.Components
{
    public class TransformComponent : IComponent
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 Scale = Vector3.One;
        public Vector3 Rotation = Vector3.Zero;
        public Matrix4 ModelMatrix = Matrix4.Identity;
    }
}
