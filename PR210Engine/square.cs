using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR210Engine
{
    internal static class Square
    {
        public static float[] Vertices = new float[]
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

        public static uint[] Indicies =
        {
            0, 1, 2,  2, 3, 0,  // Front face
            4, 5, 6,  6, 7, 4,  // Back face
            8, 9, 10, 10,11,8,  // Right face
            12,13,14, 14,15,12, // Left face
            16,17,18, 18,19,16, // Top face
            20,21,22, 22,23,20  // Bottom face
        };

    }
}
