Shader "ArcadePack/RowLight"
{
    Properties
    {
		_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
		[HDR]_EmissionLow ("Emission Low", Color) = (1,1,1,1)
		[HDR]_EmissionHigh ("Emission High", Color) = (1,1,1,1)
		_Speed ("Speed (Loops in second)", Range(-100,100)) = 10.0
		_Gradient("Gradient", Range(0, 1)) = 0.0
		_Bias ("Bias", Range(0, 1)) = 0.5
		_Tile ("Tile", Range(0, 100)) = 1
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
		fixed4 _EmissionLow;
		fixed4 _EmissionHigh;
		half _Smoothness;
		half _Speed;
		half _Gradient;
		half _Bias;
		half _Tile;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed amount = sin((IN.uv_MainTex.x * _Tile + _Time.y * _Speed) * UNITY_PI * 2) * 0.5 + 0.5;
			amount = smoothstep(_Bias - _Gradient * 0.5, _Bias + _Gradient, amount);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			o.Emission = lerp(_EmissionLow, _EmissionHigh, amount);
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
