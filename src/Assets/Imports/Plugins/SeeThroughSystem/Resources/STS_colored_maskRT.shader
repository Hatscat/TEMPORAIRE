Shader "Hidden/STS_colored_mask_RT" {

	Properties 
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
		_STScolor ("Color", Color) = (1,1,1,1)
	}
	
	SubShader
	{
	    Tags { "RenderType"="Opaque" }
	    Cull Off
	    Lighting Off
		ZWrite On	
	    Pass 
	    {
	    Fog { Mode Off }
		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag
		
		fixed4 _STScolor;
		
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
			return _STScolor;
		}
		
		ENDCG
	    }
	}
	
	SubShader 
	{
	
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	
	Lighting Off
	Cull Off
	ZWrite Off	
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _alphaDiscard;
			fixed4 _STScolor;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				if (col.a < _alphaDiscard) discard;
				return _STScolor;
			}
		ENDCG
	}
} 

	
	SubShader 
	{
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}		

		ZWrite On
		Lighting Off
		Cull Off		

		Pass 
		{  
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"

				struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					half2 texcoord : TEXCOORD0;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed _Cutoff;
				fixed4 _STScolor;

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}
				
				fixed4 frag (v2f i) : COLOR
				{
					fixed4 col = tex2D(_MainTex, i.texcoord);
					if (col.a < _Cutoff) discard;
					return _STScolor;
				}
			ENDCG
		}
	}
}