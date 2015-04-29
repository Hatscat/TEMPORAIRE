Shader "Hidden/STS_mask_check" 
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
		
		float4 frag(v2f_img i) : COLOR
		{
			float4 c = (0,0,0,0);						
			
			for (half x = -0.056; x <= 0.056; x = x + 0.016)
			{
				for (half y = -0.056; y <= 0.056; y = y + 0.016)
				{
					c = saturate(c + tex2D(_MainTex,i.uv+(x,y))*1000);
				}
			}
			
			return c;			
		}

		ENDCG
		} 
	}
}