using OpenTK.Mathematics;

namespace Engine.Core.DebugGUI;

public interface IDebugGuiService : IDisposable
{
    void Initialize(int width, int height);
    void Update(float deltaTime);
    void Render();
    void OnResize(int width, int height);
    void OnTextInput(char unicode);
    void OnMouseScroll(Vector2 offset);
}