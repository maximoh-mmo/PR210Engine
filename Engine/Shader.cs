using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Engine
{
    internal class Shader
    {
        public int Handle { get; private set; }
        public int VertexShader { get; private set; }
        public int FragmentShader { get; private set; }


        public Shader(string vertPath, string fragPath)
        {
            string vertexShaderPath = File.ReadAllText(Path.GetFullPath(vertPath));
            string fragmentShaderPath = File.ReadAllText(Path.GetFullPath(fragPath));

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, fragmentShaderPath);

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, vertexShaderPath);

            GL.CompileShader(FragmentShader);
            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int code);
            if (code == 0)
            {
                throw new Exception(GL.GetShaderInfoLog(FragmentShader));
            }

            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out code);
            if (code == 0)
            {
                throw new Exception(GL.GetShaderInfoLog(VertexShader));
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out code);
            if (code == 0)
            {
                throw new Exception(GL.GetProgramInfoLog(Handle));
            }

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);

            GL.DeleteShader(VertexShader);
            GL.DeleteShader(FragmentShader);

            Use();
        }

        public void Use()
        {
            GL.UseProgram(Handle);
            var viewMatrix = Matrix4.CreateTranslation(-.50f, 0.5f, -3);
            SetMatrix4("view", viewMatrix);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        public void SetMatrix4(string name, Matrix4 matrix)
        {
            int loc = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(loc, false, ref matrix);
        }


        public void SetFloat(string name, float value)
        {
            int loc = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(loc, value);
        }

        public void SetVector2(string name, Vector2 vector)
        {
            int loc = GL.GetUniformLocation(Handle, name);
            GL.Uniform2(loc, vector);
        }

        public void SetVector3(string name, Vector3 vector)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform3(location, vector);
        }

        public void SetVector4(string name, Vector4 vector)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform4(location, vector);
        }

        public void SetInt(string name, int value)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }
    }
}

