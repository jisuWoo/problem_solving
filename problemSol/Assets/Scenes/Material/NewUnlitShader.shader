Shader "Unlit/NewUnlitShader"
{
     Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
        _LightDirection("LightDirection", Vector) = (1,-1,-1,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            float4 _DiffuseColor;
            float4 _LightDirection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = float4(1.0f,1.0f,0.0,1.0f);
                float3 normal = normalize(i.normal);
                float3 viewDir = normalize(i.vertex.xyz);
                float3 lightDir = normalize(_LightDirection.xyz);

                float ambientS = 0.2;
                float3 ambient = ambientS * _DiffuseColor.rgb;
                
                float diff = max(dot(normal, lightDir),0.0);
                float3 diffuse = diff * _DiffuseColor.rgb;

                float3 reflectDir = reflect(-lightDir,normal);
                float spec = pow(max(dot(viewDir,reflectDir),0.0),32.0);
                float3 specular = 0.5 * spec * float3(1.0, 1.0, 1.0);

                float3 result = (ambient + diffuse + specular);
                
                float threshold = 0.4;
				float3 banding = floor(result / threshold);
				float3 finalIntensity = banding * threshold;
				
				float4 col = float4(finalIntensity.x, finalIntensity.y, finalIntensity.z, 1.0);
                //float4 col = float4(result,1.0);

                return col;
            }
            ENDCG
        }
    }
}
