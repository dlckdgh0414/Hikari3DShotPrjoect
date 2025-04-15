Shader "Custom/UI/ImprovedGlow"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        _GlowColor ("Glow Color", Color) = (1,1,1,1)
        _GlowPower ("Glow Power", Range(0.5, 10)) = 2
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 2
        [Toggle] _UseAdditiveBlend ("Use Additive Blend", Float) = 0
        
        // UI 셰이더 속성
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        
        _ColorMask ("Color Mask", Float) = 15
        
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }
    
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }
        
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        ColorMask [_ColorMask]
        
        // 첫 번째 패스: 글로우 효과
        Pass
        {
            // 토글에 따라 블렌드 모드 선택
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _GlowColor;
            float _GlowPower;
            float _GlowIntensity;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UseAdditiveBlend;
            
            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                
                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                
                OUT.color = v.color * _Color;
                return OUT;
            }
            
            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                
                // 원본 텍스처의 알파 값 저장
                half originalAlpha = color.a;
                
                // 개선된 다중 샘플링 블러 (더 부드러운 효과)
                const int samples = 16;
                float2 directions[16] = {
                    float2(1, 0), float2(0.9239, 0.3827), float2(0.7071, 0.7071),
                    float2(0.3827, 0.9239), float2(0, 1), float2(-0.3827, 0.9239),
                    float2(-0.7071, 0.7071), float2(-0.9239, 0.3827), float2(-1, 0),
                    float2(-0.9239, -0.3827), float2(-0.7071, -0.7071), float2(-0.3827, -0.9239),
                    float2(0, -1), float2(0.3827, -0.9239), float2(0.7071, -0.7071),
                    float2(0.9239, -0.3827)
                };
                
                const float blurSize = 0.01;
                half4 blurColor = half4(0, 0, 0, 0);
                
                // 샘플링하여 블러 효과 개선
                for(int i = 0; i < samples; i++)
                {
                    float2 offset = directions[i] * blurSize;
                    half4 sampledColor = tex2D(_MainTex, IN.texcoord + offset);
                    blurColor += sampledColor;
                }
                
                blurColor /= samples;
                
                // 글로우 효과 계산 (중요: 원본 이미지 알파와 분리)
                half glowAlpha = pow(blurColor.a, _GlowPower) * _GlowIntensity;
                
                // 글로우 색상 설정 (원본 이미지의 알파 영역에만 적용)
                half4 glowEffect = _GlowColor * glowAlpha * (1.0 - originalAlpha);
                
                // 최종 결과 (원본 + 글로우)
                half4 finalColor;
                if (_UseAdditiveBlend > 0.5) {
                    // 가산 블렌딩 모드 (겹칠 때 더 밝아짐)
                    finalColor = color;
                    finalColor.rgb += _GlowColor.rgb * glowAlpha;
                    finalColor.a = max(originalAlpha, glowAlpha * _GlowColor.a);
                } else {
                    // 일반 블렌딩 모드 (기본)
                    finalColor = color + glowEffect;
                    finalColor.a = max(originalAlpha, glowAlpha * _GlowColor.a);
                }
                
                #ifdef UNITY_UI_CLIP_RECT
                finalColor.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
                
                #ifdef UNITY_UI_ALPHACLIP
                clip(finalColor.a - 0.001);
                #endif
                
                return finalColor;
            }
            ENDCG
        }
    }
}