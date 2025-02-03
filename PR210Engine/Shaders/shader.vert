﻿#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoords;
layout (location = 2) in vec3 aColour;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;


out vec3 vertexColour;
out vec2 texCoords;

void main()
{
	vertexColour = aColour;
	texCoords = aTexCoords;
	//model = glm::rotate(model, (float)glfwGetTime() * glm::radians(50.0f), glm::vec3(0.5f, 1.0f, 0.0f));  
	gl_Position = projection * view * model * vec4(aPosition, 1.0);
}