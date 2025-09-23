#version 330

in vec3 a_Position;
in float a_Value;
in vec4 a_Color;
in float a_Stime;
in vec3 a_Vel;

out vec4 v_Color;

uniform vec4 u_Trans;

uniform float u_Time;

const float c_PI = 3.141592;
const vec2 c_g = vec2(0, -9.8);

void main()
{
	float newTime = u_Time - a_Stime;
	vec4 newPosition = vec4(a_Position, a_Value);

	if (newTime > 0) {
		float t = fract(newTime/2.0)*2.0;
		float tt = t*t;
		float x = 0;
		float y = 0.5 * c_g.y * tt;

		newPosition.xy += vec2(x, y);
	}
	else {
		newPosition.xy = vec2(-100000, 0);
	}
	v_Color = a_Color;
	gl_Position = newPosition;
}
