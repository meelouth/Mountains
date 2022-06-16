Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        _Border("Highest Point",float) = 0

        _Red("Highest Point Colour", Color) = (0,0,0,1)
        _Green("Second Heighest Colour",Color) = (0,0,0,1)
        _Blue("Third Colour",Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Border;
            float4 _Red;
            float4 _Green;
            float4 _Blue;

            v2f vert (appdata_full v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xyz;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color;
                if (i.uv.z > _Border)
                {
                    color = lerp(_Red, _Green, i.uv.z - _Border);
                }else
                {
                    color = fixed4(i.uv.x, i.uv.y, i.uv.z, 0);
                }
                
                // sample the texture
                return color;
            }
            ENDCG
        }
    }
}
