Shader "Overhaul/BoardShader"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_PatternSampler("Pattern Sampler", 2D) = "white" {}
		_PatternsCount("Patterns Count", Int) = 4
		_PatternSize("Pattern Size", Int) = 4
		_BlacknessLevel("Blackness Level", Float) = 0.0588235294117647
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
			"RenderType" = "Opaque"
		}
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag


		float _CameraPositionX;
		float _CameraPositionY;
		int _PixelSize;
		sampler2D _MainTex;
		float4 _MainTex_ST;
		sampler2D _PatternSampler;
		int _PatternsCount;
		int _PatternSize;
		float _BlacknessLevel;

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
				float value = (inputColor.r + inputColor.g + inputColor.b)/3;

				float4 outputColor;			

				if (value <= _BlacknessLevel) {
					return float4(0, 0, 0, 1);
				}
				else if (value >= 1) {
					return float4(1, 1, 1, 1);
				}

				value = (value - _BlacknessLevel) / (1 - _BlacknessLevel);
				int patternPositionX = mod(value * _PatternsCount * _PatternsCount, _PatternsCount);
				int patternPositionY = div(value * _PatternsCount * _PatternsCount, _PatternsCount);

				int posX = floor(input.screenPosition.x / _PixelSize);
				int posY = floor(input.screenPosition.y / _PixelSize);

				outputColor = tex2D(_PatternSampler,
					float2((0.5 + _PatternSize * patternPositionX +
							mod(posX + _CameraPositionX, _PatternSize)) / (_PatternsCount * _PatternSize),
							(0.5 + _PatternSize * patternPositionY + 
							mod(posY + _CameraPositionY, _PatternSize)) / (_PatternsCount * _PatternSize)
						));				

				float3 hslColor = RGBtoHSL(inputColor.rgb);
				hslColor.z= 0.5;
				float4 normalizedColor = float4(HSLtoRGB(hslColor), 1);

				//return outputColor * normalizedColor;
				return inputColor;
			}
			ENDCG	
		}
	}	
}