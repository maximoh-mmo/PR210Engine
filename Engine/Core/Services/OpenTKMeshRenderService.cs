using Engine.Core.Components;
using Engine.Core.DataTypes;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Engine.Render;

namespace Engine.Core.Services
{
    public class OpenTKMeshRenderService : IMeshRenderingService
    {
        public void Render(GameObject gameObject, float aspectRatio)
        {
            var skinnedMeshComp = gameObject.GetComponent<SkinnedMeshComponent>();
            if (skinnedMeshComp == null) return;
            if (skinnedMeshComp.Mesh == null) return;
            
            skinnedMeshComp.Material?.Apply(
                gameObject.GetComponent<TransformComponent>()?.WorldMatrix ?? Matrix4.Identity,
                Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f),
                aspectRatio);

            GL.BindVertexArray(skinnedMeshComp.Mesh.MeshData.VAO);
            if (skinnedMeshComp.Mesh.DrawMode == DrawMode.Triangles)
                GL.DrawElements(PrimitiveType.Triangles, skinnedMeshComp.Mesh.MeshData.Indices.Length,
                    DrawElementsType.UnsignedInt, 0);
        }
    }
}
