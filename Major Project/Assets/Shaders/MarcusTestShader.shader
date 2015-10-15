Shader "MarcusTestShader" {
   Properties {
   _MainTex("Texture",2D) = "White"{}
	}
	SubShader {

		Tags {  "RenderType"="Opaque"} //???
		LOD 200 //???
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows



		sampler2D _MainTex;
	

			struct Input 
			{
			 float2 uv_MainTex;
			};




		void surf (Input IN, inout SurfaceOutputStandard o)
		{
		  fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
		  o.Albedo = c.rgb;


		}
		ENDCG
	} 

}

