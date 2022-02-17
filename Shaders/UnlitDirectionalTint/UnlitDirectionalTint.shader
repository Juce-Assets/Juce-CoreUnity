Shader "Juce/Unlit/Directional Tint"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_ColorA("ColorA", Color) = (1,1,1,1)
		_ColorB("ColorB", Color) = (1,1,1,1)
		_ColorC("ColorC", Color) = (1,1,1,1)
		[HideInInspector] _texcoord("", 2D) = "white" {}
		[HideInInspector] __dirty("", Int) = 1
	}

		SubShader
		{
			Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
			Cull Back
			CGINCLUDE
			#include "UnityPBSLighting.cginc"
			#include "Lighting.cginc"
			#pragma target 3.0
			struct Input
			{
				float2 uv_texcoord;
				float3 worldNormal;
			};

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float4 _Color;
			uniform float4 _ColorA;
			uniform float4 _ColorB;
			uniform float4 _ColorC;

			inline half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
			{
				return half4 (0, 0, 0, s.Alpha);
			}

			void surf(Input i , inout SurfaceOutput o)
			{
				float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
				float3 ase_worldNormal = i.worldNormal;
				float3 ase_normWorldNormal = normalize(ase_worldNormal);
				float3 appendResult10 = (float3((ase_normWorldNormal.x * ase_normWorldNormal.x) , (ase_normWorldNormal.y * ase_normWorldNormal.y) , (ase_normWorldNormal.z * ase_normWorldNormal.z)));
				float4 temp_output_9_0 = (_Color * _ColorA);
				float3 layeredBlendVar13 = appendResult10;
				float4 layeredBlend13 = (lerp(lerp(lerp(temp_output_9_0 , (_Color * _ColorB) , layeredBlendVar13.x) , temp_output_9_0 , layeredBlendVar13.y) , (_Color * _ColorC) , layeredBlendVar13.z));
				o.Emission = (tex2D(_MainTex, uv_MainTex) * layeredBlend13).rgb;
				o.Alpha = 1;
			}

			ENDCG
			CGPROGRAM
			#pragma surface surf Unlit keepalpha fullforwardshadows 

			ENDCG
			Pass
			{
				Name "ShadowCaster"
				Tags{ "LightMode" = "ShadowCaster" }
				ZWrite On
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#pragma multi_compile_shadowcaster
				#pragma multi_compile UNITY_PASS_SHADOWCASTER
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
				#include "HLSLSupport.cginc"
				#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
					#define CAN_SKIP_VPOS
				#endif
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				struct v2f
				{
					V2F_SHADOW_CASTER;
					float2 customPack1 : TEXCOORD1;
					float3 worldPos : TEXCOORD2;
					float3 worldNormal : TEXCOORD3;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};
				v2f vert(appdata_full v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_OUTPUT(v2f, o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					Input customInputData;
					float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					half3 worldNormal = UnityObjectToWorldNormal(v.normal);
					o.worldNormal = worldNormal;
					o.customPack1.xy = customInputData.uv_texcoord;
					o.customPack1.xy = v.texcoord;
					o.worldPos = worldPos;
					TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
					return o;
				}
				half4 frag(v2f IN
				#if !defined( CAN_SKIP_VPOS )
				, UNITY_VPOS_TYPE vpos : VPOS
				#endif
				) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);
					Input surfIN;
					UNITY_INITIALIZE_OUTPUT(Input, surfIN);
					surfIN.uv_texcoord = IN.customPack1.xy;
					float3 worldPos = IN.worldPos;
					half3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
					surfIN.worldNormal = IN.worldNormal;
					SurfaceOutput o;
					UNITY_INITIALIZE_OUTPUT(SurfaceOutput, o)
					surf(surfIN, o);
					#if defined( CAN_SKIP_VPOS )
					float2 vpos = IN.pos;
					#endif
					SHADOW_CASTER_FRAGMENT(IN)
				}
				ENDCG
			}
		}
		Fallback "Diffuse"
}