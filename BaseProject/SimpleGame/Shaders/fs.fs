#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;
in vec2 v_UV;

uniform float u_Time;

const float PI = 3.141592;

void Test()
{
	//// 파동 강도
    //float amplitude = 0.04;  // 높을수록 크게 흔들림
    //// 파동 주기
    //float frequency = 2.0;  // 높을수록 촘촘
    //// 속도
    //float speed = 4.0;       // 파동 진행 속도

    //// sin 파형으로 UV 변형
    //vec2 uv = v_UV;
    //uv.y += sin(uv.x * frequency * 2.0 * PI + u_Time * speed) * amplitude;

    //vec4 color = texture(u_RGBTexture, uv);
    //FragColor = color;

    vec2 newPos = v_UV;
    newPos.y += sin(0.5 * v_UV.x * PI * 2 + u_Time);
    vec4 newColor = texture(u_RGBTexture, newPos);
    FragColor = newColor;
}

void Circles()
{
	vec2 newUV = v_UV;
	vec2 center = vec2(0.5, 0.5);
	float d = distance(newUV, center);
    vec4 newColor = vec4(0);
    
    float value = sin(d * 4 * PI * 4 - u_Time * 10);
    newColor = vec4(value);

	FragColor = newColor;
}

void Flag()
{
    // 계산이 너무 복잡해져서 좌표계를 바꾼다
    vec2 newUV = vec2(v_UV.x, (1 - v_UV.y) - 0.5);
    float sinValue = v_UV.x * sin(v_UV.x * 2 * PI - u_Time * 5) * 0.2;
    vec4 newColor = vec4(0);
    float width = 0.2 * abs(sin(1 - v_UV.x) * 1 * PI);

    if (sinValue + width > newUV.y && sinValue - width < newUV.y)
    {
        newColor = vec4(1);
    }
    FragColor = newColor;
}

void main()
{
    //Test();
    //Circles();
    Flag();
}