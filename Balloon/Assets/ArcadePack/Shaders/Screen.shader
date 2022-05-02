Shader "ArcadePack/Screen"
{
    Properties
    {
        [HDR]_Color ("Color (HDR)", Color) = (1,1,1,1)
        _MainTex ("Emission (RGB)", 2D) = "white" {}
		_Smoothness("Smoothness", Range(0,1)) = 0.95

		_LineIntensity  ("Line Intensity", Range(0,1)) = 0.25
		_LineDensity ("Line Density", Range(0,1000)) = 100.0
		_LineScroll ("Line Scroll", Range(-100,100)) = 10.0
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
		half _Smoothness;
		half _LineIntensity;
		half _LineDensity;
		half _LineScroll;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color ;
			c *= sin(IN.uv_MainTex.y * _LineDensity + _Time.y * _LineScroll) * _LineIntensity * 0.5 + (1.0 - _LineIntensity * 0.5);
			o.Albedo = fixed4(0,0,0,1);
			o.Emission = c.rgb;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
