using Engine.Core.DataTypes;
using Engine.Render;

namespace Engine.Shaders.Descriptors
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