Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)
        _OutlineThickness ("Thickness", Range(0,10)) = 2
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off Lighting Off ZWrite Off

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 texcol = tex2D(_MainTex, uv);

                // 네 방향 샘플링해서 외곽선 생성
                float alpha = texcol.a;
                alpha += tex2D(_MainTex, uv + float2(_OutlineThickness/512,0)).a;
                alpha += tex2D(_MainTex, uv - float2(_OutlineThickness/512,0)).a;
                alpha += tex2D(_MainTex, uv + float2(0,_OutlineThickness/512)).a;
                alpha += tex2D(_MainTex, uv - float2(0,_OutlineThickness/512)).a;

                if (texcol.a > 0.01)
                    return texcol;
                else if (alpha > 0.01)
                    return _OutlineColor;
                else
                    return fixed4(0,0,0,0);
            }
            ENDCG
        }
    }
}
