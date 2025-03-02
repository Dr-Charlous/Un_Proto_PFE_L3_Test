Shader "Unlit/RippleShader"
{
    Properties
    {
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _PrevRT;
            sampler2D _CurrentRT;
            float4 _CurrentRT_TexelSize;
            float _Speed;
            float _Min;
            float _Max;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 e = float3(_CurrentRT_TexelSize.xy,0);
                float2 uv = i.uv;
                

                float p10 = tex2D(_CurrentRT, uv - e.zy * _Speed).x;
                float p01 = tex2D(_CurrentRT, uv - e.xz * _Speed).x;
                float p21 = tex2D(_CurrentRT, uv + e.xz * _Speed).x;
                float p12 = tex2D(_CurrentRT, uv + e.zy * _Speed).x;

                float p11 = tex2D(_PrevRT, uv).x;

                //float p = smoothstep(_Min, _Max,;
                float d = (p10 + p01 + p21 + p12) / 2 - p11;
                d *= 0.99f;
                return d;
            }
            ENDCG
        }
    }
}
