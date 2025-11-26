#version 330

layout(location=0) out vec4 FragColor;
layout(location=1) out vec4 FragColor1;

uniform sampler2D u_TexID;

in vec2 v_Tex;

void main()
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
