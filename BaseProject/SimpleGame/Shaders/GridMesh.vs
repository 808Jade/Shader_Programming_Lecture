#version 330
#define MAX_POINTS 500

in vec3 a_Position;

out vec4 v_Color;

uniform float u_Time;
uniform vec4 u_Points[MAX_POINTS];
uniform int u_DropCount;

const float c_PI = 3.141592;
//const vec4 c_Points[MAX_POINTS] = vec4[](vec4(0,0,2,2),vec4(0.5,0,3,3),vec4(-0.5,0,4,4));

void flag()
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = ( a_Position.x + 0.5 ) * 2 * c_PI;
	float value1 = ( a_Position.x + 0.5 );
	float dx = 0;
	float dy = value1 * 0.3 * sin(value - u_Time * 10);
	
	newPosition.y *= 1 - value1; // 깃발처럼 x가 클 수록 y가 모이게..
	newPosition.xy += vec2(dx, dy);

	gl_Position = newPosition;

	float shading = (sin(value - u_Time * 10) + 1) / 2 + 0.2 ;

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

	v_Color = vec4(value * p);

	// 거리가 한 찰 바뀔 때 마다 0 or 1
}

void rainDrop()
{
    vec4 newPosition = vec4(a_Position, 1);
    gl_Position = newPosition;
    
	vec2 pos = a_Position.xy;
    float newColor = 0;
	
    for (int i = 0; i < u_DropCount; ++i)
    {
        vec2 cen = u_Points[i].xy;
        float sTime = u_Points[i].z;
        float lTime = u_Points[i].w;
		
        float newTime = u_Time - sTime;
        if (newTime > 0.0f)
        {
            float baseTime = fract(newTime / lTime);
            float oneMinus = 1 - baseTime;
            float t = baseTime * lTime;
            float range = baseTime * lTime / 10;

            float d = distance(pos, cen);
            float value = sin(d * 4 * c_PI * 10 - (t * 10));
            float p = 30 * clamp(range - d, 0, 1);

			newColor += value * p * oneMinus;
        }
    }
    v_Color = vec4(newColor);
}

void Q1() // 물결, 다이아몬드
{
	vec4 newPosition = vec4(a_Position, 1);

	float valueX = 2 * (newPosition.x + 0.5) * c_PI; // 하단에서는 0 상단에서는 2PI
	float valueY = 2 * (newPosition.y + 0.5) * c_PI; // 하단에서는 0 상단에서는 2PI

	float grayScale = sin(valueX * 4);
	grayScale += sin(valueY*4);			// sin 곡선 두 개가 겹치면..

	gl_Position = newPosition;

	v_Color = vec4(grayScale);
}

void main()
{
	//flag();
	//wave();
    //rainDrop();
	Q1();
}
