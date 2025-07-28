#version 330 core

in vec3 vertexColour;
in vec2 texCoords;

struct Material {
    sampler2D diffuse;
};
uniform Material material;
out vec4 FragColor;

void main()
{
    FragColor = texture(material.diffuse, texCoords) * vec4(vertexColor, 1.0);
}