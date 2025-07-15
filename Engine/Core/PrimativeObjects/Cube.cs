using Engine.Core;

namespace EduEngineCore.PrimitiveObjects;

public class CubeMesh : Mesh
{
    private static readonly float[] _vertices =
    [
        // Positions          // Texture Coords // Color
        // Front face
        -0.5f, -0.5f, 0.5f, 0f, 0f, 1f, 0f, 0f,
        0.5f, -0.5f, 0.5f, 1f, 0f, 1f, 0f, 0f,
        0.5f, 0.5f, 0.5f, 1f, 1f, 1f, 0f, 0f,
        -0.5f, 0.5f, 0.5f, 0f, 1f, 1f, 0f, 0f,
        // Back face
        -0.5f, -0.5f, -0.5f,  0f, 0f, 1f, 0f, 0f,
        0.5f, -0.5f, -0.5f, 1f, 0f, 1f, 0f, 0f,
        0.5f, 0.5f, -0.5f, 1f, 1f, 1f, 0f, 0f,
        -0.5f, 0.5f, -0.5f, 0f, 1f, 1f, 0f, 0f,
        // Left face
        -0.5f, 0.5f, 0.5f, -1f, 1f, 1f, 0f, 0f,
        -0.5f, 0.5f, -0.5f,  0f, 1f, 1f, 0f, 0f,
        -0.5f, -0.5f, -0.5f, 0f, 0f, 1f, 0f, 0f,
        -0.5f, -0.5f, 0.5f,  1f, 0f, 1f, 0f, 0f,
        // Right face
        0.5f, 0.5f, 0.5f, 1f, 1f, 1f, 0f, 0f,
        0.5f, 0.5f, -0.5f, 0f, 1f, 1f, 0f, 0f,
        0.5f, -0.5f, -0.5f, 0f, 0f, 1f, 0f, 0f,
        0.5f, -0.5f, 0.5f, 1f, 0f, 1f, 0f, 0f,
        // Top face
        -0.5f, 0.5f, -0.5f, 0f, 1f, 1f, 0f, 0f,
        0.5f, 0.5f, -0.5f, 1f, 1f, 1f, 0f, 0f,
        0.5f, 0.5f, 0.5f, 1f, 0f, 1f, 0f, 0f,
        -0.5f, 0.5f, 0.5f, 0f, 0f, 1f, 0f, 0f,
        // Bottom face
        -0.5f, -0.5f, -0.5f, 0f, 1f, 1f, 0f, 0f,
        0.5f, -0.5f, -0.5f, 1f, 1f, 1f, 0f, 0f,
        0.5f, -0.5f, 0.5f, 1f, 0f, 1f, 0f, 0f,
        -0.5f, -0.5f, 0.5f, 0f, 0f, 1f, 0f, 0f
    ];
    // https://learnopengl.com/Advanced-OpenGL/Face-culling
    //     CCW Order:          CW Order:
    //     2                   2
    //    /\                  /\
    //   /  \                /  \
    //  /    \              /    \
    // 0------1            1------0
    private static readonly uint[] _indices =
    [
        // Front face
        0, 1, 2, 2, 3, 0,
        // Back face (fixed)
        4, 6, 5, 6, 4, 7,
        // Left face
        8, 9, 10, 10, 11, 8,
        // Right face (fixed)
        12, 14, 13, 14, 12, 15,
        // Top face (fixed)
        16, 18, 17, 18, 16, 19,
        // Bottom face
        20, 21, 22, 22, 23, 20
    ];
    public CubeMesh() : base(_vertices, _indices, DrawMode.Triangles) { }


}