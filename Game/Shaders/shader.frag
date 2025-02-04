#version 330 core

in vec3 vertexColour;
in vec2 texCoords;

struct Material{
	sampler2D diffuse;
	};

uniform Material material;
uniform sampler2D ourTexture;
out vec4 FragColour;

void main()
{
	FragColour = texture(material.diffuse, texCoords) * vec4(vertexColour, 1.0);
}