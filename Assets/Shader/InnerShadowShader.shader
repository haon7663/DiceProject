Shader "Custom/TMP_InnerShadow"
{
    Properties
    {
        _MainTex ("Font Atlas", 2D) = "white" {}
        _FaceColor ("Face Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0.0, 0.1)) = 0.0
        _ShadowColor ("Shadow Color", Color) = (0, 0, 0, 0.5)
        _ShadowOffset ("Shadow Offset", Vector) = (0.05, -0.05, 0, 0)
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Tags { "LightMode" = "ForwardBase" }
            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _FaceColor;
            float4 _OutlineColor;
            float _OutlineThickness;
            float4 _ShadowColor;
            float4 _ShadowOffset;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 텍스트 메인 색상
                fixed4 col = tex2D(_MainTex, i.uv) * _FaceColor;

                // 내부 그림자 계산
                float2 shadowUV = i.uv + _ShadowOffset.xy * _OutlineThickness;
                fixed4 shadow = tex2D(_MainTex, shadowUV) * _ShadowColor;

                // 메인 텍스트에 그림자 합성
                col.rgb = lerp(col.rgb, shadow.rgb, shadow.a);

                return col;
            }
            ENDCG
        }
    }
    FallBack "TextMeshPro/Mobile/Distance Field"
}