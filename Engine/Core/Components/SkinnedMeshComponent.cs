using Engine.Materials;

namespace Engine.Core.Components
{
    public class SkinnedMeshComponent : IComponent
    {
        public IMesh? Mesh;
        public Material? Material;
    }
}
