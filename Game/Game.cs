using System.Diagnostics;
using Engine;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.DebugGUI;
using Engine.Core.Logging;
using Engine.Core.PrimativeObjects;
using Engine.Core.Services;
using Engine.Core.Systems;
using Engine.Material;
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
        private readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private IServiceProvider? _serviceProvider;
        private IPerformanceService? PerformanceService => _serviceProvider?.GetRequiredService<IPerformanceService>();

        private RenderSystem? _renderSystem;
        private readonly List<GameObject> _gameObjects = [];
        private readonly GameObject _cube = new("Cube");
        private readonly GameObject _sphere = new("Sphere");
        private float _aspectRatio;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var cubeTransform = _cube.GetComponent<TransformComponent>();
            if (cubeTransform != null)
            {
                cubeTransform.Rotation += new Vector3(0, (float)args.Time * MathHelper.DegreesToRadians(90f), (float)args.Time * MathHelper.DegreesToRadians(-90f));
            } 
            
            _serviceProvider.GetService<TransformSystem>()?.Update(_gameObjects);

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

            _serviceCollection.AddSingleton<IMeshRenderingService, OpenTKMeshRenderService>();
            _serviceCollection.AddSingleton<RenderSystem>();

            _serviceCollection.AddSingleton<ILoggingService, SerilogLoggingService>();
            _serviceCollection.AddSingleton<SerilogLoggingService>();

            _serviceCollection.AddSingleton<TransformSystem>();

            _serviceCollection.AddSingleton<IPerformanceService, PerformanceService>();
            _serviceCollection.AddSingleton<IDebugRenderer, ImGuiPerformanceRenderer>();
            _serviceCollection.AddSingleton<IDebugGuiService>(sp => new OpenTKImGuiService(this));

            _serviceProvider = _serviceCollection.BuildServiceProvider();
            
            IMeshRenderingService meshRenderer = new OpenTKMeshRenderService();
            _renderSystem = new RenderSystem(meshRenderer);

            var texture = new Texture();
            texture.LoadTexture("Textures/rocky_ground.jpg");

            var material = new Material(new DiffuseDescriptor());
            material.SetProperty("material.diffuse", texture);

            _sphere.AddComponent(new SkinnedMeshComponent()
            {
                Material = material,
                Mesh = new SphereMesh()
            });

            _sphere.AddComponent(new TransformComponent());
            _sphere.AddComponent(new HierarchyComponent());
            _sphere.GetComponent<TransformComponent>()!.Position = new Vector3(1, 0, 0);
            _gameObjects.Add(_sphere);
            PerformanceService.GameObjects.Add(_sphere);

            _cube.AddComponent(new SkinnedMeshComponent()
            {
                Material = material,
                Mesh = new CubeMesh()
            });
            _cube.AddComponent(new TransformComponent());
            _cube.AddComponent(new HierarchyComponent());
            HierarchyComponent.SetParent(_cube, _sphere);
            _gameObjects.Add(_cube);
            PerformanceService.GameObjects.Add(_cube);
            _serviceProvider.GetService<ILoggingService>()?.LogTrace("Cube created with SkinnedMeshComponent");

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
            PerformanceService.BeginFrame();
            base.OnRenderFrame(args);
            _serviceProvider?.GetService<RenderSystem>()?.Render(_gameObjects,_aspectRatio);
            _serviceProvider?.GetService<IDebugGuiService>()?.Update((float)args.Time);
            _serviceProvider?.GetService<IDebugRenderer>()?.Render();
            _serviceProvider?.GetService<IDebugGuiService>().Render();
            SwapBuffers();
            PerformanceService.EndFrame();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args);
            GL.Viewport(0, 0, args.Width, args.Height);
            _aspectRatio = args.Width / args.Height;
        }
    }
}