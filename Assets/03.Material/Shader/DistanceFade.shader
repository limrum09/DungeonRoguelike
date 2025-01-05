Shader "Custom/DistanceFade"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)  // 객체의 기본 색상
        _FadeStart("Fade Start Distance", Float) = 10  // 투명 시작 거리
        _FadeEnd("Fade End Distance", Float) = 30  // 완전 투명해지는 거리
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float distance : TEXCOORD0;
            };

            float _FadeStart;
            float _FadeEnd;
            fixed4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.distance = length(_WorldSpaceCameraPos - v.vertex.xyz);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 거리 기반 알파 계산
                float alpha = saturate((i.distance - _FadeStart) / (_FadeEnd - _FadeStart));
                return fixed4(_Color.rgb, alpha * _Color.a);
            }
            ENDCG
        }
    }
}
