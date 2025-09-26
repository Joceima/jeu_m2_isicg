Shader "Custom/test"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
        _Specular("Specular", Color) = (1,1,1,1)
        _Diffuse("Diffuse", Color) = (1,1,1,1)
        _Ambient("Ambient", Color) = (1,1,1,1)
        _Shininess("Shininess", Color ) = (0.5,0.5,0.5,0.5)

    }

    //bling phong classique

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {

                float4 positionOS : POSITION;
                float4 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 lightPosition : TEXCOORD2;
                float3 worldPos : TEXCOORD3;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
            
                OUT.positionHCS = mul(UNITY_MATRIX_MVP, IN.positionOS);
            
                float4 worldPos = mul(unity_ObjectToWorld, IN.positionOS);
                
                OUT.worldPos = worldPos.xyz;
            
                OUT.normalWS = normalize(mul((float3x3)unity_ObjectToWorld, IN.normal.xyz));
            
                OUT.uv = IN.uv;
            
                return OUT;
            }


            half4 frag(Varyings IN) : SV_Target
            {
                float3 lightPosition = float3(0, 5, 0);
                float3 normal = normalize(IN.normalWS);
                float3 lightDir = normalize(lightPosition - IN.positionHCS.xyz);
                float distance = length(lightPosition - IN.positionHCS.xyz);
                lightDir = normalize(lightDir);

                float lambertian = max(dot(lightDir, normal), 0.0);
                float specular = 0.0;

                if(lambertian > 0.0){
                    float3 viewDir = normalize(-IN.positionHCS.xyz);
                    float3 halfDir = normalize(lightDir + viewDir);
                    float specAngle = max(dot(halfDir, normal), 0.0);
                    specular = pow( specAngle, _Shininess);

                    }

                half4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _BaseColor;
                float3 lighting = _Ambient.rgb + _Diffuse.rgb * lambertian + _Specular.rgb * specular;
                half4 color = half4(baseColor.rgb * lighting, baseColor.a);


                return color;
            }
            ENDHLSL
        }
    }
}
