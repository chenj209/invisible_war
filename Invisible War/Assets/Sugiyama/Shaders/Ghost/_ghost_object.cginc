// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

uniform fixed _EdgeScale;
uniform fixed _EdgePower;
uniform fixed4 _EdgeColor;


struct VS {
  half4 pos: POSITION;
  half3 normal: TEXCOORD1;
  half3 eye: TEXCOORD2;
};

VS vert (appdata_base i) {

  half4 world_pos = UnityObjectToClipPos (i.vertex);
  half3 world_normal = normalize (mul (unity_ObjectToWorld, half4(i.normal, 0)).xyz);
  half3 camera_dir = normalize (WorldSpaceViewDir (i.vertex));

  VS o;
  o.pos = world_pos;
  o.normal = world_normal;
  o.eye = camera_dir;

  return o;
}

fixed4 frag (VS i): COLOR {

  fixed s = _EdgeScale;
  fixed n =  dot ((i.normal), normalize (i.eye)); // 0 -> 1
  fixed r = n + ((s - 1) / s);
  r = clamp (r, 0, 1);
  r = pow (r, _EdgePower);

  fixed a = (1 - r) * 0.5f;

  fixed4 c = _EdgeColor; 
  c.a += a; 
 
  return c;
}
