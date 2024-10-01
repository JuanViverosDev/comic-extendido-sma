Shader "Custom/GrayscaleUI"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // Para manejar la transparencia correctamente
            ZWrite Off // No escribir en el z-buffer
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
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Mantiene la posición en el espacio del Canvas
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Obtener el color original de la textura
                fixed4 col = tex2D(_MainTex, i.uv);
                // Convertir a escala de grises
                float gray = dot(col.rgb, float3(0.3, 0.59, 0.11));
                return fixed4(gray, gray, gray, col.a); // Mantener el canal alfa para la transparencia
            }
            ENDCG
        }
    }
    FallBack "Transparent"
}