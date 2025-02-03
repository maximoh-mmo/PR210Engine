using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace PR210Engine
{
    internal class RenderObject
    {
        private Mesh _mesh;
        private Texture _texture;
        private Shader _shader;
        private Matrix4 _modelMatrix;
        private float _scale;
        private Vector3 _position;
        private Vector3 _rotation;

        public RenderObject(Mesh mesh, Texture texture)
        {
            _scale = 1f;
            _position = Vector3.Zero;
            _rotation = Vector3.Zero;
            _modelMatrix = Matrix4.CreateScale(_scale) *
                           Matrix4.CreateRotationX(_rotation.X) *
                           Matrix4.CreateRotationY(_rotation.Y) *
                           Matrix4.CreateRotationZ(_rotation.Z) *
                           Matrix4.CreateTranslation(_position);

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
        public void SetMatrix4(string name, Matrix4 matrix)
        {
            _shader.SetMatrix4(name, matrix);
        }

        public void SetRotation(Matrix4 rotationMatrix)
        {
            _modelMatrix = rotationMatrix + _modelMatrix;
        }
        public void Render()
        {
            _shader.Use();
            SetMatrix4("model", _modelMatrix);
            _texture.Bind();
            _mesh.Bind();
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _mesh.Draw();
            _mesh.Unbind();
            _texture.Unbind();
        }
    }
}