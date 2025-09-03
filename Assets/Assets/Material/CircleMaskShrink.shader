Shader "UI/CircleMaskShrink"
{
    Properties
    {
        _Color("Tint", Color) = (0,0,0,0.8)       // 검은색 영역 색상
        _Center("Center", Vector) = (0.5, 0.5, 0, 0)  // 원 중심 (UV 좌표)
        _Radius("Radius", Float) = 1.0            // 현재 반지름 (1 → 0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; };

            fixed4 _Color;
            float4 _Center;
            float _Radius;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float dist = distance(i.uv, _Center.xy);

                // 반지름 바깥은 검은색, 반지름 안쪽은 투명
                if (dist > _Radius)
                    return _Color;     // 반지름 밖: 검은색
                else
                    return fixed4(0,0,0,0); // 반지름 안: 투명
            }
            ENDCG
        }
    }
}