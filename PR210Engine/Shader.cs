
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace PR210Engine
{
    internal class Shader
    {
        private int _handle;
        private int _vertexShader;
        private int _fragmentShader;

        public Shader(string vertPath, string fragPath)
        {
            string vertexShaderPath = File.ReadAllText(Path.GetFullPath(vertPath));
            string fragmentShaderPath = File.ReadAllText(Path.GetFullPath(fragPath));

            _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragmentShader, fragmentShaderPath);

            _vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShader,vertexShaderPath);
            
            GL.CompileShader(_fragmentShader);
            GL.GetShader(_fragmentShader,ShaderParameter.CompileStatus, out int code);
            if (code == 0)
            {
                throw new Exception(GL.GetShaderInfoLog(_fragmentShader));
            }
            
            GL.CompileShader(_vertexShader);
            GL.GetShader((_vertexShader), ShaderParameter.CompileStatus, out code);
            if (code == 0)
            {
                throw new Exception(GL.GetShaderInfoLog(_vertexShader));
            }

            _handle = GL.CreateProgram();
            
            GL.AttachShader(_handle, _vertexShader);
            GL.AttachShader(_handle, _fragmentShader);

            GL.LinkProgram(_handle);
            GL.GetProgram(_handle, GetProgramParameterName.LinkStatus, out code);
            if (code == 0)
            {
                throw new Exception(GL.GetProgramInfoLog(_handle));
            }
            GL.DetachShader(_handle, _vertexShader);
            GL.DetachShader(_handle, _fragmentShader);

            GL.DeleteShader(_vertexShader);
            GL.DeleteShader(_fragmentShader);
            
            Use();
        }

        public void SetMatrix4(string name, Matrix4 matrix)
        {
            int loc = GL.GetUniformLocation(_handle, name);
            GL.UniformMatrix4(loc, false, ref matrix);
        }

        public void Use()
        {
            GL.UseProgram(_handle);
            var viewMatrix = Matrix4.CreateTranslation(-.50f, 0.5f, -3);
            var projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(80), 800f / 600f, 0.1f, 100f);
            SetMatrix4("view", viewMatrix);
            SetMatrix4("projection", projectionMatrix);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(_handle,attribName);
        }
    }
}
