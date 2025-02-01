Shader "Custom/AdvancedWaterShader"
{
    Properties
    {
        [MainTexture] _BaseMap("Water Texture", 2D) = "white" {}
        _BaseColor("Water Color", Color) = (0.2, 0.6, 1, 0.8)
        _Speed("Speed", Range(0,2)) = 0.5
        _FoamColor("Foam Color", Color) = (1,1,1,1)
        _WaveSpeed("Wave Speed", Range(0,5)) = 1.0
        _WaveAmplitude("Wave Amplitude", Range(0,1)) = 0.1
        _WaveFrequency("Wave Frequency", Range(0,5)) = 1.0
        _FoamThreshold("Foam Threshold", Range(0,1)) = 0.8
        _FoamSpeed("Foam Speed", Range(0,2)) = 0.5
        _Distortion("Distortion", Range(0,0.1)) = 0.02
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent+100"
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest LEqual
            Cull Off

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
                float3 positionWS : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float foam : TEXCOORD2;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _Speed;
                float4 _FoamColor;
                float4 _BaseMap_ST;
                float _WaveSpeed;
                float _WaveAmplitude;
                float _WaveFrequency;
                float _FoamThreshold;
                float _FoamSpeed;
                float _Distortion;
            CBUFFER_END

            float gradientNoise(float2 uv)
            {
                float2 i = floor(uv);
                float2 f = frac(uv);
                
                float a = dot(i, float2(127.1, 311.7));
                float b = dot(i + float2(1,0), float2(127.1, 311.7));
                float c = dot(i + float2(0,1), float2(127.1, 311.7));
                float d = dot(i + float2(1,1), float2(127.1, 311.7));
                
                float4 x = frac(sin(float4(a,b,c,d)) * 43758.5453123);
                float2 u = f * f * (3.0 - 2.0 * f);
                return lerp(lerp(x.x, x.y, u.x), lerp(x.z, x.w, u.x), u.y);
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                
                // Wave animation with distortion
                float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                float time = _Time.y * _WaveSpeed;
                
                float2 noiseUV = positionWS.xz * 0.5 + time;
                float distortion = gradientNoise(noiseUV) * _Distortion;
                
                float wave1 = sin((positionWS.x + distortion) * _WaveFrequency + time) * _WaveAmplitude;
                float wave2 = sin((positionWS.z + distortion) * _WaveFrequency * 0.8 + time * 1.2) * _WaveAmplitude;
                positionWS.y += (wave1 + wave2) * 0.5;

                OUT.foam = saturate(abs(wave1 - wave2) * 0.5 - _FoamThreshold);
                OUT.positionHCS = TransformWorldToHClip(positionWS);
                OUT.positionWS = positionWS;
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Texture sampling with moving UV
                float2 movingUV = IN.uv + float2(_Time.y * 0.1, _Time.y * 0.08) * _Speed;
                half4 baseTex = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, movingUV);
                
                // Foam calculation
                float2 foamUV = IN.positionWS.xz * 0.5 + _Time.y * _FoamSpeed;
                float foamNoise = gradientNoise(foamUV);
                float foam = saturate(IN.foam + foamNoise * 0.5);
                
                // Final color composition
                half4 color = baseTex * _BaseColor;
                color = lerp(color, _FoamColor, saturate(foam * 0.5));
                color.a = _BaseColor.a * (1 - foam * 0.2);
                
                return color;
            }
            ENDHLSL
        }
    }
}