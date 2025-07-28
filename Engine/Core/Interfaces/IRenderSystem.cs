namespace Engine.Core.Interfaces
{
    public interface IRenderSystem
    { 
        void Render(List<GameObject> gameObjects, float aspectRatio);
    }
}
