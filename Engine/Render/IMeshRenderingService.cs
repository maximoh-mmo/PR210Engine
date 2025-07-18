using Engine.Core;

namespace Engine.Render
{
    public interface IMeshRenderingService
    {
        public void Render(GameObject gameObject, float aspectRatio);
    }
}
