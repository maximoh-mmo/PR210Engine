namespace Engine.Render
{
    public class PhongShaderDescriptor : ShaderDescriptor
    {
        public PhongShaderDescriptor() : base("Shaders/shader.vert", "Shaders/shader.frag")
        {
            AddProperty("material.diffuse", ShaderPropertyType.Texture2D, null);
            AddTextureUnits("material.diffuse");
        }
    }
}
