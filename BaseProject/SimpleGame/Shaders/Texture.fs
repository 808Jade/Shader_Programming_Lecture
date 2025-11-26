#version 330

layout(location=0) out vec4 FragColor;
layout(location=1) out vec4 FragColor1;

uniform sampler2D u_TexID;
uniform float u_Time; 

in vec2 v_Tex;

void Lense()
{
        //FragColor = vec4(v_Tex, 0, 1);
    //FragColor = texture(u_TexID, vec2(v_Tex.x, 1 - v_Tex.y));

    // 중심좌표 (0.5, 0.5)
    vec2 center = vec2(0.5, 0.5);

    // 중심에서 얼마나 떨어졌는지
    vec2 offset = v_Tex - center;

    // 거리 (0 = 중앙, 0.7 정도 = 모서리)
    float dist = length(offset);

    // ---- 볼록 렌즈 왜곡 ----
    // dist^power 로 곡선 제어 (power가 클수록 강한 렌즈)
    float power = 2.5;

    // 왜곡량(= 새로 계산된 비율)
    float distorted = pow(dist, power);

    // dist → distorted 로 이동시키는 비율 계산
    float scale = distorted / dist;
    scale = mix(1.0, scale, 0.6);  // 0.6 = 왜곡 강도

    // 볼록 렌즈 UV
    vec2 lensUV = center + offset * scale;

    // 텍스처 범위를 벗어나면 그냥 검정 처리
    if (lensUV.x < 0.0 || lensUV.x > 1.0 ||
        lensUV.y < 0.0 || lensUV.y > 1.0)
    {
        FragColor = vec4(0,0,0,1);
        FragColor1 = vec4(0,0,0,1);
        return;
    }

    // 샘플링
    vec3 col = texture(u_TexID, lensUV).rgb;

    // 출력
    FragColor = vec4(col, 1.0);
    FragColor1 = vec4(col, 1.0);
}

void Heat()
{
    vec2 uv = v_Tex;

    // 시간 없이도 UV 패턴으로만 흐르는듯한 distortion 생성
    float waveX = sin(uv.y * 40.0 + sin(uv.x * 10.0) * 3.0) * 0.012;
    float waveY = sin(uv.x * 50.0 + cos(uv.y * 8.0)  * 4.0) * 0.012;

    uv += vec2(waveX, waveY);

    // UV 범위 보정
    uv = clamp(uv, 0.0, 1.0);

    vec3 col = texture(u_TexID, uv).rgb;

    FragColor  = vec4(col, 1.0);
    FragColor1 = vec4(col, 1.0);
}

void Mosaic()
{
    vec2 uv = v_Tex;

    // 1) UV를 블록 단위로 정수화
    uv = floor(uv / 0.02) * 0.02 + 0.02 * 0.5;

    // 2) 텍스처 샘플
    vec3 col = texture(u_TexID, uv).rgb;

    // 3) 출력
    FragColor  = vec4(col, 1.0);
    FragColor1 = vec4(col, 1.0);  // 같은 결과를 두 번째 출력에도
}

// 빗방울 수
const int DROP_COUNT = 10;

// 각 빗방울 초기 위치(X,Y)와 반지름(Radius)
vec3 drops[DROP_COUNT] = vec3[](
    vec3(0.2, 0.7, 0.05),
    vec3(0.5, 0.5, 0.08),
    vec3(0.8, 0.3, 0.06),
    vec3(0.3, 0.2, 0.04),
    vec3(0.7, 0.8, 0.07),
    vec3(0.1, 0.4, 0.05),
    vec3(0.9, 0.6, 0.06),
    vec3(0.4, 0.9, 0.05),
    vec3(0.6, 0.1, 0.03),
    vec3(0.2, 0.5, 0.04)
);

void RainDrop()
{    
    vec2 uv = v_Tex;
    vec3 color = texture(u_TexID, uv).rgb;

    // 각 빗방울 처리
    for(int i=0;i<DROP_COUNT;i++){
        vec2 center = drops[i].xy;

        // 시간 기반 떨어짐
        float speed = 0.2 + drops[i].z * 2.0; // radius 크기에 따라 속도 달라짐
        center.y += mod(0.1 * u_Time * speed + center.y, 1.0); // 반복해서 떨어짐

        float radius = drops[i].z;
        vec2 diff = uv - center;
        float dist = length(diff);

        if(dist < radius){
            // 중앙일수록 굴절 강하게
            float effect = (radius - dist) / radius; 
            vec2 offset = diff * effect * 5.5;      // 굴절 강도
            color = texture(u_TexID, uv - offset).rgb;
        }
    }

    FragColor  = vec4(color, 1.0);
    FragColor1 = vec4(color, 1.0);
}



void main()
{
    RainDrop();
}