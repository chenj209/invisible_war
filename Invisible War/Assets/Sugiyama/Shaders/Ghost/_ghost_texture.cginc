// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

uniform fixed _EdgeScale;
uniform fixed _EdgePower;
uniform sampler2D _ColorBase;
uniform fixed4 _ColorBase_ST;

struct VS {
  half4 pos: POSITION;
  fixed2 uv: TEXCOORD0;
  half3 normal: TEXCOORD1;
  half3 eye: TEXCOORD2;
};

VS vert (appdata_base i) {

  half3 world_pos = mul (unity_ObjectToWorld, i.vertex).xyz;
  half3 world_normal = mul (unity_ObjectToWorld, half4(i.normal, 0)).xyz;
  half3 camera_dir = WorldSpaceViewDir (i.vertex);

  VS o;
  o.pos = UnityObjectToClipPos (i.vertex);
  o.uv = TRANSFORM_TEX(i.texcoord, _ColorBase);
  o.normal = normalize (world_normal);
  o.eye = camera_dir;
  return o;
}

fixed4 frag (VS i): COLOR {

  fixed n =  dot ( (i.normal), normalize (i.eye)); // 0 -> 1
  fixed r = n + ((_EdgeScale - 1) / _EdgeScale);
  r = clamp (r, 0, 1);
  r = pow (r, _EdgePower);

  fixed a = (1 - r) * 0.5;

  fixed4 c = tex2D (_ColorBase, i.uv); 
  c.a += a;  
    
  return c;
}

