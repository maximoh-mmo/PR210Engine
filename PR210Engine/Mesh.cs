using OpenTK.Graphics.OpenGL;

namespace PR210Engine
{
    internal class Mesh
    {
        private int _vertexBufferObject;
        private int _elementBufferObject;
        private int _vertexArrayObject;
        private uint[] _indices;
        private float[] _vertices;

        public Mesh(float[] vertices, uint[] indices)
        {
            _vertices = vertices;
            _indices = indices;

            _vertexBufferObject = GL.GenBuffer();
            _vertexArrayObject = GL.GenVertexArray();
            _elementBufferObject = GL.GenBuffer();

            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(0);
        }

        public void Bind()
        {
            GL.BindVertexArray(_vertexArrayObject);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}