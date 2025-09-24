#version 330

in vec3 a_Position;
in float a_Value;
in vec4 a_Color;
in float a_Stime;
in vec3 a_Vel;
in float a_LifeTime; 
in float a_Mass;

out vec4 v_Color;

uniform vec4 u_Trans;
uniform float u_Time;
uniform vec3 u_Force;

const float c_PI = 3.141592;
const vec3 c_g = vec3(0, -9.8, 0);

void main()
{
	float lifeTime = a_LifeTime;
	float newAlpha = 1;
	float newTime = u_Time - a_Stime;
	vec4 newPosition = vec4(a_Position, a_Value);

	if (newTime > 0) {
		float t = fract(newTime/lifeTime)*lifeTime;
		float tt = t*t;

		float fX = c_g.x*a_Mass + u_Force.x;
		float fY = c_g.y*a_Mass + u_Force.y;
		float aX = fX / a_Mass;
		float aY = fY / a_Mass;
		// 합력(알짜힘) : 중력 + 외력 / 질량
		// vec3 totalAccel = c_g + (u_Force / a_Mass);
		
		float x = a_Vel.x * t + 0.5 * aX * tt * 10;
		float y = a_Vel.y * t + 0.5 * aY * tt;
		newPosition.xy += vec2(x, y);
		newAlpha = 1-t / lifeTime;
	}
	else {
		newPosition.xy = vec2(-100000, 0);
	}
	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, newAlpha);
}
