#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

#pragma multi_compile _ _ADDITIONAL_LIGHTS

struct CustomLightingData {
	float3 globalLighting;

	float3 normalWS;

	float3 worldPosition;

	//Surface attributes
	float3 albedo;
};

#ifndef SHADERGRAPH_PREVIEW
float3 CustomLightHandling(CustomLightingData d, Light light) {
	//float3 radiance = float3(clamp(light.color.r, 0, 1), clamp(light.color.g, 0, 1), clamp(light.color.b, 0, 1));
	float3 radiance = light.color;

	float diffuse = saturate(dot(d.normalWS, light.direction));

	float3 color = d.albedo * radiance * diffuse * (light.distanceAttenuation * light.shadowAttenuation);

	return color;
}
#endif

float3 CalculateCustomLighting(CustomLightingData d) {

#ifdef SHADERGRAPH_PREVIEW
	float3 lightDir = float3(0.5, 0.5, 0);
	float intensity = saturate(dot(d.normalWS, lightDir));
	return clamp(d.albedo * intensity, 0, 1);
#else
	Light mainLight = GetMainLight();
	float color = 0;
	color += CustomLightHandling(d, mainLight);

	int pixelLightCount = GetAdditionalLightsCount();
	for (int i = 0; i < pixelLightCount; i++)
	{
		Light light = GetAdditionalLight(i, d.worldPosition);

		color += CustomLightHandling(d, light);
	}

	color = clamp(color, max(max(mainLight.color.r, mainLight.color.g), mainLight.color.b) * d.globalLighting, 1);

	return color;
#endif
}

void CalculateCustomLighting_float(float3 Albedo, float3 Normal, float3 GlobalLighting, float3 WorldPosition, out float3 Color)
{
	CustomLightingData d;
	d.normalWS = Normal;
	d.albedo = Albedo;
	d.globalLighting = GlobalLighting;

	d.worldPosition =  WorldPosition;

	Color = CalculateCustomLighting(d);
}
#endif