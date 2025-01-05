Shader "Unlit/TransparentTerrain2"
{
    Properties
    {
        _MainTex("Terrain Texture", 2D) = "white" {}
        _FadeDistance("Fade Distance", Float) = 50.0
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float _FadeDistance;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 pos : SV_POSITION;
                    float distance : TEXCOORD1;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;

                    // Calculate distance from the camera to the terrain vertex
                    o.distance = length(UnityObjectToWorldPos(v.vertex).xyz - _WorldSpaceCameraPos);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                // Apply fade based on distance
                if (i.distance < _FadeDistance)
                {
                    col.a = 0;  // Fully transparent if within fade distance
                }

                return col;
            }
            ENDCG
        }
        }
            FallBack "Diffuse"
}