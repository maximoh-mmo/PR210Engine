#version 330 core

in vec3 vertexColour;

in vec2 texCoords;

uniform sampler2D ourTexture;

out vec4 FragColour;

void main()
{
	FragColour = texture(ourTexture, texCoords) * vec4(vertexColour, 1.0);
}