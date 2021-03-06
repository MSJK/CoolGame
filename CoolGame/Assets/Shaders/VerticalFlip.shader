﻿Shader "FX/VerticalFlip"
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

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, float2(i.uv.x, 1 - i.uv.y));
				return color;
			}
			ENDCG
		}
	}
}
