//UNITY_SHADER_NO_UPGRADE
#ifndef MAJORAS_MASK_IS_BETTER_THAN_OCARAINA_OF_TIME_COME_AT_ME_BRO_INCLUDED
#define MAJORAS_MASK_IS_BETTER_THAN_OCARAINA_OF_TIME_COME_AT_ME_BRO_INCLUDED
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"

void N64Sample_float(
    in UnityTexture2D Texture,
    in float2 UV,
    out float4 Out)
{
    // texel coordinates
    float4 texelSize = Texture.texelSize;
    float2 texels = UV * texelSize.zw;

    // calculate mip level
    float2 dx = ddx(texels);
    float2 dy = ddy(texels);
    float delta_max_sqr = max(dot(dx, dx), dot(dy, dy));
    float mip = max(0.0, 0.5 * log2(delta_max_sqr));

    // scale texel sizes and texel coordinates to handle mip levels properly
    float scale = pow(2, floor(mip));
    texelSize.xy *= scale;
    texelSize.zw /= scale;
    texels = texels / scale - 0.5;

    // calculate blend for the three points of the tri-filter
    float2 fracTexels = frac(texels);
    float3 blend = float3(
        abs(fracTexels.x + fracTexels.y - 1),
        min(abs(fracTexels.xx - float2(0, 1)), abs(fracTexels.yy - float2(1, 0)))
        );

    // calculate equivalents of point filtered uvs for the three points
    float2 uvA = (floor(texels + fracTexels.yx) + 0.5) * texelSize.xy;
    float2 uvB = (floor(texels) + float2(1.5, 0.5)) * texelSize.xy;
    float2 uvC = (floor(texels) + float2(0.5, 1.5)) * texelSize.xy;

    // sample points
    float4 A = Texture.SampleLevel(Texture.samplerstate, uvA, mip);
    float4 B = Texture.SampleLevel(Texture.samplerstate, uvB, mip);
    float4 C = Texture.SampleLevel(Texture.samplerstate, uvC, mip);

    // blend and return
    Out = A * blend.x + B * blend.y + C * blend.z;
}
#endif