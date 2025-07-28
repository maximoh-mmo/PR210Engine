using Engine.Core.DataTypes;
using Engine.Render;
using OpenTK.Mathematics;

namespace Engine.Material
{
    public class Material
    {

        private readonly Shader _shader;
        private readonly Dictionary<string, (object Value, int TextureUnit)> _properties = [];
        private readonly ShaderDescriptor _descriptor;

        public Material(ShaderDescriptor descriptor)
        {
            _descriptor = descriptor;
            _shader = new Shader(descriptor.VertexShaderPath, descriptor.FragmentShaderPath);

            foreach (var property in descriptor.Properties)
            {
                _properties.Add(property.Key, (property.Value.DefaultValue, -1));
            }
        }

        public void SetProperty(string name, object value)
        {

            if (!_properties.TryGetValue(name, out var tuple))
            {
                throw new Exception($"Property {name} not found");
            }
            _properties[name] = (value, tuple.TextureUnit);
        }

        public void SetProperty(string name, Texture texture)
        {
            if (!_properties.TryGetValue(name, out var tuple))
            {
                throw new Exception($"Property {name} not found");
            }
            _properties[name] = (texture, tuple.TextureUnit);
        }


        public void Apply(Matrix4 model, Matrix4 view, float aspectRatio)
        {
            _shader.Use();
            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", view);
            _shader.SetMatrix4("projection",
                Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(80), 800f / 600f, 0.1f, 100f));

            _shader.SetVector3("viewPos", new Vector3(0, 0, 3));
            _shader.SetVector3("lightPos", new Vector3(2, 2, 2));

            foreach (var property in _properties.Keys.ToList())
            {
                SetShaderProperty(property, _properties[property]);
            }
        }

        public void SetShaderProperty(string name, (object Value, int TextureUnit) property)
        {
            var propertyType = _descriptor.Properties[name].Type;

            switch (propertyType)
            {
                case ShaderPropertyType.Texture2D:
                    var unit = _descriptor.TextureUnits[name];
                    ((Texture)property.Value).Bind(unit);
                    break;
                case ShaderPropertyType.Int:
                    _shader.SetFloat(name, (int)property.Value);
                    break;
                case ShaderPropertyType.Float:
                    _shader.SetFloat(name, (float)property.Value);
                    break;
                case ShaderPropertyType.Vector2:
                    _shader.SetVector2(name, (Vector2)property.Value);
                    break;
                case ShaderPropertyType.Vector3:
                    _shader.SetVector3(name, (Vector3)property.Value);
                    break;
                case ShaderPropertyType.Vector4:
                    _shader.SetVector4(name, (Vector4)property.Value);
                    break;
                case ShaderPropertyType.Matrix4:
                    _shader.SetMatrix4(name, (Matrix4)property.Value);
                    break;
                default:
                    throw new Exception("Unsupported property type");

            }
        }
    }
}


