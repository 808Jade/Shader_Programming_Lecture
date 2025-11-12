#version 330

layout(location = 0) out vec4 FragColor;

uniform sampler2D u_RGBTexture;
in vec2 v_UV;

uniform float u_Time;

const float PI = 3.141592;

void main()
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