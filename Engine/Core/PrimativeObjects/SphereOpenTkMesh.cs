using Engine.Core.DataTypes;
namespace Engine.Core.PrimativeObjects;

public class SphereOpenTkMesh(int latitudeSegments = 16, int longitudeSegments = 32, float radius = 0.5f)
    : OpenTKMesh(CreateVertices(latitudeSegments, longitudeSegments, radius),
        CreateIndices(latitudeSegments, longitudeSegments),
        DrawMode.Triangles)
{
    private static float[] CreateVertices(int latSegments, int lonSegments, float radius)
    {
        var vertices = new List<float>();

        for (int y = 0; y <= latSegments; y++)
        {
            float v = (float)y / latSegments;
            float theta = v * MathF.PI;

            for (int x = 0; x <= lonSegments; x++)
            {
                float u = (float)x / lonSegments;
                float phi = u * MathF.PI * 2;

                float sinTheta = MathF.Sin(theta);
                float cosTheta = MathF.Cos(theta);
                float sinPhi = MathF.Sin(phi);
                float cosPhi = MathF.Cos(phi);

                float px = radius * sinTheta * cosPhi;
                float py = radius * cosTheta;
                float pz = radius * sinTheta * sinPhi;

                float nx = sinTheta * cosPhi;
                float ny = cosTheta;
                float nz = sinTheta * sinPhi;

                // You can also calculate tangents/bitangents here if needed
                // Store position, texCoord, normal (8 floats total)
                vertices.Add(px);
                vertices.Add(py);
                vertices.Add(pz);

                vertices.Add(u);    // Texture u
                vertices.Add(v);    // Texture v

                vertices.Add(nx);   // Normal x
                vertices.Add(ny);   // Normal y
                vertices.Add(nz);   // Normal z
            }
        }

        return [.. vertices];
    }

    private static uint[] CreateIndices(int latSegments, int lonSegments)
    {
        var indices = new List<uint>();

        for (int y = 0; y < latSegments; y++)
        {
            for (int x = 0; x < lonSegments; x++)
            {
                int i0 = y * (lonSegments + 1) + x;
                int i1 = i0 + lonSegments + 1;

                // Two triangles per quad
                indices.Add((uint)i0);
                indices.Add((uint)i1);
                indices.Add((uint)(i0 + 1));

                indices.Add((uint)(i0 + 1));
                indices.Add((uint)i1);
                indices.Add((uint)(i1 + 1));
            }
        }

        return [..indices];
    }
}
