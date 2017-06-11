Shader "Gargore/Water1 Transparent" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	//_MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {}
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_NormalMultiplier ("Normals Multiplier", Range (0.0, 1.0)) = 1.0
	_BumpMap2 ("Normalmap 2", 2D) = "bump" {}
	_NormalMultiplier2 ("Normals Multiplier2", Range (0.0, 1.0)) = 1.0
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 300
	
CGPROGRAM
#pragma surface surf Lambert alpha
#pragma exclude_renderers d3d11_9x

//sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _BumpMap2;
samplerCUBE _Cube;

fixed4 _Color;
fixed4 _ReflectColor;
float _NormalMultiplier;
float _NormalMultiplier2;

struct Input {
	//float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_BumpMap2;
	float3 worldRefl;
	INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) {
	//fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	//fixed4 c = tex * _Color;
	fixed4 c = _Color;
	o.Albedo = c.rgb;
	
	//o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)) * _NormalMultiplier + UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap)) * (_NormalMultiplier2);
	o.Normal = normalize(UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)) * _NormalMultiplier + UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap2)) * (_NormalMultiplier2));
	
	float3 worldRefl = WorldReflectionVector(IN, o.Normal);
	fixed4 reflcol = texCUBE(_Cube, worldRefl);
	//reflcol *= tex.a;
	o.Emission = reflcol.rgb * _ReflectColor.rgb;
	o.Alpha = _ReflectColor.a * c.a;
}
ENDCG
}

FallBack "Reflective/VertexLit"
}



/*
Shader "Gargore/Water1 Transparent" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	//_MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {}
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_NormalMultiplier ("Normals Multiplier", Range (0.0, 1.0)) = 1.0
	_BumpMap2 ("Normalmap 2", 2D) = "bump" {}
	_NormalMultiplier2 ("Normals Multiplier2", Range (0.0, 1.0)) = 1.0
}

SubShader {
	Tags { "Queue"="Transparent" "RenderType"="Transparent" }
	LOD 300
	
CGPROGRAM
#pragma surface surf Lambert
#pragma exclude_renderers d3d11_9x

//sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _BumpMap2;
samplerCUBE _Cube;

fixed4 _Color;
fixed4 _ReflectColor;
float _NormalMultiplier;
float _NormalMultiplier2;

struct Input {
	//float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_BumpMap2;
	float3 worldRefl;
	INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) {
	//fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	//fixed4 c = tex * _Color;
	fixed4 c = _Color;
	o.Albedo = c.rgb;
	
	//o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)) * _NormalMultiplier + UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap)) * (_NormalMultiplier2);
	o.Normal = normalize(UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)) * _NormalMultiplier + UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap2)) * (_NormalMultiplier2));
	
	float3 worldRefl = WorldReflectionVector(IN, o.Normal);
	fixed4 reflcol = texCUBE(_Cube, worldRefl);
	//reflcol *= tex.a;
	//o.Emission = reflcol.rgb * _ReflectColor.rgb;
	//o.Alpha = reflcol.a * _ReflectColor.a;
	o.Alpha = _ReflectColor.a;
}
ENDCG
}

FallBack "Reflective/VertexLit"
}
*/

/*
Shader "Gargore/Water1 Transparent" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_NormalMultiplier ("Normals Multiplier", Range (0.0, 1.0)) = 1.0
	_BumpMap2 ("Normalmap 2", 2D) = "bump" {}
	_NormalMultiplier2 ("Normals Multiplier2", Range (0.0, 1.0)) = 1.0
	//_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 400
	
CGPROGRAM
#pragma surface surf BlinnPhong alpha

//sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _BumpMap2;
samplerCUBE _Cube;

fixed4 _Color;
fixed4 _ReflectColor;
float _NormalMultiplier;
float _NormalMultiplier2;
//half _Shininess;

struct Input {
	//float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_BumpMap2;
	float3 worldRefl;
	INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) {
	//fixed3 normalvector = normalize(UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)) * _NormalMultiplier + UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap2)) * (_NormalMultiplier2));
	//float3 worldRefl = WorldReflectionVector(IN, normalvector);
	//fixed4 reflcol = texCUBE(_Cube, worldRefl);
	//
	////fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	//fixed4 tex = _Color;
	////tex.rgb = normalvector;
	////o.Albedo = tex.rgb * _Color.rgb;
	//o.Albedo = normalvector;
	////o.Albedo = reflcol.rgb;
	//o.Gloss = tex.a;
	////o.Emission = reflcol.rgb * _ReflectColor.rgb;
	//o.Alpha = tex.a * _Color.a;
	////o.Specular = _Shininess;
	////o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	//o.Normal = normalvector;

	fixed4 c = _Color;
	o.Albedo = c.rgb;
	o.Normal = normalize(UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)) * _NormalMultiplier + UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap2)) * (_NormalMultiplier2));
	float3 worldRefl = WorldReflectionVector(IN, o.Normal);
	fixed4 reflcol = texCUBE(_Cube, worldRefl);
	//o.Emission = reflcol.rgb * _ReflectColor.rgb;
	o.Alpha = reflcol.a * _ReflectColor.a;
}
ENDCG
}

FallBack "Transparent/VertexLit"
}
*/