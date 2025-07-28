using Engine.Core.DataTypes;

namespace Engine.Render
{
    public class DiffuseDescriptor : ShaderDescriptor
    {
        public DiffuseDescriptor() : base("Shaders/shader.vert", "Shaders/shader.frag")
        {
            AddProperty("material.diffuse", ShaderPropertyType.Texture2D, null);
            AddTextureUnit("material.diffuse");
        }
    }
}