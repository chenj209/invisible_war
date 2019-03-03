Shader "Sugiyama/Ghost/Texture" {
  // @version 1.1.0
  // @date $date$
  // @author sugiyama-mitsunari

  Properties {
    _EdgeScale ("Edge Scale", Range (0.5, 4.0)) = 1
    _EdgePower ("Edge Power", Range (0.0, 50.0)) = 1
    _ColorBase ("Diffuse", 2D) = "gray" {}
  }

  SubShader {

    Tags {
      "Queue" = "Transparent"
      "IgnoreProjector"="True"
      "RenderType"="Transparent"
    }

    Pass {
      ZWrite On
      ColorMask 0
    }

    // Outline pass
    Pass {
      Cull Back
      ZTest LEqual
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

#include "_ghost_texture.cginc"

ENDCG
    }
  }

  FallBack "VertexLit"
}
