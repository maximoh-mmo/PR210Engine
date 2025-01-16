using OpenTK.Graphics.OpenGL;

namespace PR210Engine
{
    internal class RenderObject
    {
        private Mesh _mesh;
        private Texture _texture;
        private Shader _shader;

        public RenderObject(Mesh mesh, Texture texture)
        {
            _mesh = mesh;
            _texture = texture;
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            int aPositionLocation = _shader.GetAttribLocation("aPosition");
            int texCoordLocation = _shader.GetAttribLocation("aTexCoords");
            int aColourLocation = _shader.GetAttribLocation("aColour");

            mesh.Bind();

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

            GL.BindVertexArray(0);

        }

        public void Render()
        {
            _shader.Use();
            _texture.Bind();
            _mesh.Bind();
            _mesh.Draw();
            _mesh.Unbind();
            _texture.Unbind();
        }
    }
}