Shader "Hidden/STS_saturate_mask" 
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
		float _transparency;
		float _power;
		
		float4 frag(v2f_img i) : COLOR
		{
			
			return saturate(saturate(tex2D(_MainTex, i.uv) * _power) * _transparency);
		}

		ENDCG
		} 
	}
}