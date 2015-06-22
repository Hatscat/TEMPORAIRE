Shader "Custom/PixelEffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_pixelSize ("pixel size", Float) = 10.0
	}
	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _pixelSize;

			fixed4 frag (v2f_img i) : SV_Target
			{
				return fixed4( tex2D(_MainTex, floor(i.uv * _ScreenParams.xy / _pixelSize) * _pixelSize / _ScreenParams.xy).rgb, 1.0);
			}
			ENDCG
		}
	}
	Fallback off
}