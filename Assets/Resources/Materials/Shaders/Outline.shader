Shader "Overhaul/OutlineShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Base (RGB)", 2D) = "white" {}
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
			"Outline" = "True"
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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			int _OutlineSize;
			int _TexWidth;
			int _TexHeight;

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
				float4 centerColor = tex2D(_MainTex, input.texcoord);
				float4 upColor = tex2D(_MainTex, input.texcoord + float2(0, _OutlineSize / (float)_TexHeight));
				float4 downColor = tex2D(_MainTex, input.texcoord + float2(0, - _OutlineSize / (float)_TexHeight));
				float4 leftColor = tex2D(_MainTex, input.texcoord + float2(-_OutlineSize / (float)_TexWidth, 0));
				float4 rightColor = tex2D(_MainTex, input.texcoord + float2(_OutlineSize / (float)_TexWidth, 0));

				float value;
				if (centerColor.a > 0) {
					value = centerColor.a;
					return float4(0,0,0,0);
				}
				else {
					value = min(1.0, centerColor.a + upColor.a + downColor.a + leftColor.a + rightColor.a);
					return float4(value, 0, 0, value);
				}
			}
			ENDCG	
		}
	}	
}