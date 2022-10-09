#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex   : POSITION;
    float4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
};

struct v2f
{
    float4 vertex   : SV_POSITION;
    fixed4 color    : COLOR;
    float2 texcoord  : TEXCOORD0;
};

fixed4 _Color;

v2f vert(appdata_t IN)
{
    v2f OUT;
    OUT.vertex = UnityObjectToClipPos(IN.vertex);
    OUT.texcoord = IN.texcoord;
    OUT.color = IN.color * _Color;
    #ifdef PIXELSNAP_ON
    OUT.vertex = UnityPixelSnap (OUT.vertex);
    #endif

    return OUT;
}

sampler2D _MainTex;
sampler2D _ShadowTex;
sampler2D _AlphaTex;
float _AlphaSplitEnabled;

fixed4 SampleSpriteTexture (float2 uv, sampler2D tex)
{
    fixed4 color = tex2D (tex, uv);
    return color;
}

fixed4 frag(v2f IN) : SV_Target
{
    fixed4 c = saturate(SampleSpriteTexture (IN.texcoord, _MainTex) * IN.color);
    c.rgb *= c.a;
    return c;
}