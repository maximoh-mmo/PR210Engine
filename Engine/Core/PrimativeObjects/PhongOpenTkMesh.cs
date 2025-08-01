using Engine.Core.DataTypes;

namespace Engine.Core.PrimativeObjects
{
    public class PhongOpenTkMesh() : OpenTKMesh(Vertices, Indices, DrawMode.Triangles)
    {
        private static readonly float[] Vertices =
        [
            -0.5f, -0.5f,  0.5f,   0f,  0f,  1f,       0f, 0f,
            0.5f, -0.5f,  0.5f,   0f,  0f,  1f,       1f, 0f,
            0.5f,  0.5f,  0.5f,   0f,  0f,  1f,       1f, 1f,
            -0.5f,  0.5f,  0.5f,   0f,  0f,  1f,       0f, 1f,
            -0.5f, -0.5f, -0.5f,   0f,  0f, -1f,       1f, 0f,
            0.5f, -0.5f, -0.5f,   0f,  0f, -1f,       0f, 0f,
            0.5f,  0.5f, -0.5f,   0f,  0f, -1f,       0f, 1f,
            -0.5f,  0.5f, -0.5f,   0f,  0f, -1f,       1f, 1f,
            -0.5f,  0.5f,  0.5f,  -1f,  0f,  0f,       1f, 1f,
            -0.5f,  0.5f, -0.5f,  -1f,  0f,  0f,       0f, 1f,
            -0.5f, -0.5f, -0.5f,  -1f,  0f,  0f,       0f, 0f,
            -0.5f, -0.5f,  0.5f,  -1f,  0f,  0f,       1f, 0f,
            0.5f,  0.5f,  0.5f,   1f,  0f,  0f,       0f, 1f,
            0.5f,  0.5f, -0.5f,   1f,  0f,  0f,       1f, 1f,
            0.5f, -0.5f, -0.5f,   1f,  0f,  0f,       1f, 0f,
            0.5f, -0.5f,  0.5f,   1f,  0f,  0f,       0f, 0f,
            -0.5f,  0.5f, -0.5f,   0f,  1f,  0f,       0f, 1f,
            0.5f,  0.5f, -0.5f,   0f,  1f,  0f,       1f, 1f,
            0.5f,  0.5f,  0.5f,   0f,  1f,  0f,       1f, 0f,
            -0.5f,  0.5f,  0.5f,   0f,  1f,  0f,       0f, 0f,
            -0.5f, -0.5f, -0.5f,   0f, -1f,  0f,       1f, 1f,
            0.5f, -0.5f, -0.5f,   0f, -1f,  0f,       0f, 1f,
            0.5f, -0.5f,  0.5f,   0f, -1f,  0f,       0f, 0f,
            -0.5f, -0.5f,  0.5f,   0f, -1f,  0f,       1f, 0f
        ];

        private static readonly uint[] Indices =
        [
            0, 1, 2, 2, 3, 0,
            4, 5, 6, 6, 7, 4,
            8, 9,10,10,11, 8,
            12,13,14,14,15,12,
            16,17,18,18,19,16,
            20,21,22,22,23,20
        ];
    }
}
