using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;

namespace PR210Engine
{
    public class Game : GameWindow
    {
        private float[] _vertices = new float[]
        {
            -0.5f,0.5f,-0.5f, 1,1, 1,0,0,
            -0.5f,-0.5f,-0.5f, 1,0, 0,1,0,
            0.5f,-0.5f,-0.5f, 0,0, 0,0,1,
            0.5f,0.5f,-0.5f, 0,1, 1,1,0,
            -0.5f,0.5f,0.5f,1,1, 1,0,0,
            -0.5f,-0.5f,0.5f,1,0, 0,1,0,
            0.5f,-0.5f,0.5f,0,0, 0,0,1,
            0.5f,0.5f,0.5f,0,1, 1,1,0,
            0.5f,0.5f,-0.5f,1,1, 1,0,0,
            0.5f,-0.5f,-0.5f,1,0, 0,1,0,
            0.5f,-0.5f,0.5f, 0,0, 0,0,1,
            0.5f,0.5f,0.5f, 0,1, 1,1,0,
            -0.5f,0.5f,-0.5f,1,1, 1,0,0,
            -0.5f,-0.5f,-0.5f,1,0, 0,1,0,
            -0.5f,-0.5f,0.5f, 0,0, 0,0,1,
            -0.5f,0.5f,0.5f, 0,1, 1,1,0,
            -0.5f,0.5f,0.5f,1,1, 1,0,0,
            -0.5f,0.5f,-0.5f,1,0, 0,1,0,
            0.5f,0.5f,-0.5f, 0,0, 0,0,1,
            0.5f,0.5f,0.5f, 0,1, 1,1,0,
            -0.5f,-0.5f,0.5f,1,1, 1,0,0,
            -0.5f,-0.5f,-0.5f,1,0, 0,1,0,
            0.5f,-0.5f,-0.5f, 0,0, 0,0,1,
            0.5f,-0.5f,0.5f, 0,1, 1,1,0
        };

        private uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

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

            Mesh mesh = new Mesh(_vertices, _indices);
            Texture texture = new Texture("Textures/rocky_ground.jpg");
            _renderObjects = [new RenderObject(mesh, texture)];
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (_renderObjects != null)
            {    
                foreach (var renderObject in _renderObjects)
                {
                    renderObject.Render();
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