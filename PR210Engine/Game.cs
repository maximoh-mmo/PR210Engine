using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using System.Reflection;
using OpenTK.Mathematics;

namespace PR210Engine
{
    public class Game : GameWindow
    {
        private RenderObject[]? _renderObjects;

        public Game(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings() { ClientSize = (width, height), Title = title })
        {
        }

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

            GL.ClearColor(0f, 0f, 0f, 1f);

            Mesh mesh = new Mesh(Square.Vertices, Square.Indicies);
            Texture texture = new Texture("Textures/rocky_ground.jpg");
            _renderObjects = [new RenderObject(mesh, texture)];
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (_renderObjects != null)
            {    
                for (int i = 0; i < _renderObjects.Length; i++)
                {
                    float angle = 20.0f * i;
                    var rotation = Matrix4.CreateRotationY((float)(angle * TimeSinceLastUpdate()));
                    _renderObjects[i].SetRotation(rotation);
                    _renderObjects[i].Render();
                    


                }
            }
            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args);
            GL.Viewport(0, 0, args.Width, args.Height);
        }
    }
}