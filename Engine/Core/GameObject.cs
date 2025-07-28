using Engine.Core.Interfaces;

namespace Engine.Core
{
    public class GameObject(string name = "Game Object")
    {
        public string Name { get; private set; } = name;

        private readonly HashSet<IComponent> _components = new();

        public T? GetComponent<T>() where T : IComponent => _components.OfType<T>().FirstOrDefault();


        public void AddComponent<T>(T component) where T : IComponent
        {
            _components.Add(component);
        }

        public void RemoveComponent<T>(IComponent component) where T : IComponent
        {
            _components.Remove(component);
        }
    }
}
