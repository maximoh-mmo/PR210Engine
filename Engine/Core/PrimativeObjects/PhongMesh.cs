namespace Engine.Core.PrimativeObjects
{
    public class PhongMesh : Mesh
    {
        private static readonly float[] _vertices =
        [
            // Positions           // Normals           // TexCoords
            // Front face
            -0.5f, -0.5f,  0.5f,   0f,  0f,  1f,       0f, 0f,
            0.5f, -0.5f,  0.5f,   0f,  0f,  1f,       1f, 0f,
            0.5f,  0.5f,  0.5f,   0f,  0f,  1f,       1f, 1f,
            -0.5f,  0.5f,  0.5f,   0f,  0f,  1f,       0f, 1f,
            // Back face
            -0.5f, -0.5f, -0.5f,   0f,  0f, -1f,       1f, 0f,
            0.5f, -0.5f, -0.5f,   0f,  0f, -1f,       0f, 0f,
            0.5f,  0.5f, -0.5f,   0f,  0f, -1f,       0f, 1f,
            -0.5f,  0.5f, -0.5f,   0f,  0f, -1f,       1f, 1f,
            // Left face
            -0.5f,  0.5f,  0.5f,  -1f,  0f,  0f,       1f, 1f,
            -0.5f,  0.5f, -0.5f,  -1f,  0f,  0f,       0f, 1f,
            -0.5f, -0.5f, -0.5f,  -1f,  0f,  0f,       0f, 0f,
            -0.5f, -0.5f,  0.5f,  -1f,  0f,  0f,       1f, 0f,
            // Right face
            0.5f,  0.5f,  0.5f,   1f,  0f,  0f,       0f, 1f,
            0.5f,  0.5f, -0.5f,   1f,  0f,  0f,       1f, 1f,
            0.5f, -0.5f, -0.5f,   1f,  0f,  0f,       1f, 0f,
            0.5f, -0.5f,  0.5f,   1f,  0f,  0f,       0f, 0f,
            // Top face
            -0.5f,  0.5f, -0.5f,   0f,  1f,  0f,       0f, 1f,
            0.5f,  0.5f, -0.5f,   0f,  1f,  0f,       1f, 1f,
            0.5f,  0.5f,  0.5f,   0f,  1f,  0f,       1f, 0f,
            -0.5f,  0.5f,  0.5f,   0f,  1f,  0f,       0f, 0f,
            // Bottom face
            -0.5f, -0.5f, -0.5f,   0f, -1f,  0f,       1f, 1f,
            0.5f, -0.5f, -0.5f,   0f, -1f,  0f,       0f, 1f,
            0.5f, -0.5f,  0.5f,   0f, -1f,  0f,       0f, 0f,
            -0.5f, -0.5f,  0.5f,   0f, -1f,  0f,       1f, 0f
        ];

        private static readonly uint[] _indices =
        [
            // Front face
            0, 1, 2, 2, 3, 0,
            // Back face
            4, 5, 6, 6, 7, 4,
            // Left face
            8, 9,10,10,11, 8,
            // Right face
            12,13,14,14,15,12,
            // Top face
            16,17,18,18,19,16,
            // Bottom face
            20,21,22,22,23,20
        ];

        public PhongMesh() : base(_vertices, _indices, DrawMode.Triangles)
        {
        }
    }
}
