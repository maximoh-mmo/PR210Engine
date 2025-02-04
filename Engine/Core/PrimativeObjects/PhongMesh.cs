using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.PrimativeObjects
{
    internal class PhongMesh : Mesh
    {
        private static float[] vertices =
        {
            // Front face
            -0.5f, -0.5f, 0.5f, 0f, 0f, 1f, 0f, 0f,
            0.5f, -0.5f, 0.5f, 0f, 0f, 1f, 1f, 0f,
            0.5f, 0.5f, 0.5f, 0f, 0f, 1f, 1f, 1f,
            -0.5f, 0.5f, 0.5f, 0f, 0f, 1f, 0f, 1f,
            // Back face
            -0.5f, -0.5f, -0.5f, 0f, 0f, -1f, 0f, 0f,
            0.5f, -0.5f, -0.5f, 0f, 0f, -1f, 1f, 0f,
            0.5f, 0.5f, -0.5f, 0f, 0f, -1f, 1f, 1f,
            -0.5f, 0.5f, -0.5f, 0f, 0f, -1f, 0f, 1f,
            // Left face
            -0.5f, 0.5f, 0.5f, -1f, 0f, 0f, 1f, 1f,
            -0.5f, 0.5f, -0.5f, -1f, 0f, 0f, 0f, 1f,
            -0.5f, -0.5f, -0.5f, -1f, 0f, 0f, 0f, 0f,
            -0.5f, -0.5f, 0.5f, -1f, 0f, 0f, 1f, 0f,
            // Right face
            0.5f, 0.5f, 0.5f, 1f, 0f, 0f, 1f, 1f,
            0.5f, 0.5f, -0.5f, 1f, 0f, 0f, 0f, 1f,
            0.5f, -0.5f, -0.5f, 1f, 0f, 0f, 0f, 0f,
            0.5f, -0.5f, 0.5f, 1f, 0f, 0f, 1f, 0f,
            // Top face
            -0.5f, 0.5f, -0.5f, 0f, 1f, 0f, 0f, 1f,
            0.5f, 0.5f, -0.5f, 0f, 1f, 0f, 1f, 1f,
            0.5f, 0.5f, 0.5f, 0f, 1f, 0f, 1f, 0f,
            -0.5f, 0.5f, 0.5f, 0f, 1f, 0f, 0f, 0f,
            // Bottom face
            -0.5f, -0.5f, -0.5f, 0f, -1f, 0f, 0f, 1f,
            0.5f, -0.5f, -0.5f, 0f, -1f, 0f, 1f, 1f,
            0.5f, -0.5f, 0.5f, 0f, -1f, 0f, 1f, 0f,
            -0.5f, -0.5f, 0.5f, 0f, -1f, 0f, 0f, 0f
        };

        private static uint[] indices =
        {
            0, 1, 2, 2, 3, 0, // Front face
            4, 5, 6, 6, 7, 4, // Back face
            8, 9, 10, 10, 11, 8, // Right face
            12, 13, 14, 14, 15, 12, // Left face
            16, 17, 18, 18, 19, 16, // Top face
            20, 21, 22, 22, 23, 20 // Bottom face
        };

        public PhongMesh() : base(vertices, indices, DrawMode.Triangles)
        {
        }
    }
}
