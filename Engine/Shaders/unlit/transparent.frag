#version 330 core
in vec2 texCoords;
in vec4 vertexColor;
out vec4 FragColor;

uniform sampler2D diffuse;

void main()
{
    vec4 texColor = texture(diffuse, texCoords);
    FragColor = texColor * vertexColor;
    if(FragColor.a < 0.1)
        discard;
}