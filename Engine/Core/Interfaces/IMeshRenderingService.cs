using Engine.Core;

namespace Engine.Core.Interfaces
{
    public interface IMeshRenderingService
    {
        public void Render(GameObject gameObject, float aspectRatio);
    }
}
