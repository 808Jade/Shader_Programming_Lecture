#version 330

layout(location=0) out vec4 FragColor;
layout(location=1) out vec4 FragColor1;

in vec4 v_Color;
in vec2 v_Tex;

uniform vec4 u_Color;
uniform sampler2D u_Texture;

void Textured()
{
	vec4 result = texture(u_Texture, v_Tex) * v_Color;
	float brightness = dot(result.rgb, vec3(0.2126, 0.7152, 0.0722));
	FragColor = clamp(result, 0.0, 1.0); // temp
	if(brightness > 1.0)
		FragColor1 = result - vec4(0);
	else
		FragColor1 = vec4(0);
}

void main()
{
//	FragColor = v_Color;
//	FragColor1 = vec4(v_Tex, 0, 1);
	Textured();
}