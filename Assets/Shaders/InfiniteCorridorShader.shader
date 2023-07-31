Shader "Custom/InfiniteCorridorShader" {
 Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Clouds ("Clouds", 2D) = "white" {}
    }
    SubShader
    {
        Pass {
            Tags { "LightMode" = "ForwardBase" }
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
 
                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };
                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                    float3 worldPos : TEXCOORD1;
                };
                sampler2D _MainTex;
                sampler2D _Clouds;
                float4 _MainTex_ST;
                float4 _Clouds_ST;
                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.normal = normalize(mul(v.normal, unity_WorldToObject).xyz);
                    return o;
                }
                fixed4 _LightColor0;
                fixed4 frag(v2f i) : SV_Target
                {
                    float dif = max(0.05, dot(i.normal, normalize(_WorldSpaceLightPos0.xyz)));
                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed4 result = fixed4(col.rgb * dif * _LightColor0.rgb, 1);
                    return result;
                }
            ENDCG
        }
    }
}

