Shader "Hidden/STS_green_mask" {
	SubShader
	{
	    Tags { "RenderType"="Opaque" }
		ZWrite On	
	    Pass 
	    {
	    Fog { Mode Off }
		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag

		struct v2f 
		{
		    float4 pos : POSITION;    
		};

		float4 vert(float4 v:POSITION) : SV_POSITION 
		{
			return mul (UNITY_MATRIX_MVP, v);
		}

		half4 frag() : COLOR 
		{ 
			return fixed4(0.0,1.0,0.0,0.0);
		}
		
		ENDCG
	    }
	}
}