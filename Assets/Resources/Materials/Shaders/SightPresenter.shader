Shader "Overhaul/SightPresenter"
{
	Properties
	{
		_Color("Interior Color", Color) = (0,0,0,1)
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	ENDCG
	SubShader
	{
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Geometry"
			"IgnoreProjector" = "True"
		}
		Cull Back
		ZTest On
		Lighting Off
		Fog{ Mode Off }

		Pass
		{
			Blend DstAlpha OneMinusSrcAlpha
			CGPROGRAM
			
			float4 _Color;

			#pragma vertex vert
			#pragma fragment frag


			struct vertexInput {
				float4 vertex: POSITION;
				float2 texcoord : TEXCOORD0;
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
				return float4(0, 0, 0, 1);
			}
			ENDCG
		}
	}	
}