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
            0, 1, 3,
            1, 2, 3
        };
    }
}
