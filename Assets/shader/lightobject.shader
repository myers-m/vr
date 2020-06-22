Shader "Unlit/lightobject"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("OutlineColor", 2D) = "blue"{}
		_OutlineLength("OutlineLength", Range(0,10)) = 10
		_Light("Light",Range(0,1)) = 0.2
		_A("A",Range(0,1)) = 0
    }


    SubShader
    {
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 100

		//第一个pass，各顶点沿法线向外位移指定距离，只输出描边的纯颜色
		Pass
		{

		//剔除正面，只渲染背面，防止描边pass与正常渲染pass的模型交叉
		Cull Front
		//Zwrite off

		//透明混合
		Blend SrcAlpha OneMinusSrcAlpha
		//深度偏移操作，两个参数的数值越大，深度测试时该pass渲染的片元将获得比原先更大的深度值
		//即距离相机更远，更容易被正常渲染的pass覆盖，防止描边pass与正常渲染pass的模型交叉
		Offset 20,20
		//Zwrite Off

		CGPROGRAM
		#include "UnityCG.cginc"
		sampler2D _OutlineColor;
		float _OutlineLength;
		//使用了TRANSFROM_TEX宏就需要定义XXX_ST
		float4 _OutlineColor_ST;

		int _Light;
		float _A;
		float time = 0.3;

		struct v2f
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
		};

		v2f vert(appdata_base v)
		{
			v2f o;
			//在物体空间下，每个顶点沿法线位移，这种描边会造成近大远小的透视问题
			v.vertex.xyz += v.normal * _OutlineLength;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _OutlineColor);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			//这个Pass直接输出描边颜色
			fixed4 color;
			color = tex2D(_OutlineColor, UnityStereoScreenSpaceUVAdjust(i.uv, _OutlineColor_ST));
			color.a = _Light == 1 ? _A * color.a : 0;
			return color;
		}

		#pragma vertex vert
		#pragma fragment frag
		ENDCG
	}


	}

		FallBack "Diffuse"
}
