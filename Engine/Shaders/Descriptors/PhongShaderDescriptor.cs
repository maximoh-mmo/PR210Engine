using Engine.Core.DataTypes;
using Engine.Render;

namespace Engine.Shaders.Descriptors
{
    public class PhongShaderDescriptor : ShaderDescriptor
    {
        public PhongShaderDescriptor() : base("Shaders/phong.vert", "Shaders/phong.frag")
        {
            AddProperty("material.diffuse", ShaderPropertyType.Texture2D, null);
            AddTextureUnit("material.diffuse");
            AddProperty("material.specular", ShaderPropertyType.Texture2D,null);
            AddTextureUnit("material.specular");
            AddProperty("material.shininess", ShaderPropertyType.Float, 0f);
        }
    }
}
