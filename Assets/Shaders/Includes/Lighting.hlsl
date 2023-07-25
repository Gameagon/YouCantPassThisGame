#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

#ifndef SHADERGRAPH_PREVIEW
half CustomLinearAttenuation(uint lightIndexInBuffer, Light light, float3 worldPos)
{
	/*float range = rsqrt(light.distanceAttenuation);
    float distance = worldPos - light.position;

    return clamp(1.0f - (distance / range), 0.0, 1.0);*/

#if USE_STRUCTURED_BUFFER_FOR_LIGHT_DATA
        float4 lightPositionWS = _AdditionalLightsBuffer[lightIndexInBuffer].position;
		half4 distanceAndSpotAttenuation = _AdditionalLightsBuffer[lightIndexInBuffer].attenuation;
		half4 spotDirection = _AdditionalLightsBuffer[lightIndexInBuffer].spotDirection;
        float range = rsqrt(_AdditionalLightsBuffer[lightIndexInBuffer].attenuation);
#else
    float4 lightPositionWS = _AdditionalLightsPosition[lightIndexInBuffer];
    half4 distanceAndSpotAttenuation = _AdditionalLightsAttenuation[lightIndexInBuffer];
    half4 spotDirection = _AdditionalLightsSpotDir[lightIndexInBuffer];
    float range = rsqrt(_AdditionalLightsAttenuation[lightIndexInBuffer]);
#endif
	
    float distance = length(lightPositionWS - worldPos);
	
    return clamp(1.0f - (distance / range), 0.0, 1.0) * AngleAttenuation(spotDirection.xyz, light.direction, distanceAndSpotAttenuation.zw);
}
#endif

void CalculateMainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out half DistanceAtten, out half ShadowAtten)
{
#if SHADERGRAPH_PREVIEW
	Direction = half3(0.5, 0.5, 0);
	Color = 1;
	DistanceAtten = 1;
	ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
	half4 clipPos = TransformWorldToHClip(WorldPos);
	half4 shadowCoord = ComputeScreenPos(clipPos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
	Light mainLight = GetMainLight(shadowCoord);
	Direction = mainLight.direction;
	Color = mainLight.color;
	DistanceAtten = mainLight.distanceAttenuation;
	ShadowAtten = mainLight.shadowAttenuation;
#endif
}

void AddAdditionalLights_float(float Smoothness, float RawSpecular, float3 WorldPosition, float3 WorldNormal, float3 WorldView,
	float MainDiffuse, float MainSpecular, float3 MainColor,
	out float Diffuse, out float Specular, out float3 Color)
{
	Diffuse = MainDiffuse;
	Specular = MainSpecular;
	Color = MainColor * (MainDiffuse + MainSpecular);

#ifndef SHADERGRAPH_PREVIEW
	int pixelLightCount = GetAdditionalLightsCount();
	for (int i = 0; i < pixelLightCount; ++i)
	{
        Light light = GetAdditionalLight(i, WorldPosition, 1);
		half NdotL = saturate(dot(WorldNormal, light.direction));
        half atten = CustomLinearAttenuation(GetPerObjectLightIndex(i), light, WorldPosition) * light.shadowAttenuation; //light.distanceAttenuation * light.shadowAttenuation;
		half thisDiffuse = atten * NdotL;
		half thisSpecular = LightingSpecular(thisDiffuse, light.direction, WorldNormal, WorldView, 1, exp2(Smoothness * 10)) * Smoothness * RawSpecular;
		Diffuse += thisDiffuse;
		Specular += thisSpecular;
		Color += light.color * (thisDiffuse + thisSpecular);
	}
#endif

	half total = Diffuse + Specular;
	// If no light touches this pixel, set the color to the main light's color
	Color = total <= 0 ? MainColor : Color / total;
}

/*void AddGlobalIllumination_float(float Smoothness, float3 BakedGI, float AmbientOcclusion, float3 WorldNormal, float3 WorldView,
float MainDiffuse, float MainSpecular, float3 MainColor,
out float Diffuse, out float Specular, out float3 Color)
{
	Diffuse = MainDiffuse;
	Specular = MainSpecular;
	Color = MainColor;
	
	Diffuse += length(BakedGI)/3 * AmbientOcclusion;
	
#ifndef SHADERGRAPH_PREVIEW
	float3 reflectVector = reflect(-WorldView, WorldNormal);
	// This is a rim light term, making reflections stronger along
	// the edges of view, maybe something likeSubsurface?
	float fresnel = Pow4(1 - saturate(dot(WorldView, WorldNormal)));
	// This function samples the baked reflections cubemap
	// It is located in URP/ShaderLibrary/Lighting.hlsl
	Specular += GlossyEnvironmentReflection(reflectVector,
		sqrt(1 - Smoothness),
		AmbientOcclusion) * fresnel;
#endif

	Color += BakedGI * AmbientOcclusion; //indirectDiffuse + indirectSpecular;
}*/

void GetGlobalIllumination_float(float Smoothness, float3 Albedo, float3 BakedGI, float AmbientOcclusion, float3 WorldNormal, float3 WorldView, out float3 Color)
{
#ifndef SHADERGRAPH_PREVIEW
	float3 indirectDiffuse = Albedo * BakedGI * AmbientOcclusion;

	float3 reflectVector = reflect(-WorldView, WorldNormal);
	// This is a rim light term, making reflections stronger along
	// the edges of view, maybe something likeSubsurface?
	float fresnel = Pow4(1 - saturate(dot(WorldView, WorldNormal)));
	// This function samples the baked reflections cubemap
	// It is located in URP/ShaderLibrary/Lighting.hlsl
	float3 indirectSpecular = GlossyEnvironmentReflection(reflectVector,
		sqrt(1 - Smoothness),
		AmbientOcclusion) * fresnel;

	Color = indirectDiffuse + indirectSpecular;
#else
	Color = float3(0,0,0);
#endif
}

#endif