Shader "Hidden/STS_compose" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ColorMask ("Color mask", 2D) = "black" {}
	}

 SubShader {
  Pass{
 CGPROGRAM
#include "UnityCG.cginc"
#pragma vertex vert
#pragma fragment frag	

uniform sampler2D _MainTex;
uniform sampler2D _BackTex;
uniform sampler2D _MaskTex;
uniform sampler2D _ObstMaskTex;
uniform sampler2D _ColorMask;

half4 _MainTex_TexelSize;

fixed4 _TintColor;
float _BlurSpilling;

struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;		
		#if UNITY_UV_STARTS_AT_TOP	
		float2 uv1 : TEXCOORD1;				
		#else
		#endif
	};


v2f vert( appdata_img v )
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );	
	#if UNITY_UV_STARTS_AT_TOP	
	o.uv1 = o.uv;
	if (_MainTex_TexelSize.y < 0)
		o.uv1.y = 1-o.uv1.y;		
	#else
	#endif
	return o;
}
 

//Fragment Shader
 float4 frag (v2f i) : COLOR{
	float4 mT = tex2D(_MainTex, i.uv);
	#if UNITY_UV_STARTS_AT_TOP
	float4 bT = tex2D(_BackTex, i.uv1);	
	float ms = tex2D(_MaskTex, i.uv1).g;
	float oms = tex2D(_ObstMaskTex, i.uv1).r;
	float4 cms = tex2D(_ColorMask, i.uv1);
	#else
	float4 bT = tex2D(_BackTex, i.uv);	
	float ms = tex2D(_MaskTex, i.uv).g;
	float oms = tex2D(_ObstMaskTex, i.uv).r;
	float4 cms = tex2D(_ColorMask, i.uv);
	#endif

	bT = bT * _TintColor * 2 * ((1,1,1,1) - cms);
 
	float4 c = lerp(mT,bT,ms * saturate(_BlurSpilling + oms));	
	
	return c;
}
 ENDCG
}
} 
}