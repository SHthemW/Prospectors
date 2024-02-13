// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "VFX/DisNew"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin]_UseCustomData("UseCustomData", Range( 0 , 1)) = 0
		[Enum(Default,0,On,1,Off,2)]_ZWrite("ZWrite", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)]_ZTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)]_CullMode("CullMode", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)]_Src("Src", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)]_Dst("Dst", Float) = 10
		_RampDesaturation("RampDesaturation", Range( 0 , 1)) = 0
		_RampAngel("RampAngel", Float) = 0
		_RampTex("RampTex", 2D) = "white" {}
		_Main_U("Main_U", Float) = 0
		_Main_V("Main_V", Float) = 0
		_MainPower("MainPower", Float) = 1
		[KeywordEnum(Add,Blend)] _AddBlend("Add&Blend", Float) = 1
		_MainIntensity("MainIntensity", Float) = 1
		_MainAngel("MainAngel", Float) = 0
		[KeywordEnum(R,A)] _UseRA("UseR&A", Float) = 0
		_MainDesaturation("MainDesaturation", Range( 0 , 1)) = 0
		[HDR]_MainColor("MainColor", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		_NoiseIntensity("NoiseIntensity", Float) = 0
		[KeywordEnum(Multiply,Lerp)] _NoiseChange("NoiseChange", Float) = 0
		_Noise01_U("Noise01_U", Float) = 0
		_Noise01_V("Noise01_V", Float) = 0
		_Noise01Tex("Noise01Tex", 2D) = "white" {}
		_Noise02_U("Noise02_U", Float) = 0
		_Noise02_V("Noise02_V", Float) = 0
		_Noise02Tex("Noise02Tex", 2D) = "white" {}
		_MaskAngel("MaskAngel", Float) = 0
		_MaskTex("MaskTex", 2D) = "white" {}
		_Mask02Angel("Mask02Angel", Float) = 0
		_Mask02Tex("Mask02Tex", 2D) = "white" {}
		_SoftValue("SoftValue", Float) = 1
		_DissolveValue("DissolveValue", Float) = 1
		_Dissolve_U("Dissolve_U", Float) = 0
		_Dissolve_V("Dissolve_V", Float) = 0
		_Dissolve("Dissolve", 2D) = "white" {}
		[ASEEnd]_DF_FD("DF_FD", Float) = 0

		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
	}

	SubShader
	{
		LOD 0

		
		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
		
		Cull Back
		AlphaToMask Off
		HLSLINCLUDE
		#pragma target 2.0

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS

		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }
			
			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZWrite Off
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define _RECEIVE_SHADOWS_OFF 1
			#define ASE_SRP_VERSION 999999
			#define REQUIRE_DEPTH_TEXTURE 1

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#if ASE_SRP_VERSION <= 70108
			#define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
			#endif

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _NOISECHANGE_MULTIPLY _NOISECHANGE_LERP
			#pragma shader_feature_local _ADDBLEND_ADD _ADDBLEND_BLEND
			#pragma shader_feature_local _USERA_R _USERA_A


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef ASE_FOG
				float fogFactor : TEXCOORD2;
				#endif
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_color : COLOR;
				float4 ase_texcoord5 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _MainTex_ST;
			float4 _MainColor;
			float4 _RampTex_ST;
			float4 _Noise02Tex_ST;
			float4 _MaskTex_ST;
			float4 _Dissolve_ST;
			float4 _Noise01Tex_ST;
			float4 _Mask02Tex_ST;
			float _MaskAngel;
			float _ZTest;
			float _MainIntensity;
			float _Dissolve_U;
			float _Dissolve_V;
			float _SoftValue;
			float _DissolveValue;
			float _RampAngel;
			float _Mask02Angel;
			float _MainPower;
			float _MainAngel;
			float _RampDesaturation;
			float _NoiseIntensity;
			float _Noise02_V;
			float _Noise02_U;
			float _Noise01_V;
			float _Noise01_U;
			float _Main_V;
			float _Main_U;
			float _UseCustomData;
			float _ZWrite;
			float _Src;
			float _CullMode;
			float _Dst;
			float _MainDesaturation;
			float _DF_FD;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTex;
			sampler2D _Noise01Tex;
			sampler2D _Noise02Tex;
			sampler2D _MaskTex;
			sampler2D _Mask02Tex;
			sampler2D _Dissolve;
			sampler2D _RampTex;
			uniform float4 _CameraDepthTexture_TexelSize;


						
			VertexOutput VertexFunction ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord5 = screenPos;
				
				o.ase_texcoord3 = v.ase_texcoord1;
				o.ase_texcoord4.xy = v.ase_texcoord.xy;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord4.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				VertexPositionInputs vertexInput = (VertexPositionInputs)0;
				vertexInput.positionWS = positionWS;
				vertexInput.positionCS = positionCS;
				o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				#ifdef ASE_FOG
				o.fogFactor = ComputeFogFactor( positionCS.z );
				#endif
				o.clipPos = positionCS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif
				float4 texCoord106 = IN.ase_texcoord3;
				texCoord106.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult107 = (float2(texCoord106.x , texCoord106.y));
				float2 appendResult11 = (float2(_Main_U , _Main_V));
				float2 uv_MainTex = IN.ase_texcoord4.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 panner8 = ( 1.0 * _Time.y * appendResult11 + uv_MainTex);
				float2 appendResult17 = (float2(_Noise01_U , _Noise01_V));
				float2 uv_Noise01Tex = IN.ase_texcoord4.xy * _Noise01Tex_ST.xy + _Noise01Tex_ST.zw;
				float2 panner18 = ( 1.0 * _Time.y * appendResult17 + uv_Noise01Tex);
				float4 tex2DNode13 = tex2D( _Noise01Tex, panner18 );
				float2 appendResult19 = (float2(_Noise02_U , _Noise02_V));
				float2 uv_Noise02Tex = IN.ase_texcoord4.xy * _Noise02Tex_ST.xy + _Noise02Tex_ST.zw;
				float2 panner20 = ( 1.0 * _Time.y * appendResult19 + uv_Noise02Tex);
				float4 tex2DNode24 = tex2D( _Noise02Tex, panner20 );
				float4 lerpResult52 = lerp( tex2DNode13 , tex2DNode24 , 0.5);
				#if defined(_NOISECHANGE_MULTIPLY)
				float4 staticSwitch54 = ( tex2DNode13 * tex2DNode24 );
				#elif defined(_NOISECHANGE_LERP)
				float4 staticSwitch54 = lerpResult52;
				#else
				float4 staticSwitch54 = ( tex2DNode13 * tex2DNode24 );
				#endif
				float4 Noise27 = staticSwitch54;
				float2 temp_output_33_0 = (Noise27).rg;
				float cos77 = cos( ( ( _MainAngel / 360.0 ) * TWO_PI ) );
				float sin77 = sin( ( ( _MainAngel / 360.0 ) * TWO_PI ) );
				float2 rotator77 = mul( ( panner8 + ( temp_output_33_0 * _NoiseIntensity ) ) - float2( 0.5,0.5 ) , float2x2( cos77 , -sin77 , sin77 , cos77 )) + float2( 0.5,0.5 );
				float4 tex2DNode1 = tex2D( _MainTex, ( ( appendResult107 * _UseCustomData ) + rotator77 ) );
				float3 desaturateInitialColor70 = tex2DNode1.rgb;
				float desaturateDot70 = dot( desaturateInitialColor70, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar70 = lerp( desaturateInitialColor70, desaturateDot70.xxx, _MainDesaturation );
				float3 temp_cast_1 = (_MainPower).xxx;
				float2 uv_MaskTex = IN.ase_texcoord4.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				float cos49 = cos( ( ( _MaskAngel / 360.0 ) * TWO_PI ) );
				float sin49 = sin( ( ( _MaskAngel / 360.0 ) * TWO_PI ) );
				float2 rotator49 = mul( uv_MaskTex - float2( 0.5,0.5 ) , float2x2( cos49 , -sin49 , sin49 , cos49 )) + float2( 0.5,0.5 );
				float4 tex2DNode44 = tex2D( _MaskTex, rotator49 );
				float2 uv_Mask02Tex = IN.ase_texcoord4.xy * _Mask02Tex_ST.xy + _Mask02Tex_ST.zw;
				float cos93 = cos( ( ( _Mask02Angel / 360.0 ) * TWO_PI ) );
				float sin93 = sin( ( ( _Mask02Angel / 360.0 ) * TWO_PI ) );
				float2 rotator93 = mul( uv_Mask02Tex - float2( 0.5,0.5 ) , float2x2( cos93 , -sin93 , sin93 , cos93 )) + float2( 0.5,0.5 );
				float Mask67 = ( ( tex2DNode44.r * tex2DNode44.a ) * tex2D( _Mask02Tex, rotator93 ).r );
				#if defined(_ADDBLEND_ADD)
				float staticSwitch86 = Mask67;
				#elif defined(_ADDBLEND_BLEND)
				float staticSwitch86 = 1.0;
				#else
				float staticSwitch86 = 1.0;
				#endif
				float2 appendResult99 = (float2(_Dissolve_U , _Dissolve_V));
				float2 uv_Dissolve = IN.ase_texcoord4.xy * _Dissolve_ST.xy + _Dissolve_ST.zw;
				float2 panner100 = ( 1.0 * _Time.y * appendResult99 + uv_Dissolve);
				float4 texCoord64 = IN.ase_texcoord3;
				texCoord64.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float lerpResult59 = lerp( _SoftValue , -1.5 , ( _DissolveValue + texCoord64.z ));
				float Dissolve66 = saturate( ( ( tex2D( _Dissolve, panner100 ).r * _SoftValue ) - lerpResult59 ) );
				#if defined(_ADDBLEND_ADD)
				float staticSwitch84 = Dissolve66;
				#elif defined(_ADDBLEND_BLEND)
				float staticSwitch84 = 1.0;
				#else
				float staticSwitch84 = 1.0;
				#endif
				#if defined(_ADDBLEND_ADD)
				float staticSwitch120 = IN.ase_color.a;
				#elif defined(_ADDBLEND_BLEND)
				float staticSwitch120 = 1.0;
				#else
				float staticSwitch120 = 1.0;
				#endif
				float2 uv_RampTex = IN.ase_texcoord4.xy * _RampTex_ST.xy + _RampTex_ST.zw;
				float cos115 = cos( ( ( _RampAngel / 360.0 ) * TWO_PI ) );
				float sin115 = sin( ( ( _RampAngel / 360.0 ) * TWO_PI ) );
				float2 rotator115 = mul( uv_RampTex - float2( 0.5,0.5 ) , float2x2( cos115 , -sin115 , sin115 , cos115 )) + float2( 0.5,0.5 );
				float3 desaturateInitialColor119 = tex2D( _RampTex, rotator115 ).rgb;
				float desaturateDot119 = dot( desaturateInitialColor119, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar119 = lerp( desaturateInitialColor119, desaturateDot119.xxx, _RampDesaturation );
				#if defined(_USERA_R)
				float staticSwitch42 = tex2DNode1.r;
				#elif defined(_USERA_A)
				float staticSwitch42 = tex2DNode1.a;
				#else
				float staticSwitch42 = tex2DNode1.r;
				#endif
				#if defined(_ADDBLEND_ADD)
				float staticSwitch121 = staticSwitch42;
				#elif defined(_ADDBLEND_BLEND)
				float staticSwitch121 = 1.0;
				#else
				float staticSwitch121 = 1.0;
				#endif
				float4 screenPos = IN.ase_texcoord5;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth101 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth101 = abs( ( screenDepth101 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _DF_FD ) );
				float temp_output_103_0 = saturate( distanceDepth101 );
				
				float3 BakedAlbedo = 0;
				float3 BakedEmission = 0;
				float3 Color = ( float4( pow( desaturateVar70 , temp_cast_1 ) , 0.0 ) * _MainIntensity * _MainColor * IN.ase_color * staticSwitch86 * staticSwitch84 * staticSwitch120 * float4( desaturateVar119 , 0.0 ) * staticSwitch121 * temp_output_103_0 ).rgb;
				float Alpha = ( _MainColor.a * staticSwitch42 * IN.ase_color.a * Mask67 * Dissolve66 * temp_output_103_0 );
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_ON
					clip( Alpha - AlphaClipThreshold );
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#ifdef ASE_FOG
					Color = MixFog( Color, IN.fogFactor );
				#endif

				return half4( Color, Alpha );
			}

			ENDHLSL
		}

	
	}
	CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=18909
