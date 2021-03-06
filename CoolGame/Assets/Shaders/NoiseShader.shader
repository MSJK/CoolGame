﻿Shader "FX/NoiseShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_EffectTime ("Effect Time", Float) = 0
		_MaxTime ("Max Time", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _EffectTime;
			float _MaxTime;

			float hash(float x)
			{
				x *= 1234.5678;
				return frac(x * frac(x));
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float h = hash(i.uv.x + hash(i.uv.y) + frac(_EffectTime));
				float amt = sin(3.1415 * _EffectTime / _MaxTime);

				return col * (1 - amt) + h * amt;
			}
			ENDCG
		}
	}
}
