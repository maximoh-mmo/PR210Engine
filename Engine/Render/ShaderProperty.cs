using Engine.Core.DataTypes;

namespace Engine.Render
{
    public class ShaderProperty(string name, ShaderPropertyType type, object defaultValue)
    {
        public string Name { get; private set; } = name;
        public ShaderPropertyType Type { get; private set; } = type;
        public object DefaultValue { get; private set; } = defaultValue;
    }
}
