Shader "Overhaul/Interior"
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
		Tags
		{
			"IgnoreProjector" = "True"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
		Cull Off
		Lighting Off
		Fog{ Mode Off }

		Pass
		{
			Tags
			{
				"Queue" = "Transparent+2"
				"RenderType" = "Transparent+2"
			}
			ZWrite Off
			ZTest On
			Blend DstAlpha OneMinusSrcAlpha
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			float _PixelSize;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _OutlineColor;
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
				float4 downColor = tex2D(_MainTex, input.texcoord + float2(0, -_OutlineSize / (float)_TexHeight));
				float4 leftColor = tex2D(_MainTex, input.texcoord + float2(-_OutlineSize / (float)_TexWidth, 0));
				float4 rightColor = tex2D(_MainTex, input.texcoord + float2(_OutlineSize / (float)_TexWidth, 0));
				
				if (centerColor.a + upColor.a + downColor.a + leftColor.a + rightColor.a >= 1) {
					return float4(1, 0, 0, 1);
				}

				return float4(0, 0, 0, 0);
			}
			ENDCG
		}

			Pass
			{
				Tags
				{
					"Queue" = "Transparent+1"
					"RenderType" = "Transparent+1"
				}


				ZWrite On
				ZTest Off
				BlendOp Sub
				Blend DstAlpha OneMinusSrcAlpha
				CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			float _PixelSize;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _OutlineColor;
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

				if (centerColor.a > 0) {
					return float4(1,1,1,1);
				}

				return float4(0, 0, 0, 0);
			}
				ENDCG
		}
	}	
}