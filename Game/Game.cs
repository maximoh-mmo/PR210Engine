using System.Diagnostics;
using Engine;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.DebugGUI;
using Engine.Core.Logging;
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
        private IPerformanceService performanceService => serviceProvider?.GetRequiredService<IPerformanceService>();

        RenderSystem? RenderSystem;
        private List<GameObject> gameObjects = new();
        GameObject Cube = new("Cube");
        GameObject Sphere = new("Sphere");
        float _aspectRatio;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var cubeTransform = Cube.GetComponent<TransformComponent>();
            if (cubeTransform != null)
            {
                cubeTransform.Rotation += new Vector3(0, (float)args.Time * MathHelper.DegreesToRadians(90f), (float)args.Time * MathHelper.DegreesToRadians(-90f));
            } 
            
            serviceProvider.GetService<TransformSystem>()?.Update(gameObjects);

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
            GL.Enable(EnableCap.DepthTest);
            GL.CullFace(TriangleFace.Back);

            serviceCollection.AddSingleton<IMeshRenderingService, OpenTKMeshRenderService>();
            serviceCollection.AddSingleton<RenderSystem>();

            serviceCollection.AddSingleton<ILoggingService, SerilogLoggingService>();
            serviceCollection.AddSingleton<SerilogLoggingService>();

            serviceCollection.AddSingleton<TransformSystem>();

            serviceCollection.AddSingleton<IPerformanceService, PerformanceService>();
            serviceCollection.AddSingleton<IDebugRenderer, ImGuiPerformanceRenderer>();
            serviceCollection.AddSingleton<IDebugGuiService>(sp => new OpenTKImGuiService(this));

            serviceProvider = serviceCollection.BuildServiceProvider();
            
            IMeshRenderingService meshRenderer = new OpenTKMeshRenderService();
            RenderSystem = new RenderSystem(meshRenderer);

            //Texture diffuse = new Texture();
            //Texture specular = new Texture();
            //diffuse.LoadTexture("Textures/rocky_ground.jpg");
            //specular.LoadTexture("Textures/specular.png");

            //Material material = new Material(new PhongShaderDescriptor());
            //material.SetProperty("material.shininess", 0.2f);
            //material.SetProperty("material.diffuse", diffuse);
            //material.SetProperty("material.specular", specular);

            Texture texture = new Texture();
            texture.LoadTexture("Textures/rocky_ground.jpg");

            Material material = new Material(new DiffuseDescriptor());
            material.SetProperty("material.diffuse", texture);

            Sphere.AddComponent(new SkinnedMeshComponent()
            {
                Material = material,
                Mesh = new SphereMesh()
            });
            Sphere.AddComponent(new TransformComponent());
            Sphere.AddComponent(new HierarchyComponent());
            Sphere.GetComponent<TransformComponent>()!.Position = new Vector3(1, 0, 0);
            gameObjects.Add(Sphere);
            performanceService.GameObjects.Add(Sphere);

            Cube.AddComponent(new SkinnedMeshComponent()
            {
                Material = material,
                Mesh = new CubeMesh()
            });
            Cube.AddComponent(new TransformComponent());
            Cube.AddComponent(new HierarchyComponent());
            HierarchyComponent.SetParent(Cube, Sphere);
            gameObjects.Add(Cube);
            performanceService.GameObjects.Add(Cube);
            serviceProvider.GetService<ILoggingService>()?.LogTrace("Cube created with SkinnedMeshComponent");

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
            performanceService.BeginFrame();
            base.OnRenderFrame(args);
            serviceProvider?.GetService<RenderSystem>()?.Render(gameObjects,_aspectRatio);
            serviceProvider?.GetService<IDebugGuiService>()?.Update((float)args.Time);
            serviceProvider?.GetService<IDebugRenderer>()?.Render();
            serviceProvider?.GetService<IDebugGuiService>().Render();
            SwapBuffers();
            performanceService.EndFrame();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args);
            GL.Viewport(0, 0, args.Width, args.Height);
            _aspectRatio = args.Width / args.Height;
        }
    }
}