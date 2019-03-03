// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sugiyama/Ghost/Z" {
  // @version 1.1.0
  // @date $date$
  // @author sugiyama-mitsunari
  
  Properties {
    _EdgeScale ("Edge Scale", Range (0.5, 4.0)) = 1
    _EdgePower ("Edge Power", Range (0.0, 50.0)) = 1
    _EdgeColor ("Edge Color", Color) = (0,0,0,1)
  }

  SubShader {

    Tags {
      "Queue" = "Transparent"
      "IgnoreProjector"="True"
      "RenderType"="Transparent"
    }
    
    // Outline pass
    Pass {
        ZWrite On
        ColorMask 0
    }

    // Outline pass
    Pass {
      Cull Back
      ZTest LEqual
      ZWrite On
      Blend SrcAlpha OneMinusSrcAlpha
  //    BlendOp Add

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
      
      uniform fixed _EdgeScale;
      uniform fixed _EdgePower;
      uniform fixed3 _EdgeColor;
      
      // Vertex
      struct VS {
        half4 pos: POSITION;
        half3 normal: TEXCOORD1;
        half3 eye: TEXCOORD2;
      };

      // Vertex shader
      VS vert (appdata_base i) {

        half3 world_pos = mul (unity_ObjectToWorld, i.vertex).xyz;
        half3 world_normal = mul (unity_ObjectToWorld, half4(i.normal, 0)).xyz;
        half3 camera_dir = normalize (WorldSpaceViewDir (i.vertex));

        VS o;
        o.pos = UnityObjectToClipPos (i.vertex);
        o.normal = normalize (world_normal);
        o.eye = camera_dir;
        return o;
      }
      // Fragment shader
      fixed4 frag (VS i): COLOR {

        fixed s = _EdgeScale;
        fixed n =  dot (i.normal, i.eye); // 0 -> 1
        fixed r = n + ((s - 1) / s);
        r = clamp (r, 0, 1);
        r = pow (r, _EdgePower);

        fixed a = 1 - r;

        return fixed4 (_EdgeColor.rgb, a);
      }

ENDCG
    }
  }

  FallBack "VertexLit"
}

