Shader "FX/CRTShader"
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
				float4 scr_pos : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.scr_pos = ComputeScreenPos(o.vertex);
				return o;
			}
			
			sampler2D _MainTex;
			float _EffectTime;
			float _MaxTime;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);

				float2 ps = i.scr_pos.xy * _ScreenParams.xy / i.scr_pos.w;
				uint pp = (uint)ps.x % 3;
				float4 outcolor = float4(0, 0, 0, 1);
				if (pp == 1) outcolor.r = color.r;
				else if (pp == 2) outcolor.g = color.g;
				else outcolor.b = color.b;

				pp = (uint)ps.y % 5;
				if (pp == 0 || pp == 1 || pp == 2)
				{
					outcolor = float4(outcolor.g, outcolor.r, outcolor.b, 1.0);
				}

				return outcolor;
			}
			ENDCG
		}
	}
}
