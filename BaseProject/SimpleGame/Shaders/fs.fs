#version 330

layout(location = 0) out vec4 FragColor;
layout(location = 1) out vec4 FragColor1;

uniform sampler2D u_RGBTexture;
uniform sampler2D u_NUMTexture;
uniform sampler2D u_TotalNumTexture;
uniform int u_Number;

in vec2 v_UV;

uniform float u_Time;

const float PI = 3.141592;

vec4 Test()
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
    return newColor;
}

vec4 Circles()
{
	vec2 newUV = v_UV;
	vec2 center = vec2(0.5, 0.5);
	float d = distance(newUV, center);
    vec4 newColor = vec4(0);
    
    float value = sin(d * 4 * PI * 4 - u_Time * 10);
    newColor = vec4(value);

	return newColor;
}

vec4 Flag()
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
    else
        discard;
    return newColor;
}

vec4 Q1() // *****
{
    float newX = v_UV.x;
    float newY = 1 - abs((v_UV.y * 2) - 1); // 0 ~ 1 ~ 0
    return texture(u_RGBTexture, vec2(newX, newY));
}

vec4 Q2() // ******
{
    // UV 좌표값은 좌상단 0,0 우하단 1,1
    float newX = fract(v_UV.x * 3); // 0~1 0~1 0~1
    float newY = (2 - floor(v_UV.x * 3)) / 3 + (v_UV.y / 3);// 2/3 ~ 3/3  offset 2 
                                                            // 1/3 ~ 2/3  offset 1
                                                            // 0/3 ~ 1/3  offset 0
    return texture(u_RGBTexture, vec2(newX, newY));
}

vec4 Q3()
{
    float newX = fract(v_UV.x * 3);
    float newY = fract(v_UV.x * 3) / 3 + v_UV.y / 3;
    return texture(u_RGBTexture, vec2(newX, newY));
}

vec4 Q4()
{
    float count = 6; // uniform 으로 뺄 수 있다
    float shift = 0.5 + u_Time / 10; // uniform 으로 뺄 수 있다
    float newX = fract(fract(v_UV.x * count) + (floor(v_UV.y * count) + 1) * shift);
    float newY = fract(v_UV.y * count);
    return texture(u_RGBTexture, vec2(newX, newY));
}

vec4 Q5()
{
    float count = 2; // uniform 으로 뺄 수 있다
    float shift = 0.5; // uniform 으로 뺄 수 있다
    float newX = fract(v_UV.x * count);
    float newY = fract(fract(v_UV.y * count) + (floor(v_UV.x * count) + 1) * shift);
    return texture(u_RGBTexture, vec2(newX, newY));
}

void Number()
{
    FragColor = texture(u_NUMTexture, v_UV);
}

void TotalNumber()
{
    // 각 셀 크기
    // 좌우로 1/5 위아래로 1/2
    vec2 cellSize = vec2(1.0 / 5.0, 1.0 / 2.0);

    // u_Number에 해당하는 row, col 계산
    // 0~4 → 윗줄, 5~9 → 아래줄
    // u_Num = 9
    // col = 4 // 의 몇 번째
    // row = 1 // 0 윗줄 1 아랫줄
    int col = u_Number % 5;
    int row = u_Number / 5;

    // UV offset 계산
    vec2 tileOrigin = vec2(col, row) * cellSize;

    // 화면 전체(v_UV)를 해당 셀의 텍스쳐 좌표로 매핑
    vec2 numberUV = tileOrigin + v_UV * cellSize;

    // 텍스쳐 샘플링
    FragColor = texture(u_TotalNumTexture, numberUV);
}

void main()
{
    //Test();
    //Circles();
    //Flag();
    //Q1();
    //Q2();
    //Q3();
    //Q4();
    //Q5();
    //TotalNumber();

    FragColor = Circles();
    FragColor1 = Circles();
}