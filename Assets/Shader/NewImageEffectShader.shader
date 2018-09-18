  Shader "Example/Diffuse Simple3" {
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float4 color : COLOR;
      };
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = .5;
      }
      ENDCG
    }
    Fallback "Diffuse"
  }