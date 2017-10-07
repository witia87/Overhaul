Shader "Overhaul/OutlineShader"
{
	Properties
	{
		_MainTex("CameraInput", 2D) = "white" {}
		_BackgroundColor("Background Color", Color) = (0,0,1,1)
		_OutlineSize("Outline Size", Float) = 1
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
			"Queue" = "Geometry"
			"IgnoreProjector" = "True"
			"RenderType" = "Geometry"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
			"OutlineType" = "Outline"
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
			float4 _BackgroundColor;

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

			bool equalsBackground(float4 color) {
				return color.r == _BackgroundColor.r &&
					color.g == _BackgroundColor.g &&
					color.b == _BackgroundColor.b;
			}

			bool equals(float4 a, float4 b) {
				return a.r == b.r &&
					a.g == b.g &&
					a.b == b.b;
			}


			float4 frag(vertexOutput input) : COLOR
			{
				float4 centerColor = tex2D(_MainTex, input.texcoord);
				float4 upColor = tex2D(_MainTex, input.texcoord + float2(0, _OutlineSize / (float)_TexHeight));
				float4 downColor = tex2D(_MainTex, input.texcoord + float2(0, -_OutlineSize / (float)_TexHeight));
				float4 leftColor = tex2D(_MainTex, input.texcoord + float2(-_OutlineSize / (float)_TexWidth, 0));
				float4 rightColor = tex2D(_MainTex, input.texcoord + float2(_OutlineSize / (float)_TexWidth, 0));
				
				if (equalsBackground(centerColor) &&
					(!(equalsBackground(upColor) &&
						equalsBackground(downColor) &&
						equalsBackground(leftColor) &&
						equalsBackground(rightColor))))
				{
					return float4(1, 0, 0, 0.5);
				}

				return float4(0, 1, 1, 1);
				
			}
			ENDCG
		}
	}
}