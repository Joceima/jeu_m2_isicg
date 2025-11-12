Shader "Custom/PowerBlend"
{
    Properties
    {
        _ColorA("Color A", Color) = (0.2, 0.6, 1, 1)
        _ColorB("Color B", Color) = (1, 0, 0.6, 1)
        _BlendSpeed("Blend Speed", Range(0, 10)) = 2
        _BlendSharpness("Blend Sharpness", Range(0, 10)) = 3
        _NoiseScale("Noise Scale", Range(0.1, 10)) = 2
        _MainTex("Noise Texture", 2D) = "white" {}
        _Intensity("Emission Intensity", Range(0,5)) = 1.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _ColorA;
                float4 _ColorB;
                float _BlendSpeed;
                float _BlendSharpness;
                float _NoiseScale;
                float _Intensity;
                float4 _MainTex_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv * _NoiseScale;

                // Texture de bruit (tu peux utiliser une texture de Perlin ou cloud)
                half noise = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(_Time.y * 0.1, _Time.y * 0.05)).r;

                // Oscillation sinusoïdale pour faire pulser le mélange
                float t = sin(_Time.y * _BlendSpeed + noise * 6.2831);
                t = pow(saturate(t * 0.5 + 0.5), _BlendSharpness);

                half3 color = lerp(_ColorA.rgb, _ColorB.rgb, t);

                // Effet d’émission
                return half4(color * _Intensity, 1);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
