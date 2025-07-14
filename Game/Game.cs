

using Engine;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.PrimativeObjects;
using Engine.Core.Services;
using Engine.Core.Systems;
using Engine.Materials;
using Engine.Render;
using Engine.Window;
using Microsoft.Extensions.DependencyInjection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Game
{
    public class Game(int width, int height, string title) : FixedTimestepWindow(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {
        private readonly IServiceCollection serviceCollection = new ServiceCollection();
        private IServiceProvider? serviceProvider;

        RenderSystem? RenderSystem;
        private List<GameObject> gameObjects;
        GameObject Cube = new();

        float _aspectRatio;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1f);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(TriangleFace.Back);

            var mesh = new PhongMesh();

            serviceCollection.AddSingleton<IMeshRenderingService, OpenTKMeshRenderSystem>();
            serviceCollection.AddSingleton<RenderSystem>();

            serviceCollection.AddSingleton<ILoggingService, SerilogLoggingService>();
            serviceCollection.AddSingleton<SerilogLoggingService>();

            serviceProvider = serviceCollection.BuildServiceProvider();

            IMeshRenderingService meshRenderer = new OpenTKMeshRenderSystem();
            RenderSystem = new RenderSystem(meshRenderer);
            
            Texture diffuse = new Texture();
            Texture specular = new Texture();
            diffuse.LoadTexture("Textures/rocky_ground.jpg");
            specular.LoadTexture("Textures/specular.png");

            Material material = new Material(new PhongShaderDescriptor());
            material.SetProperty("material.diffuse", diffuse);
            material.SetProperty("material.specular", specular);
            material.SetProperty("material.shininess", 1f);

            Cube.AddComponent(new SkinnedMeshComponent()
            {
                Material = material,
                Mesh = mesh
            });
            
            serviceProvider.GetService<ILoggingService>()?.LogTrace("Cube created with SkinnedMeshComponent");

            Cube.AddComponent(new TransformComponent());
        }
        protected override void OnUnload()
        {
            base.OnUnload();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            serviceProvider.GetService<RenderSystem>()?.Render(new List<GameObject>(){Cube},_aspectRatio);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            var skinnedMeshComp = Cube.GetComponent<SkinnedMeshComponent>();
            if (skinnedMeshComp == null) return;
            if (skinnedMeshComp.Mesh == null) return;

            skinnedMeshComp.Material?.Apply(Cube.GetComponent<TransformComponent>()?.ModelMatrix ??
                                            Matrix4.Identity,

                Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f),
                (float)Size.X / Size.Y);

            GL.BindVertexArray(skinnedMeshComp.Mesh.MeshData.VAO);
            if (skinnedMeshComp.Mesh.DrawMode == DrawMode.Triangles)
                GL.DrawElements(PrimitiveType.Triangles, skinnedMeshComp.Mesh.MeshData.Indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args);
            GL.Viewport(0, 0, args.Width, args.Height);
            _aspectRatio = args.Width / args.Height;
        }
    }
}