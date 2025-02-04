using OpenTK.Graphics.OpenGL;

namespace Engine.Core
{
    public struct MeshData
    {
        public int VAO;
        public uint[] Indices;
        public float[] Vertices;
    }

    public enum DrawMode
    {
        Triangles,
        TriangleStrip,
        Lines
    }

    public interface IMesh
    {
        MeshData MeshData { get; }
        DrawMode DrawMode { get; }
    }

    public class Mesh : IMesh
    {
        public MeshData MeshData { get; }
        public DrawMode DrawMode { get; }

        public Mesh(float[] vertices, uint[] indices, DrawMode drawMode)
        {
            MeshData = new MeshData()
            {
                Indices = indices,
                Vertices = vertices,
                VAO = GL.GenVertexArray()
            };
            // Vertex Buffer Object (VBO) ID (Name) bekommen
            var VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            // Vertexdaten in VBO laden
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Vertex Array Object (VAO)
            GL.BindVertexArray(MeshData.VAO);

            // Indexliste in Element Buffer Object (EBO) laden und damit an VAO binden
            var EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            // VAO binden
            GL.BindVertexArray(MeshData.VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO); // Add this line

            // Position attribute
            GL.EnableVertexAttribArray(0);

            // Texture coordinates attribute  
            GL.EnableVertexAttribArray(1);

            // Color attribute
            GL.EnableVertexAttribArray(2);
            DrawMode = drawMode;
            SetVertexAttributes();
        }
    
        public void SetVertexAttributes()
        {
            GL.VertexAttribPointer(
                0,
                3,
                VertexAttribPointerType.Float,
                false,
                8 * sizeof(float),
                0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(
                1,
                2,
                VertexAttribPointerType.Float,
                false,
                8 * sizeof(float),
                3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(
                2,
                3,
                VertexAttribPointerType.Float,
                false,
                8 * sizeof(float),
                5 * sizeof(float));
            GL.EnableVertexAttribArray(2);
        }
    }
}