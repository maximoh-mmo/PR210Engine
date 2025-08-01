using System.Diagnostics;
using Engine;
using Engine.Core;
using Engine.Core.Camera;
using Engine.Core.Components;
using Engine.Core.DebugGUI;
using Engine.Core.Interfaces;
using Engine.Core.PrimativeObjects;
using Engine.Core.Services;
using Engine.Core.Systems;
using Engine.Material;
using Engine.Render.Graphics;
using Engine.Shaders.Descriptors;
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
        private readonly GameObject _camera = new("Camera");
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

            if (_camera.GetComponent<CameraComponent>() != null)
            {
                var moveSpeed = 5f * (float)args.Time;
                CameraComponent cameraComponent = _camera.GetComponent<CameraComponent>()!;
                if (KeyboardState.IsKeyDown(Keys.W))
                {
                    cameraComponent.Position +=
                        cameraComponent.Forward * moveSpeed;
                }

                if (KeyboardState.IsKeyDown(Keys.S))
                {
                    cameraComponent.Position -=
                        cameraComponent.Forward * moveSpeed;
                }

                if (KeyboardState.IsKeyDown(Keys.A))
                {
                    cameraComponent.Position -=
                        cameraComponent.Right * moveSpeed;
                }

                if (KeyboardState.IsKeyDown(Keys.D))
                {
                    cameraComponent.Position +=
                        cameraComponent.Right * moveSpeed;
                }
            }

            _serviceProvider?.GetService<CameraSystem>()?.Update(_gameObjects, _aspectRatio);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnLoad()
        {

            _aspectRatio = Size.X / (float)Size.Y;
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
            _serviceCollection.AddSingleton<CameraSystem>();

            _serviceCollection.AddSingleton<IPerformanceService, PerformanceService>();
            _serviceCollection.AddSingleton<IDebugRenderer, ImGuiPerformanceRenderer>();
            //_serviceCollection.AddSingleton<IDebugRenderer, RenderPipelineDebugRenderer>();
            _serviceCollection.AddSingleton<IDebugGuiService>(sp => new OpenTKImGuiService(this));

            _serviceCollection.AddSingleton<IRenderContext, OpenTKRenderContext>();
            _serviceCollection.AddSingleton<IRenderSystem, RenderSystem>();
            _serviceCollection.AddSingleton<ICullingService, FrustrumCullingService>();
            //_serviceCollection.AddSingleton<ICullingService, NonRenderableCullingService>();

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
                Mesh = new SphereOpenTkMesh()
            });
            _sphere.AddComponent(new BoundingSphereComponent(null, 0.5f));
            _sphere.AddComponent(new TransformComponent());
            _sphere.AddComponent(new HierarchyComponent());
            _sphere.GetComponent<TransformComponent>()!.Position = new Vector3(1, 0, 0);
            _gameObjects.Add(_sphere);
            PerformanceService!.GameObjects.Add(_sphere);

            _cube.AddComponent(new SkinnedMeshComponent()
            {
                Material = material,
                Mesh = new CubeOpenTkMesh()
            });
            _cube.AddComponent(new BoundingSphereComponent(null,0.5f));
            _cube.AddComponent(new TransformComponent());
            _cube.AddComponent(new HierarchyComponent());
            HierarchyComponent.SetParent(_cube, _sphere);
            _gameObjects.Add(_cube);
            PerformanceService.GameObjects.Add(_cube);
            _serviceProvider.GetService<ILoggingService>()?.LogTrace("Cube created with SkinnedMeshComponent");

            _camera.AddComponent(new CameraComponent()
            {
                FieldOfView = 45f,
                NearPlane = 0.1f,
                FarPlane = 100f
            });

            _gameObjects.Add(_camera);
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
            if (PerformanceService != null)
            {
                PerformanceService.BeginFrame();
                base.OnRenderFrame(args);
                
                _serviceProvider?.GetService<RenderSystem>()?.Render(_serviceProvider?.GetService<ICullingService>()?.Cull(_gameObjects, _camera.GetComponent<CameraComponent>()!)??
                                                                     _gameObjects, _camera.GetComponent<CameraComponent>()!);
                _serviceProvider?.GetService<IDebugGuiService>()?.Update((float)args.Time);
                _serviceProvider?.GetService<IDebugRenderer>()?.Render();
                _serviceProvider?.GetService<IDebugGuiService>()?.Render();
                SwapBuffers();
                PerformanceService.EndFrame();
            }
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args);
            GL.Viewport(0, 0, args.Width, args.Height);
            _aspectRatio = args.Width / args.Height;
            _serviceProvider?.GetService<IDebugGuiService>()?.OnResize(args.Width,args.Height);
        }
    }
}