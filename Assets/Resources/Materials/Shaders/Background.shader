Shader "Overhaul/Background"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Color("Color", Color) = (0,0,1,1)
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	#include "./ShaderUtilities.cginc"
	ENDCG

	SubShader
	{
		LOD 200
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
		Cull Off
		Lighting Off
		ZWrite Off
		Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
			
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			int _Color;

			struct vertexInput {
				float4 vertex: POSITION;
			};

			struct vertexOutput {
				float4 pos : POSITION0;
				float2 screenPosition : TEXCOORD1;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				output.screenPosition = float2((1.0 + output.pos.x) / 2.0, (1.0 + output.pos.y) / 2.0) * _ScreenParams.xy;
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				return _Color;				
			}
			ENDCG
		}
	}
}