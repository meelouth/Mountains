Shader "Custom/HeightDependentTint"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}

        _First("Highest Point",float) = 1
        _Second("Second Heighest",float) = 1
        _Third("Third Heighest",float) = 1
        _Fourth("Fourth Heighest",float) = 1

        _FirstC("Highest Point Colour", Color) = (0,0,0,1)
        _SecondC("Second Heighest Colour",Color) = (0,0,0,1)
        _ThirdC("Third Colour",Color) = (0,0,0,1)
        _FourthC("Fourth Colour",Color) = (0,0,0,1)
        _FifthC("Fifth Colour",Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float _Change;
        float _Second;
        float _Third;
        float _Fourth;
        float _Fifth;
        float4 _FirstC;
        float4 _SecondC;
        float4 _ThirdC;
        float4 _FourthC;
        float4 _FifthC;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 tintColor;

            if (IN.worldPos.y >= _Change)
            {
                tintColor = lerp(_FirstC.rgba, _SecondC.rgba, _Change / IN.worldPos.y);
            }

            else if (IN.worldPos.y >= _Second)
            {
                tintColor = lerp(_SecondC.rgba, _ThirdC.rgba, _Second / IN.worldPos.y);
            }

            else if (IN.worldPos.y >= _Third)
            {
                tintColor = lerp(_ThirdC.rgba, _FourthC.rgba, _Third / IN.worldPos.y);
            }

            else if (IN.worldPos.y >= _Fourth)
            {
                tintColor = lerp(_FourthC.rgba, _FifthC.rgba, _FourthC / IN.worldPos.y);
            }

            else if (IN.worldPos.y <= _Third && IN.worldPos.y > _Fourth)
            {
                tintColor = _FifthC;
            }


            o.Albedo = c.rgb * tintColor.rgb;
            o.Alpha = c.a * tintColor.a;
        }
        ENDCG
    }
    Fallback "Diffuse"
}