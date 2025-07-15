using Engine.Core.Components;
using Engine.Core.Services;
using OpenTK.Mathematics;

namespace Engine.Core.Systems
{
    public class TransformSystem
    {
        public void Update (List<TransformComponent> transformComponents, float deltaTime)
        { 
            foreach (var transform in transformComponents)
            {
                if (transform != null)
                {
                    transform.ModelMatrix = Matrix4.CreateScale(transform.Scale)
                                            * Matrix4.CreateRotationX(transform.Rotation.X)
                                            * Matrix4.CreateRotationY(transform.Rotation.Y)
                                            * Matrix4.CreateRotationZ(transform.Rotation.Z)
                                            * Matrix4.CreateTranslation(transform.Position);
                }
            }
        }
    }
}