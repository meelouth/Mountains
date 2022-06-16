//CG code for Unity shader.

Shader "Custom/ContourColour"
{
    Properties{
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _HueShift("HueShift", Float) = 0
        _HueScale("HueScale", Float) = 1
        _MinColor("Max Color", Color) = (0,0,1,1)
        _MaxColor("Max Color", Color) = (1,0,0,1)

        _LineFillRatio("LineFillRatio", Range(-0.001, 1)) = 0.3
        _LineScale("LineScale", Float) = 1

        _Brightness("Brightness", Range(-0.001, 2)) = 0.7
        _Contrast("Contrast", Range(-0.001, 1.5)) = 1
    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _HueShift;
            float _HueScale;
            float _LineFillRatio;
            float _LineScale;
            float _Brightness;
            float _Contrast;
                float4 _MinColor;
                float4 _MaxColor;
            sampler2D _MainTex;

            half4 hsv_to_rgb(float3 HSV)
            {
                half4 RGB = HSV.z;

                        float var_h = HSV.x * 6;
                        float var_i = floor(var_h);   // Or ... var_i = floor( var_h )
                        float var_1 = HSV.z * (1.0 - HSV.y);
                        float var_2 = HSV.z * (1.0 - HSV.y * (var_h - var_i));
                        float var_3 = HSV.z * (1.0 - HSV.y * (1 - (var_h - var_i)));
                        if (var_i == 0) { RGB = half4(HSV.z, var_3, var_1,1); }
                        else if (var_i == 1) { RGB = half4(var_2, HSV.z, var_1,1); }
                        else if (var_i == 2) { RGB = half4(var_1, HSV.z, var_3,1); }
                        else if (var_i == 3) { RGB = half4(var_1, var_2, HSV.z,1); }
                        else if (var_i == 4) { RGB = half4(var_3, var_1, HSV.z,1); }
                        else { RGB = half4(HSV.z, var_1, var_2, 1); }

                return (RGB);
            }


            struct v2f {
                float4 vertex : SV_POSITION;
                float3 localPos : TEXCOORD0;
                half3 worldNormal : TEXCOORD1;
                fixed4 color : COLOR;
            };



            v2f vert(appdata_full v)
            {
                v2f o;

                o.localPos = v.vertex.xyz;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.color = v.color;
                
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                float l =  i.color.r;

                if (i.color.r <= 0)
                {
                    l = 1;
                }else
                {
                    l += 0.5f;
                }

                float power = 5;

                float x = power * l;

                //x = clamp(x, -2.78, 2.78);

                //max 
                //min = 1.5
                
                float zPos = (i.localPos.z * x);
                half4 col =  hsv_to_rgb(float3(((-0.57) * _HueScale + 100000 + _HueShift) % 1.0, 1, 1));
                half4 lines = round((i.localPos.z * _LineScale + 100000) % 1.0 - _LineFillRatio + 0.5) * half4(1, 1, 1, 1);
                half4 normals = ((i.worldNormal.y) * _Contrast + _Brightness)*half4(1,1,1,1);
                
                return lines * col * normals;
            }

            ENDCG
        }
    }
}