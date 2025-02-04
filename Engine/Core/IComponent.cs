using System.Data;

namespace Engine.Core
{
    public interface IComponent
    {
        public void Update(float deltaTime);
    }
}
