#version 330 core
in vec2 texCoords;
in vec3 vertexColor;
out vec4 FragColor;

uniform sampler2D diffuse;

void main()
{
    FragColor = texture(diffuse, texCoords) * vec4(vertexColor, 1.0);
}