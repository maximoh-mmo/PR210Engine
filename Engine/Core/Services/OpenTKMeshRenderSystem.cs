using Engine.Core.Components;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Core.Services
{
    public class OpenTKMeshRenderSystem : IMeshRenderingService
    {
        public void Render(GameObject gameObject, float aspectRatio)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            var skinnedMeshComp = gameObject.GetComponent<SkinnedMeshComponent>();
            if (skinnedMeshComp == null) return;
            if (skinnedMeshComp.Mesh == null) return;

            Matrix4 Model = gameObject.GetComponent<TransformComponent>()?.ModelMatrix ??
                            Matrix4.Identity;

            Matrix4.CreateRotationX(5, out var result);

            Model = Model * result;

            skinnedMeshComp.Material?.Apply(Model,
                Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f),
                aspectRatio);

            GL.BindVertexArray(skinnedMeshComp.Mesh.MeshData.VAO);
            if (skinnedMeshComp.Mesh.DrawMode == DrawMode.Triangles)
                GL.DrawElements(PrimitiveType.Triangles, skinnedMeshComp.Mesh.MeshData.Indices.Length,
                    DrawElementsType.UnsignedInt, 0);
        }
    }
}
