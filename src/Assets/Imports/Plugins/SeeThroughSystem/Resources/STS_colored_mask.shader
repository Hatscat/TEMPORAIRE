Shader "Hidden/STS_colored_mask" {
Properties {
	_STScolor ("Color", Color) = (1,1,1,1)
	}
SubShader {
    Tags { "RenderType"="Opaque" }
	ZWrite On	
    Pass {
        Fog { Mode Off }
CGPROGRAM

#pragma vertex vert
#pragma fragment frag

fixed4 _STScolor;

struct v2f {
    float4 pos : POSITION;    
};

float4 vert(float4 v:POSITION) : SV_POSITION {
                return mul (UNITY_MATRIX_MVP, v);
            }

half4 frag() : COLOR 
{ 
	return _STScolor;
}
ENDCG
    }
}
}