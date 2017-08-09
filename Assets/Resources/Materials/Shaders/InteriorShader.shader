Shader "Overhaul/InteriorShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Base (RGB)", 2D) = "white" {}
		_Color("Interior Color", Color) = (0,0,0,1)
	}

	CGINCLUDE
	#include "UnityCG.cginc"
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


		float _PixelSize;
		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _Color;

			struct vertexInput {
				float4 vertex: POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct vertexOutput {
				float4 pos : POSITION0;
				float2 texcoord : TEXCOORD0;
				float2 screenPosition : TEXCOORD1;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				output.screenPosition = float2((1.0 + output.pos.x) / 2.0, (1.0 + output.pos.y) / 2.0) * _ScreenParams.xy;
				output.texcoord = TRANSFORM_TEX(input.texcoord, _MainTex);
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				float4 inputColor = tex2D(_MainTex, input.texcoord);
				_Color.a = inputColor.a;
				return _Color;
			}
			ENDCG	
		}
	}	
}