// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "projector decal"
{
	Properties
	{
		_Color("Color tint", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		[NoScaleOffset]_FalloffTex("FalloffTex", 2D) = "white" {}
		[HideInInspector]_MainTex_ST("_MainTex_ST", Vector) = (0,0,0,0)
	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Back
		ColorMask RGB
		ZWrite Off
		ZTest Equal
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "SubShader 0 Pass 0"
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
			};

			uniform float4 _Color;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;
			uniform sampler1D _FalloffTex;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 vertexToFrag46 = mul( unity_Projector, v.vertex );
				o.ase_texcoord = vertexToFrag46;
				float4 vertexToFrag58 = mul( unity_ProjectorClip, v.vertex );
				o.ase_texcoord1 = vertexToFrag58;
				
				float3 vertexValue =  float3(0,0,0) ;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 vertexToFrag46 = i.ase_texcoord;
				float2 temp_output_49_0 = ( (vertexToFrag46).xy / (vertexToFrag46).w );
				float2 clampResult51 = clamp( temp_output_49_0 , float2( 0,0 ) , float2( 1,1 ) );
				float2 temp_output_65_0 = ( ceil( clampResult51 ) * ( 1.0 - floor( clampResult51 ) ) );
				float4 vertexToFrag58 = i.ase_texcoord1;
				float clampResult89 = clamp( ( (vertexToFrag58).x / (vertexToFrag58).w ) , 0.0 , 1.0 );
				float4 lerpResult81 = lerp( float4(0,0,0,0) , ( _Color * tex2D( _MainTex, ( ( (_MainTex_ST).xy * temp_output_49_0 ) + (_MainTex_ST).zw ) ) ) , ( ( (temp_output_65_0).x * (temp_output_65_0).y ) * ( ceil( clampResult89 ) * ( 1.0 - floor( clampResult89 ) ) ) * tex1D( _FalloffTex, clampResult89 ).a ));
				
				
				finalColor = lerpResult81;
				return finalColor;
			}
			ENDCG
		}
	}
	
	
	
}
/*ASEBEGIN
Version=16400
1059.2;860.8;2003;1074;2761.193;295.1647;1.803545;True;False
Node;AmplifyShaderEditor.PosVertexDataNode;43;-2425.442,387.1014;Float;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.UnityProjectorMatrixNode;44;-2425.442,307.1013;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-2217.442,307.1013;Float;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.VertexToFragmentNode;46;-2073.442,307.1013;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.UnityProjectorClipMatrixNode;52;-1752.041,657.4957;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PosVertexDataNode;50;-1748.207,738.7954;Float;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-1540.206,658.7957;Float;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;48;-1686.762,203.2738;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;47;-1686.762,283.2739;Float;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;58;-1396.206,658.7957;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;49;-1430.758,201.1191;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;51;-1226.173,355.2899;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;66;-1152.675,744.9116;Float;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;62;-1153.253,656.3192;Float;False;True;False;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FloorOpNode;53;-1038.719,429.8408;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;55;-1783.394,29.54313;Float;False;Property;_MainTex_ST;_MainTex_ST;3;1;[HideInInspector];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;70;-888.7473,682.2407;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;89;-731.431,668.0532;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CeilOpNode;60;-869.0073,341.2421;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;59;-903.4379,421.0124;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;61;-1508.028,11.77732;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;67;-1511.581,93.4994;Float;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-1160.138,21.93143;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FloorOpNode;83;-491.8367,671.4907;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-683.9579,384.7869;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;84;-364.7811,674.4842;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;72;-974.1196,128.8889;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;71;-486.5352,439.7665;Float;False;False;True;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CeilOpNode;82;-354.0898,592.7508;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;68;-492.0097,348.4073;Float;False;True;False;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;74;-671.2433,-43.37442;Float;False;Property;_Color;Color tint;0;0;Create;False;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;76;-756.345,124.8532;Float;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;None;6e41609428c07fb48b143d82d8e5acab;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-259.0898,370.6874;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-146.8581,645.6469;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;73;-364.0884,786.9185;Float;True;Property;_FalloffTex;FalloffTex;2;1;[NoScaleOffset];Create;True;0;0;False;0;None;387bb7a9ff2786f4f9348e54f1fbbf0b;True;0;False;white;LockedToTexture1D;False;Object;-1;Auto;Texture1D;6;0;SAMPLER2D;;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;129.2554,498.2982;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-252.6188,197.6745;Float;False;2;2;0;COLOR;1,1,1,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector4Node;80;68.64397,40.89046;Float;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;81;347.7766,335.8401;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;1,1,1,0.5;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;56;-1692.991,366.1816;Float;False;False;False;True;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;38;979.4385,428.9477;Float;False;True;2;Float;;0;1;projector decal;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;82;0;5;False;-1;10;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;False;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;5;False;-1;True;True;0;False;-1;0;False;-1;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;False;False;False;False;False;False;False;False;False;True;0;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;45;0;44;0
WireConnection;45;1;43;0
WireConnection;46;0;45;0
WireConnection;54;0;52;0
WireConnection;54;1;50;0
WireConnection;48;0;46;0
WireConnection;47;0;46;0
WireConnection;58;0;54;0
WireConnection;49;0;48;0
WireConnection;49;1;47;0
WireConnection;51;0;49;0
WireConnection;66;0;58;0
WireConnection;62;0;58;0
WireConnection;53;0;51;0
WireConnection;70;0;62;0
WireConnection;70;1;66;0
WireConnection;89;0;70;0
WireConnection;60;0;51;0
WireConnection;59;0;53;0
WireConnection;61;0;55;0
WireConnection;67;0;55;0
WireConnection;63;0;61;0
WireConnection;63;1;49;0
WireConnection;83;0;89;0
WireConnection;65;0;60;0
WireConnection;65;1;59;0
WireConnection;84;0;83;0
WireConnection;72;0;63;0
WireConnection;72;1;67;0
WireConnection;71;0;65;0
WireConnection;82;0;89;0
WireConnection;68;0;65;0
WireConnection;76;1;72;0
WireConnection;75;0;68;0
WireConnection;75;1;71;0
WireConnection;85;0;82;0
WireConnection;85;1;84;0
WireConnection;73;1;89;0
WireConnection;78;0;75;0
WireConnection;78;1;85;0
WireConnection;78;2;73;4
WireConnection;79;0;74;0
WireConnection;79;1;76;0
WireConnection;81;0;80;0
WireConnection;81;1;79;0
WireConnection;81;2;78;0
WireConnection;56;0;46;0
WireConnection;38;0;81;0
ASEEND*/
//CHKSM=7C9F562950DDF1910D1977E6DF5CB48833FB66C8