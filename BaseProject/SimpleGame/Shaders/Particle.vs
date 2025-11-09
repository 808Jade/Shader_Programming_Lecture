#version 330

in vec3 a_Position;
in float a_Value;
in vec4 a_Color;
in float a_Stime;
in vec3 a_Vel;
in float a_LifeTime; 
in float a_Mass;
in float a_Period;

out vec4 v_Color;

uniform vec4 u_Trans;
uniform float u_Time;
uniform vec3 u_Force;

const float c_PI = 3.141592;
const vec3 c_g = vec3(0, -9.8, 0);

void myfountain()
{
	float lifeTime = a_LifeTime;
	float newAlpha = 1;
	float newTime = u_Time - a_Stime;
	vec4 newPosition = vec4(a_Position, 1);

	if (newTime > 0) {
		float t = fract(newTime/lifeTime) * lifeTime;
		float tt = t*t;
	
		// 합력(알짜힘) : 중력 + (외력 / 질량)
		vec3 totalAccel = c_g + (u_Force / a_Mass);
		float x = a_Vel.x * t + 0.5 * totalAccel.x * tt * 10;
		float y = a_Vel.y * t + 0.5 * totalAccel.y * tt;
		
		//        초기 속도 * (0~1 파티클의 진행률) + 0.5 * 알짜힘 * (가속도에 의한 위치 변화 계산 tt)
		//float x = a_Vel.x * t + 0.5 * aX * tt * 10;
		//float y = a_Vel.y * t + 0.5 * aY * tt;
		newPosition.xy += vec2(x, y);
		newAlpha = 1-t / lifeTime;
	}
	else {
		newPosition.xy = vec2(-100000, 0);
	}
	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, newAlpha);
}

void fountain()
{
	float lifeTime = a_LifeTime;
	float newAlpha = 1;
	float newTime = u_Time - a_Stime;
	vec4 newPosition = vec4(a_Position, 1);

	if (newTime > 0) {
		float t = fract(newTime/lifeTime) * lifeTime;
		float tt = t*t;

		float fX = c_g.x * a_Mass + u_Force.x; // 중력 * 질량 + 힘의 방향(외력)
		float fY = c_g.y * a_Mass + u_Force.y;
		float aX = fX / a_Mass;					// 을 질량으로 나눔 = 알짜힘
		float aY = fY / a_Mass;			
		// 합력(알짜힘) : 중력 + (외력 / 질량)
		//vec3 totalAccel = c_g + (u_Force / a_Mass);
		
		//        초기 속도 * (0~1 파티클의 진행률) + 0,5 * aX * (가속도에 의한 위치 변화 계산 tt)
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

void sinParticle()
{
	vec4 newPosition = vec4(a_Position, 1);
	float newAlpha = 1;

	float newTime = u_Time - a_Stime;
	float lifeTime = a_LifeTime;
	float amp = a_Value * 2 - 1;	// 진폭 -1 ~ 1
	float period = a_Period * 2;	// 주기 0 ~ 2

	vec4 newColor = a_Color;

	if(newTime > 0)
	{
		vec4 centerColor = vec4(1,1,1,1);
		vec4 borderColor = vec4(1,0,0,1);

		float t = fract(newTime / lifeTime) * lifeTime;
		float tt = t*t;
		float nTime = t / lifeTime;
		float x = nTime * 2 - 1; // -1 ~ 1
		float y = nTime * sin(nTime*c_PI) * amp * sin(period*(2*c_PI*(t/lifeTime)));

		newPosition.xy += vec2(x,y);
		newAlpha = 1-t / lifeTime; 
	
		float distance = abs(y);
		newColor = mix(centerColor, borderColor, distance * 3);
	}
	else
	{
		newPosition.xy = vec2(-1000000,0);
	}

	gl_Position = newPosition;
	v_Color = newColor; //vec4(a_Color.rgb, newAlpha);
}

void circleParticle()
{
	vec4 newPosition = vec4(a_Position, 1);
	float newAlpha = 1;

	//    2
	float lifeTime = a_LifeTime;
	//              0 ~        3
	float newTime = u_Time - a_Stime;

	if(newTime > 0)
	{
		float t = fract(newTime / lifeTime) * lifeTime; // 0 ~ lifeTime
		float tt = t*t;									// 가속도가 필요하면 그냥 이걸 곱해

		float value = a_Value * c_PI * 2;
		float x = sin(value);
		float y = cos(value);

		float newX = x + 0.5 * c_g.x * tt;
		float newY = y + 0.5 * c_g.y * tt;

		newPosition.xy += vec2(newX,newY);
		newAlpha = 1 - t / lifeTime;
	}
	else
	{
		newPosition.xy = vec2(-1000000,0);
	}

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, newAlpha);
}

void Q1() // sin 곡선
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = a_Value * c_PI * 2;
	float dx = 2 * (a_Value - 0.5);
	float dy = 0.5 * sin(value - u_Time);

	newPosition.xy += vec2(dx,dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void Q2() // 원
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = a_Value * c_PI * 2;
	float dx = sin(value);
	float dy = fract(u_Time) * cos(value);

	newPosition.xy += vec2(dx,dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void Q3() // 나선
{
	vec4 newPosition = vec4(a_Position, 1);

	float value = a_Value * c_PI * 2;
	float dx = a_Value * sin(value*4 + u_Time);
	float dy = a_Value * cos(value*4 + u_Time);

	newPosition.xy += vec2(dx,dy);

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb, 1);
}

void main()
{
	//fountain();
	//myfountain();
	//sinParticle();
	//circleParticle();
	Q3();
}
