Shader "Custom/HauntedKeyV2"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.05, 0.8, 0.1, 1)
        _GlowColor ("Glow Color", Color) = (0.3, 1.0, 0.3, 1)
        _EmissionStrength ("Glow Strength", Range(0,10)) = 5
        _DistortionSpeed ("Distortion Speed", Range(0,10)) = 2
        _DistortionAmount ("Distortion Amount", Range(0,1)) = 0.5
        _PulseSpeed ("Pulse Speed", Range(0,10)) = 3
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend One One
        ZWrite Off
        Cull Back
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
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            fixed4 _BaseColor;
            fixed4 _GlowColor;
            float _EmissionStrength;
            float _DistortionSpeed;
            float _DistortionAmount;
            float _PulseSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y;

                // Organic distortion shimmer
                float distortion = sin(i.worldPos.x * 5 + time * _DistortionSpeed) *
                                   cos(i.worldPos.z * 5 + time * _DistortionSpeed * 1.3) *
                                   _DistortionAmount;

                // Stronger pulsation that fades in/out
                float pulse = 0.5 + 0.5 * sin(time * _PulseSpeed);
                pulse = pow(pulse, 2.0); // Sharper pulses

                // Combine colors
                float3 col = _BaseColor.rgb + (_GlowColor.rgb * distortion * 2.0);
                col += _GlowColor.rgb * pulse * _EmissionStrength;

                // Subtle rim effect for magical look
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float rim = 1.0 - saturate(dot(viewDir, normalize(i.worldNormal)));
                col += _GlowColor.rgb * rim * 0.6;

                return float4(col, 1.0);
            }
            ENDCG
        }
    }
}