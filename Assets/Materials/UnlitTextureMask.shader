Shader "Unlit/ColorUnlitMask"
{
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _planePoint ("plane point", Vector) = (0,0,0,0)
      _planeNormal ("plane normal", Vector) = (0,1,0,0)
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      Cull Off
      CGPROGRAM
      #pragma surface surf NoLighting noambient
      struct Input {
          float2 uv_MainTex;
          float3 worldPos;
      };
      sampler2D _MainTex;
      float4 _planePoint;
      float4 _planeNormal;
      void surf (Input IN, inout SurfaceOutput o) {
         clip(dot(IN.worldPos - (float3)_planePoint,(float3)_planeNormal));
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
      }
      fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
            return fixed4(s.Albedo, s.Alpha);
        }
      ENDCG
    }
    Fallback "Diffuse"
}
