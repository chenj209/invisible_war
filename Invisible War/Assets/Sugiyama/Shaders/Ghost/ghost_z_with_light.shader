// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sugiyama/Ghost/Z With Light" {
  // @version 1.1.0
  // @date $date$
  // @author sugiyama-mitsunari
  
  Properties {
    _EdgeScale ("Edge Scale", Range (0.5, 4.0)) = 1
    _EdgePower ("Edge Power", Range (0.0, 50.0)) = 1
    _EdgeColor ("Edge Color", Color) = (0,0,0,1)
    _LightRange ("Light Range (0 < v)", Float) = 1
    _LightPower ("Light Power (0 < v)", Int) = 1
  }

  SubShader {

    Tags {
      "Queue" = "Transparent"
      "IgnoreProjector"="True"
      "RenderType"="Transparent"
      "LightMode"="ForwardBase"
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

      uniform fixed _LightRange;
      uniform int _LightPower;


      // Vertex
      struct VS {
        half4 pos: POSITION;
        half3 normal: TEXCOORD1;
        half3 eye: TEXCOORD2;

        float light_pow: TEXCOORD3;
        float3 light_color: TEXCOORD4;
      };

      // Vertex shader
      VS vert (appdata_base i) {

        half3 world_pos = mul (unity_ObjectToWorld, i.vertex).xyz;
        half3 world_normal = mul (unity_ObjectToWorld, half4(i.normal, 0)).xyz;
        half3 camera_dir = normalize (WorldSpaceViewDir (i.vertex));

        int index = 0;
        float3 light_pos = float3(unity_4LightPosX0[index], unity_4LightPosY0[index], unity_4LightPosZ0[index]);

        fixed3 light_color = unity_LightColor0;
        fixed light_pow = distance (light_pos, world_pos) * _LightRange;

        VS o;
        o.pos = UnityObjectToClipPos (i.vertex);
        o.normal = normalize (world_normal);
        o.eye = camera_dir;
        o.light_pow = clamp (pow (light_pow, _LightPower), 0, 1);
        o.light_color = light_color;

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

//        fixed3 color = _EdgeColor.rgb * (i.light_pow) + i.light_color * (1 - i.light_pow);
        fixed3 color = _EdgeColor.rgb * (i.light_pow) + i.light_color * (1 - i.light_pow);

        return fixed4 (color, a);
      }

ENDCG
    }
  }

  FallBack "VertexLit"
}

