Shader "Unlit/lightshader"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white"{}
		_Diffuse("DiffuseColor", Color) = (1,1,1,1)
		_otherset("otherset",Range(0,1)) = 1
    }
    SubShader
    {


		//第二个pass利用Blinn-Phong着色模型正常渲染
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			fixed4 _Diffuse;
			sampler2D _MainTex;
			//使用了TRANSFROM_TEX宏就需要定义XXX_ST
			float4 _MainTex_ST;
			float _otherset;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
				float2 uv : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul((float3x3)unity_ObjectToWorld, v.vertex);
				//通过TRANSFORM_TEX转化纹理坐标，主要处理了Offset和Tiling的改变,默认时等同于o.uv = v.texcoord.xy;
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * _otherset;
				fixed3 diffuse = _Diffuse;
				fixed4 color = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
				color.rgb = color.rgb * diffuse + ambient;
				return color;
			}
			#pragma vertex vert
			#pragma fragment frag	

			ENDCG
		}

		
    }

	FallBack "Diffuse"
}
