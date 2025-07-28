#version 330 core

in vec3 FragPos;        
in vec3 Normal;         
in vec2 texCoords;

struct Material {
    sampler2D diffuse;  // Diffuse texture
    sampler2D specular; // Specular map
    float shininess;    // Shininess factor
};

uniform Material material;
uniform vec3 lightPos; // Light Position
uniform vec3 viewPos; // Camera Position

out vec4 FragColour;

void main()
{
    vec3 texColor = texture(material.diffuse, texCoords).rgb;
    
    // Diffuse
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * texColor;

    // Specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specularMap = texture(material.specular, texCoords).rgb;
    vec3 specular = spec * specularMap;

    vec3 ambient = 0.1 * texColor;
    vec3 result = ambient + diffuse + specular;
    FragColour = vec4(result, 1.0);
}
