Shader "Unlit/TransparentTerrain"
{
    Properties
    {
        _MainTex("Terrain Texture", 2D) = "white" {}
        _Transparency("Transparency", Range(0,1)) = 0.5
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            Lighting Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                sampler2D _MainTex;
                float _Transparency;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 pos : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    col.a *= _Transparency;
                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
