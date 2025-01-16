using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using StbImageSharp;
using System.IO;
using System.Drawing;

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

        private uint[] _indicies =
        {
            0, 1, 3,
            1, 2, 3
        };

        private int _vertexBufferObject, _vertexArrayObject, _elementBufferObject, _textureHandle;
        private Shader? _shader;

        public Game(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings() { Size = (width, height), Title = title })
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
         
            GL.ClearColor(0f,0f,0f,1f);
            
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indicies.Length * sizeof(uint), _indicies, BufferUsageHint.StaticDraw);

            int aPositionLocation = _shader.GetAttribLocation("aPosition");
            int texCoordLocation = _shader.GetAttribLocation("aTexCoords");
            int aColourLocation = _shader.GetAttribLocation("aColour");
            
            GL.VertexAttribPointer(
                aPositionLocation,
                3,
                VertexAttribPointerType.Float,
                false,
                8 * sizeof(float),
                0);
            GL.VertexAttribPointer(
                texCoordLocation,
                2,
                VertexAttribPointerType.Float,
                false,
                8 * sizeof(float),
                3 * sizeof(float));
            GL.VertexAttribPointer(
                aColourLocation,
                3,
                VertexAttribPointerType.Float,
                false,
                8 * sizeof(float),
                5 * sizeof(float));

            GL.EnableVertexAttribArray(aPositionLocation);
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.EnableVertexAttribArray(aColourLocation);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            _textureHandle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _textureHandle);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            using var stream = File.OpenRead("Textures/rocky_ground.jpg");
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, 
                PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);


            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (_shader!=null)
                _shader.Use();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, _textureHandle);

            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawElements(PrimitiveType.Triangles, _indicies.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args);
            GL.Viewport(0,0,args.Width, args.Height);
        }
    }
}