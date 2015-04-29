Shader "Hidden/STS_trigger_mask_pass" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

 SubShader { 

 Pass{
 CGPROGRAM
#include "UnityCG.cginc"
#pragma vertex vert_img
#pragma fragment frag	


uniform sampler2D _MainTex;
uniform sampler2D _ObjTex;


//Fragment Shader
float4 frag(v2f_img i) : COLOR
{
	float4 c = (0,0,0,0);
	float mT = tex2D(_MainTex, i.uv).g;
	float oT = tex2D(_ObjTex, i.uv).g;
	
    c.g = mT * (1 - oT);
	return c;
};
 ENDCG
}
} 
}