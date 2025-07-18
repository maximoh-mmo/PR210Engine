﻿#version 330 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTexCoords;
layout(location = 2) in vec3 aColor;

out vec2 texCoords;
out vec3 vertexColor;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    texCoords = aTexCoords;
    vertexColor = aColor;
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}