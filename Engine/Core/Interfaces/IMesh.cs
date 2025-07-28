using Engine.Core.DataTypes;

namespace Engine.Core.Interfaces
{
    public interface IMesh
    {
        MeshData MeshData { get; }
        DrawMode DrawMode { get; }
    }
}
