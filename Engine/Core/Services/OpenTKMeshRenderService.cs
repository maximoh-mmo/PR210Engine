using Engine.Core.Camera;
using Engine.Core.Components;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Engine.Core.Interfaces;

namespace Engine.Core.Services
{
    public class OpenTKMeshRenderService : IMeshRenderingService
    {
        public void Render(GameObject gameObject, CameraComponent camera)
        {
            var skinnedMeshComp = gameObject.GetComponent<SkinnedMeshComponent>();
            if (skinnedMeshComp == null)
            {
                Console.WriteLine($"GameObject {gameObject.Name} does not have a SkinnedMeshComponent.");
                return;
            }

            if (skinnedMeshComp.Mesh == null)
            {
                Console.WriteLine($"GameObject {gameObject.Name} has a SkinnedMeshComponent but no Mesh assigned.");
                return;
            }
            
            skinnedMeshComp.Material?.Apply(gameObject.GetComponent<TransformComponent>()?.WorldMatrix ?? Matrix4.Identity, camera);

            GL.BindVertexArray(skinnedMeshComp.Mesh.MeshData.VAO);
            if (skinnedMeshComp.Mesh.DrawMode == DrawMode.Triangles)
                GL.DrawElements(PrimitiveType.Triangles, skinnedMeshComp.Mesh.MeshData.Indices.Length,
                    DrawElementsType.UnsignedInt, 0);

            else if(skinnedMeshComp.Mesh.DrawMode == DrawMode.TriangleStrip)
                GL.DrawElements(PrimitiveType.TriangleStrip, skinnedMeshComp.Mesh.MeshData.Indices.Length,
                    DrawElementsType.UnsignedInt, 0);

        }
    }
}
