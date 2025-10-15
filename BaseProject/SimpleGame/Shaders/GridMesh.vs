#version 330

in vec3 a_Position;

out vec4 v_Color;

uniform float u_Time;

const float c_PI = 3.141592;

void flag()
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = ( a_Position.x + 0.5 ) * 2 * c_PI;
	float value1 = ( a_Position.x + 0.5 );
	float dx = 0;
	float dy = value1 * 0.2 * sin(value + u_Time * 10);
	
	newPosition.y *= 1 - value1;
	newPosition.xy += vec2(dx, dy);

	gl_Position = newPosition;

	float shading = (sin(value - u_Time * 10)+1)/2 + 0.2 ;

	vec3 rgb = vec3(-dy+0.3, -dy+0.3, -dy+0.3);
	v_Color = vec4(shading);
}

void wave()
{
	vec4 newPosition = vec4(a_Position, 1);
	gl_Position = newPosition;

	float d = distance(a_Position.xy, vec2(0,0));

	//float value = clamp(0.5 - d, 0, 1);
	//value = ceil(value);
	//value = (0.5-d)*100; // anti - alliasing

	// 동심원
	float value = sin(d * 30 * c_PI - (u_Time*10));
	// 서서히 사라지게
	float p = 1 - clamp(d*2, 0, 1);

	v_Color = vec4(value*p);

	// 거리가 한 찰 바뀔 때 마다 0 or 1
}

void main()
{
	//flag();
	wave();
}
