Shader "Sprites/ShadedSprite"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _ShadowTex ("Shadow Sprite Texture", 2D) = "black" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		
		[IntRange] _StencilRef ("Stencil Shadow Ref Value", Range(0,255)) = 1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

		Pass
		{
		    Name "LightSprite"
		    Tags { "LightMode" = "Universal2D" }    //Hack because URP doesnt really support multi-pass
		    
		    Stencil
            {
                Ref [_StencilRef]
                Comp NotEqual
            }
		    
		    HLSLPROGRAM 
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			
			#include "LightSpritePass.hlsl"
		    ENDHLSL
		}
		
		Pass
		{
		    Name "ShadowSprite"
		    Tags { "LightMode" = "SRPDefaultUnlit" }    //Hack because URP doesnt really support multi-pass
		    
		    Stencil
            {
                Ref [_StencilRef]
                Comp Equal
            }
		    
		    HLSLPROGRAM 
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			
			#include "ShadowSpritePass.hlsl"
		    ENDHLSL
		}
	}
}