Shader "Hidden/STS_saturate_color_mask" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}		
	}

	SubShader 
	{
		Pass
		{
		
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag		
		
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		float _ColorStrength;
		
		float4 frag(v2f_img i) : COLOR
		{
			float4 c = tex2D(_MainTex, i.uv);			
			float cmax = max(c.r,c.g);
			cmax = max(cmax,c.b);
			return c/cmax * _ColorStrength;
			
		}

		ENDCG
		} 
	}
}