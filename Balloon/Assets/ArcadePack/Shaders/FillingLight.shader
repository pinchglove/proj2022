Shader "ArcadePack/FillingLight"
{
    Properties
    {
		_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
		[HDR]_Emission ("Emission", Color) = (1,1,1,1)
		_Position ("Position", Range(0,1)) = 0.5
		_Range ("Range", Range(0,1)) = 0.1
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

		fixed4 _Color;
		fixed4 _Emission;
		half _Smoothness;
		half _Position;
		half _Range;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			o.Emission = _Emission * (1 - smoothstep(_Position, _Position + _Range, IN.uv_MainTex.x));
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