226.6667;72.66667;803.3333;436.3333;483.4797;271.645;2.574483;True;False
Node;AmplifyShaderEditor.CommentaryNode;28;-3327.844,665.7451;Inherit;False;1720.678;899.8635;Comment;17;27;54;53;52;25;24;13;18;20;21;14;17;19;22;16;15;23;Noise;0,0,0,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-3254.278,1421.064;Inherit;False;Property;_Noise02_V;Noise02_V;26;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-3258.143,929.1628;Inherit;False;Property;_Noise01_U;Noise01_U;22;0;Create;True;0;0;0;False;0;False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-3255.68,1014.497;Inherit;False;Property;_Noise01_V;Noise01_V;23;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-3256.74,1335.729;Inherit;False;Property;_Noise02_U;Noise02_U;25;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-3079.277,1375.064;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-3277.844,743.8629;Inherit;False;0;13;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-3276.442,1150.429;Inherit;False;0;24;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;17;-3080.679,968.4974;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;20;-2955.241,1152.829;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;18;-2956.644,746.2628;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-2379.6,1189.008;Inherit;False;Constant;_Float0;Float 0;21;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;24;-2707.772,1122.311;Inherit;True;Property;_Noise02Tex;Noise02Tex;27;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-2709.174,715.7451;Inherit;True;Property;_Noise01Tex;Noise01Tex;24;0;Create;True;0;0;0;False;0;False;-1;None;33146fc305c881d46a11f1bc8b4831e9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-2275.277,913.5233;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;52;-2286.588,1043.933;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;54;-2100.934,1017.414;Inherit;False;Property;_NoiseChange;NoiseChange;21;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;Multiply;Lerp;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;51;-3328,1664;Inherit;False;1393.094;959.0836;;16;44;49;48;50;46;47;45;88;89;90;91;92;93;94;95;122;Mask;0,0,0,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-3280,1888;Inherit;False;Property;_MaskAngel;MaskAngel;28;0;Create;True;0;0;0;False;0;False;0;180;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;97;-3499.129,2924.755;Inherit;False;Property;_Dissolve_U;Dissolve_U;34;0;Create;True;0;0;0;False;0;False;0;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-1889.036,1019.423;Inherit;False;Noise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;65;-3344.353,2682.253;Inherit;False;1544;798.0002;;12;55;56;57;58;59;62;61;60;64;63;99;100;Dissolve;0,0,0,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;96;-3496.667,3010.09;Inherit;False;Property;_Dissolve_V;Dissolve_V;35;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;47;-3104,2016;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;88;-3273.912,2298.414;Inherit;False;Property;_Mask02Angel;Mask02Angel;30;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;72;-3657.668,2756.453;Inherit;False;0;55;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;99;-3321.666,2964.09;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;29;-2923.841,246.0969;Inherit;False;27;Noise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-3241.24,305.2125;Inherit;False;Property;_Main_U;Main_U;10;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;46;-3104,1888;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-3238.777,390.547;Inherit;False;Property;_Main_V;Main_V;11;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-2387.738,272.3377;Inherit;False;Property;_MainAngel;MainAngel;15;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-2745.84,342.097;Inherit;False;Property;_NoiseIntensity;NoiseIntensity;20;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-2960,1888;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;50;-3088,1712;Inherit;False;0;44;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;33;-2760.304,246.8556;Inherit;False;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;11;-3063.777,344.547;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-3244.353,3157.253;Inherit;False;Property;_DissolveValue;DissolveValue;33;0;Create;True;0;0;0;False;0;False;1;1.38;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;89;-3097.912,2426.414;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-3260.941,119.9123;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;100;-3202.63,2767.854;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;64;-3296.353,3274.253;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;83;-2499.455,-234.5568;Inherit;False;366.8507;293.6398;Comment;2;78;79;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;90;-3097.912,2298.414;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-2896.353,3274.253;Inherit;False;Constant;_Float1;Float 1;24;0;Create;True;0;0;0;False;0;False;-1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-2928.353,3002.253;Inherit;False;Property;_SoftValue;SoftValue;32;0;Create;True;0;0;0;False;0;False;1;50;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;74;-2211.738,400.3377;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;55;-2944.353,2730.253;Inherit;True;Property;_Dissolve;Dissolve;36;0;Create;True;0;0;0;False;0;False;-1;None;2c2415712e4d848458d02c631ad4623c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;-2953.912,2298.414;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;49;-2800,1824;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;91;-3081.912,2122.414;Inherit;False;0;94;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;106;-1893.258,365.7697;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-2449.455,-75.91698;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;75;-2211.738,272.3377;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;63;-2992.353,3162.253;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;8;-2939.74,122.3123;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;78;-2284.604,-184.5568;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;107;-1643.258,379.7697;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;59;-2480.353,3114.253;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;-2067.738,272.3377;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;93;-2793.912,2234.414;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;44;-2538.572,1797.429;Inherit;True;Property;_MaskTex;MaskTex;29;0;Create;True;0;0;0;False;0;False;-1;None;d4d9747e7fde6164d8777b73199b2240;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-2464.353,2826.253;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;110;-1113.458,-265.3335;Inherit;False;Property;_RampAngel;RampAngel;8;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-1902.258,562.7697;Inherit;False;Property;_UseCustomData;UseCustomData;1;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;-1513.258,466.7697;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;58;-2192.353,2938.253;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;-2206.559,1824.237;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;111;-937.4578,-137.3334;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;77;-1913.738,125.3376;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;112;-937.4578,-265.3335;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;360;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;94;-2537.912,2202.414;Inherit;True;Property;_Mask02Tex;Mask02Tex;31;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;62;-1968.354,2954.253;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;104;-1437.213,111.0498;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-2148.34,2082.726;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;113;-921.4578,-441.3335;Inherit;False;0;116;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-793.4578,-265.3335;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;67;-1885.973,2086.445;Inherit;False;Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;87;-815.6035,1091.861;Inherit;False;555.3803;423.7075;Comment;5;69;85;68;84;86;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;102;98.49738,1034.411;Inherit;False;Property;_DF_FD;DF_FD;37;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;66;-1767.846,2941.318;Inherit;False;Dissolve;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-785.3822,193.3835;Inherit;False;Property;_MainDesaturation;MainDesaturation;17;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;115;-633.4578,-329.3335;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-1126.409,85.042;Inherit;True;Property;_MainTex;MainTex;19;0;Create;True;0;0;0;False;0;False;-1;None;cb6ab611e3b26ca4e9157e75eab0d1fd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;116;-377.4579,-361.3335;Inherit;True;Property;_RampTex;RampTex;9;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;70;-600.3822,78.3835;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;68;-703.1827,1141.861;Inherit;False;67;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-380.8163,208.2783;Inherit;False;Property;_MainPower;MainPower;12;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;43;-431.5842,322.2854;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;85;-707.3273,1399.568;Inherit;False;Constant;_Float2;Float 2;28;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;101;266.3292,1004.42;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;69;-765.6035,1301.461;Inherit;False;66;Dissolve;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;118;2.734619,-239.3776;Inherit;False;Property;_RampDesaturation;RampDesaturation;7;0;Create;True;0;0;0;False;0;False;0;0.281;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;42;-444.3753,512.7623;Inherit;False;Property;_UseRA;UseR&A;16;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;R;A;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;2;1139.594,77.62637;Inherit;False;218;483;Comment;5;3;5;4;6;117;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;103;526.6799,1001.383;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;119;182.7346,-359.3776;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PowerNode;34;-232.5029,84.399;Inherit;False;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;121;-154.8799,777.6135;Inherit;False;Property;_AddBlend;Add&Blend;14;0;Create;True;0;0;0;False;0;False;0;1;1;True;;KeywordEnum;2;Add;Blend;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;84;-472.4178,1357.515;Inherit;False;Property;_AddBlend;Add&Blend;14;0;Create;True;0;0;0;False;0;False;0;1;1;True;;KeywordEnum;2;Add;Blend;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-210.8163,214.2783;Inherit;False;Property;_MainIntensity;MainIntensity;13;0;Create;True;0;0;0;False;0;False;1;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;86;-474.2231,1148.384;Inherit;False;Property;_AddBlend;Add&Blend;13;0;Create;True;0;0;0;False;0;False;0;1;1;True;;KeywordEnum;2;Add;Blend;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;120;-153.3495,627.5013;Inherit;False;Property;_AddBlend;Add&Blend;13;0;Create;True;0;0;0;False;0;False;0;1;1;True;;KeywordEnum;2;Add;Blend;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;38;-242.3874,-116.6701;Inherit;False;Property;_MainColor;MainColor;18;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;6.656253,12.42868,19.33049,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;1189.594,375.6266;Inherit;False;Property;_Dst;Dst;6;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.BlendMode;True;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;117;1183.443,454.7636;Inherit;False;Property;_ZTest;ZTest;3;1;[Enum];Create;True;0;3;Default;0;On;1;Off;2;1;UnityEngine.Rendering.CompareFunction;True;0;False;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;517.1124,620.0623;Inherit;True;6;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;1193.243,207.2117;Inherit;False;Property;_ZWrite;ZWrite;2;1;[Enum];Create;True;0;3;Default;0;On;1;Off;2;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;12;-2364.854,122.6223;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;287.1578,81.56488;Inherit;True;10;10;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;3;1190.594,129.6264;Inherit;False;Property;_CullMode;CullMode;4;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;1192.594,294.6267;Inherit;False;Property;_Src;Src;5;1;[Enum];Create;True;0;0;1;UnityEngine.Rendering.BlendMode;True;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;126;851.7092,43.25955;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=DepthOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;123;851.7092,43.25955;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;124;851.7092,43.25955;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;15;VFX/DisNew 1;2992e84f91cbeb14eab234972e07ea9d;True;Forward;0;1;Forward;8;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;False;True;1;5;False;-1;10;False;-1;1;1;False;-1;10;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;0;Hidden/InternalErrorShader;0;0;Standard;22;Surface;1;  Blend;0;Two Sided;1;Cast Shadows;0;  Use Shadow Threshold;0;Receive Shadows;0;GPU Instancing;0;LOD CrossFade;0;Built-in Fog;0;DOTS Instancing;0;Meta Pass;0;Extra Pre Pass;0;Tessellation;0;  Phong;0;  Strength;0.5,False,-1;  Type;0;  Tess;16,False,-1;  Min;10,False,-1;  Max;25,False,-1;  Edge Length;16,False,-1;  Max Displacement;25,False,-1;Vertex Position,InvertActionOnDeselection;1;0;5;False;True;False;False;False;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;125;851.7092,43.25955;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;127;851.7092,43.25955;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;19;0;22;0
WireConnection;19;1;23;0
WireConnection;17;0;15;0
WireConnection;17;1;16;0
WireConnection;20;0;21;0
WireConnection;20;2;19;0
WireConnection;18;0;14;0
WireConnection;18;2;17;0
WireConnection;24;1;20;0
WireConnection;13;1;18;0
WireConnection;25;0;13;0
WireConnection;25;1;24;0
WireConnection;52;0;13;0
WireConnection;52;1;24;0
WireConnection;52;2;53;0
WireConnection;54;1;25;0
WireConnection;54;0;52;0
WireConnection;27;0;54;0
WireConnection;99;0;97;0
WireConnection;99;1;96;0
WireConnection;46;0;45;0
WireConnection;48;0;46;0
WireConnection;48;1;47;0
WireConnection;33;0;29;0
WireConnection;11;0;9;0
WireConnection;11;1;10;0
WireConnection;100;0;72;0
WireConnection;100;2;99;0
WireConnection;90;0;88;0
WireConnection;55;1;100;0
WireConnection;92;0;90;0
WireConnection;92;1;89;0
WireConnection;49;0;50;0
WireConnection;49;2;48;0
WireConnection;79;0;33;0
WireConnection;79;1;30;0
WireConnection;75;0;73;0
WireConnection;63;0;60;0
WireConnection;63;1;64;3
WireConnection;8;0;7;0
WireConnection;8;2;11;0
WireConnection;78;0;8;0
WireConnection;78;1;79;0
WireConnection;107;0;106;1
WireConnection;107;1;106;2
WireConnection;59;0;57;0
WireConnection;59;1;61;0
WireConnection;59;2;63;0
WireConnection;76;0;75;0
WireConnection;76;1;74;0
WireConnection;93;0;91;0
WireConnection;93;2;92;0
WireConnection;44;1;49;0
WireConnection;56;0;55;1
WireConnection;56;1;57;0
WireConnection;108;0;107;0
WireConnection;108;1;109;0
WireConnection;58;0;56;0
WireConnection;58;1;59;0
WireConnection;122;0;44;1
WireConnection;122;1;44;4
WireConnection;77;0;78;0
WireConnection;77;2;76;0
WireConnection;112;0;110;0
WireConnection;94;1;93;0
WireConnection;62;0;58;0
WireConnection;104;0;108;0
WireConnection;104;1;77;0
WireConnection;95;0;122;0
WireConnection;95;1;94;1
WireConnection;114;0;112;0
WireConnection;114;1;111;0
WireConnection;67;0;95;0
WireConnection;66;0;62;0
WireConnection;115;0;113;0
WireConnection;115;2;114;0
WireConnection;1;1;104;0
WireConnection;116;1;115;0
WireConnection;70;0;1;0
WireConnection;70;1;71;0
WireConnection;101;0;102;0
WireConnection;42;1;1;1
WireConnection;42;0;1;4
WireConnection;103;0;101;0
WireConnection;119;0;116;0
WireConnection;119;1;118;0
WireConnection;34;0;70;0
WireConnection;34;1;35;0
WireConnection;121;1;42;0
WireConnection;121;0;85;0
WireConnection;84;1;69;0
WireConnection;84;0;85;0
WireConnection;86;1;68;0
WireConnection;86;0;85;0
WireConnection;120;1;43;4
WireConnection;120;0;85;0
WireConnection;40;0;38;4
WireConnection;40;1;42;0
WireConnection;40;2;43;4
WireConnection;40;3;68;0
WireConnection;40;4;69;0
WireConnection;40;5;103;0
WireConnection;12;0;8;0
WireConnection;12;1;33;0
WireConnection;12;2;30;0
WireConnection;36;0;34;0
WireConnection;36;1;37;0
WireConnection;36;2;38;0
WireConnection;36;3;43;0
WireConnection;36;4;86;0
WireConnection;36;5;84;0
WireConnection;36;6;120;0
WireConnection;36;7;119;0
WireConnection;36;8;121;0
WireConnection;36;9;103;0
WireConnection;124;2;36;0
WireConnection;124;3;40;0
ASEEND*/
//CHKSM=31604930ECBB1E6F06E0222664349F8C7CA4ECDB