Shader "Unlit/CameraEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TransitionTexture("Transition Texture", 2D) = "white" {}
        _CutOff("Cut off", Range(0.0, 1.1)) = 0.5
        _CutOffColor("Cutoff Color", Color) =(0,0,0,1) 
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _TransitionTexture;

            float _CutOff;
            fixed4 _CutOffColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 transit = tex2D(_TransitionTexture, i.uv);
                if (transit.b < _CutOff) {
                    return _CutOffColor;
                }
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
