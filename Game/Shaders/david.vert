#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoords;
layout (location = 2) in vec3 aColour;
out vec3 vertexColor;
out vec2 texCoords;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
void main()
{
     vertexColor = aColour;
     texCoords = aTexCoords;
     gl_Position = projection * view * model * vec4(aPosition, 1.0);
}