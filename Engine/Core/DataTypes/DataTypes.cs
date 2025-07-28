namespace Engine.Core.DataTypes
{
    public struct LayoutElement
    {
        public int Size;
        public Type Type;
        public int Offset;
    }
    public struct MeshData
    {
        public int VAO;
        public uint[] Indices;
        public float[] Vertices;
    }
    public enum GizmoType
    {
        Sphere,
        Cube,
        Line,
        Ray,
        WireframeCube,
        WireframeSphere,
    }
    public enum DrawMode
    {
        Points,
        Lines,
        LineStrip,
        LineLoop,
        Triangles,
        TriangleStrip,
        TriangleFan,
    }

    public enum ShaderPropertyType
    {
        Float,
        Int,
        Vector2,
        Vector3,
        Vector4,
        Matrix4,
        Texture2D
    }

    public enum VertexAttributeType
    {
        Position,
        Normal,
        TexCoord,
        Tangent,
        Bitangent,
        ColorRGB,
        ColorRGBA,
    }

    public struct AttributeLayout
    {
        public VertexAttributeType Type;
        public int Location;
    }
}
