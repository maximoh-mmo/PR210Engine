using Engine.Core.DataTypes;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Render
{
    public class ShaderDescriptor(string vertexShaderPath, string fragmentShaderPath)
    {
        public Dictionary<string, ShaderProperty> Properties { get; private set; } = [];
        public Dictionary<string, TextureUnit> TextureUnits { get; private set; } = [];
        public string VertexShaderPath { get; private set; } = vertexShaderPath;
        public string FragmentShaderPath { get; private set; } = fragmentShaderPath;

        private static int MaxTextureUnits => GetMaxTextureUnits();

        private static int GetMaxTextureUnits()
        {
            GL.GetInteger(GetPName.MaxTextureImageUnits, out int maxTextureUnits);
            return maxTextureUnits;
        }

        public void AddProperty(string name, ShaderPropertyType type, object value)
        {
            Properties[name] = new ShaderProperty(name, type, value);
        }

        public void AddTextureUnit(string name)
        {
            if (TextureUnits.Count >= MaxTextureUnits)
            {
                throw new Exception("Too many texture units");
            }
            TextureUnits.Add(name, TextureUnit.Texture0 + TextureUnits.Count);
        }
    }
}
