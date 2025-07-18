using Engine.Core.DebugGUI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Engine.Core.Services
{
    public class OpenTKImGuiService : IDebugGuiService
    {
        private readonly ImGuiController _controller;
        private readonly GameWindow _window;
        private bool _disposed;

        public OpenTKImGuiService(GameWindow window)
        {
            _window = window;
            _controller = new ImGuiController(window.ClientSize.X, window.ClientSize.Y);
        }

        public void Initialize(int width, int height) =>
            _controller.WindowResized(width, height);

        public void Update(float deltaTime) =>
            _controller.Update(_window, deltaTime);

        public void Render()
        {
            _controller.Render();
            ImGuiController.CheckGLError("End of frame");
        }

        public void OnResize(int width, int height) =>
            _controller.WindowResized(width, height);

        public void OnTextInput(char unicode) =>
            _controller.PressChar(unicode);

        public void OnMouseScroll(Vector2 offset) =>
            _controller.MouseScroll(offset);

        public void Dispose()
        {
            if (!_disposed)
            {
                _controller.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
