﻿#version 330 core
layout (location = 0) in vec3 aPosition;

layout (location = 1) in vec2 aTexCoords;

layout (location = 2) in vec3 aColour;

out vec3 vertexColour;

out vec2 texCoords;

void main()
{
	vertexColour = aColour;

	texCoords = aTexCoords;

	gl_Position = vec4(aPosition, 1.0);	
}