﻿#version 330 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec3 aColor;
layout(location = 2) in mat4 instanceModel; // Per-instance

out vec3 vertexColor;

uniform mat4 view;
uniform mat4 projection;

void main()
{
    vertexColor = aColor;
    gl_Position = projection * view * instanceModel * vec4(aPos, 1.0);
}