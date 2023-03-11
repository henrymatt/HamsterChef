#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

struct CustomLightingData {
	float3 globalLighting;

	float3 normalWS;

	//Surface attributes
	float3 albedo;
};

#ifndef SHADERGRAPH_PREVIEW
float3 CustomLightHandling(CustomLightingData d, Light light) {
	float3 radiance = light.color;

	float diffuse = saturate(dot(d.normalWS, light.direction));

	float3 color = d.albedo * radiance * diffuse + d.globalLighting;

	return color;
}
#endif

float3 CalculateCustomLighting(CustomLightingData d) {

#ifdef SHADERGRAPH_PREVIEW
	float3 lightDir = float3(0.5, 0.5, 0);
	float intensity = saturate(dot(d.normalWS, lightDir));
	return d.albedo * intensity + d.globalLighting;
#else
	Light mainLight = GetMainLight();

	float color = 0;

	color += CustomLightHandling(d, mainLight);

	return color;
#endif
}

void CalculateCustomLighting_float(float3 Albedo, float3 Normal, float3 GlobalLighting, out float3 Color)
{
	CustomLightingData d;
	d.normalWS = Normal;
	d.albedo = Albedo;
	d.globalLighting = GlobalLighting;

	Color = CalculateCustomLighting(d);
}
#endif