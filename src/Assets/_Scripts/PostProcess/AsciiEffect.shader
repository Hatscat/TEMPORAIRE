Shader "Custom/AsciiEffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_pixelSize ("pixel size", Float) = 8.0
	}
	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
				
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _pixelSize;

			float character (float n, fixed2 p)
			{
				p = floor(p * vec2(4.0, -4.0) + 2.5);

				if (clamp(p.x, 0.0, 4.0) == p.x && clamp(p.y, 0.0, 4.0) == p.y)
				{
					if (int(fmod(n / exp2(p.x + 5.0 * p.y), 2.0)) == 1) return 1.0;
				}
				return 0.0;
			}

			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed3 col = tex2D(_MainTex, floor(i.uv * _ScreenParams.xy / _pixelSize) * _pixelSize / _ScreenParams.xy).rgb;

				fixed gray = (col.r + col.g + col.b) / 3.0;

				float n =  65536.0;             // .
				if (gray > 0.2) n = 65600.0;    // :
				if (gray > 0.3) n = 332772.0;   // *
				if (gray > 0.4) n = 15255086.0; // o
				if (gray > 0.5) n = 23385164.0; // &
				if (gray > 0.6) n = 15252014.0; // 8
				if (gray > 0.7) n = 13199452.0; // @
				if (gray > 0.8) n = 11512810.0; // #

				fixed2 p = fmod( i.uv * _ScreenParams.xy / ( _pixelSize * 0.5 ), 2.0) - fixed2(1.0);
				col = col * character(n, p);

				return fixed4(col.rgb, 1.0);
			}
			ENDCG
		}
	}
	Fallback off
}