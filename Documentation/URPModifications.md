## URP Modifications

The Universal Render Pipeline (URP) is an excellent foundation for projects, offering many features and flexibility to start development. To better meet project needs, we modified URP shaders and code.

**Shader Modifications**

We replaced the Unity BRDF with a more accurate approximation, enhancing lighting on the ocean and shiny surfaces. This change slightly increases the cost for all surfaces. To mitigate this, we included a specialized non-metallic BRDF for non-metallic surfaces, configurable in the Shader Graph Settings by toggling “Non-Metallic Surface.”

Unity shadowing is flexible, allowing runtime selection between quality settings. However, reading cbuffer values, reducing shader size, and potentially reducing register usage present optimization opportunities. We hard-coded these values where appropriate. Surfaces facing away from the light source or with fully attenuated light are unaffected by shadow sampling. We moved shadow sampling into a branch to prevent sampling from the shadow map for surfaces not facing the light source.

The ship uses Reflection Probes to simulate reflections on the deck, especially when wet. These probes and the box projection must rotate with the ship for realism. We achieve this by passing a _ProbeReorientation matrix into GlobalIllumination.hlsl and applying it when calculating the cubemap sample point.

### Relevant Files
- [BRDF.hlsl](../Packages/com.unity.render-pipelines.universal/ShaderLibrary/BRDF.hlsl)
- [UniversalLitSubTarget.cs](../Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Targets/UniversalLitSubTarget.cs)
- [Shadows.hlsl](../Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl)
- [Lighting.hlsl](../Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl)
- [GlobalIllumination.hlsl](../Packages/com.unity.render-pipelines.universal/ShaderLibrary/GlobalIllumination.hlsl)

**Code Modifications**

By default, the XR display matches the MSAA value configured in the URP asset. However, any post-processing forces an MSAA resolve, making it unnecessary to blit to an MSAA display target.

URP versions before 14.0.9 have a bug causing excessive texture reallocation with Dynamic Resolution, leading to poor framerate and potential out-of-memory crashes. Fix this by following the guide [here](https://developers.meta.com/horizon/documentation/unity/dynamic-resolution-unity/). We also enabled DynamicResolution support in the RTHandles system immediately after initializing it in UniversalRenderPipeline.cs.

We use ShadowImportanceVolumes to dynamically adjust the shadow map projection, requiring an entry point in ShadowUtils. This allows modifying the shadow projection matrix and distance values. We also modified MainLightShadowCasterPass.cs to pass through camera data for accurate shadow importance volumes.

### Relevant Files
- [UniversalRenderPipeline.cs](../Packages/com.unity.render-pipelines.universal/Runtime/UniversalRenderPipeline.cs)
- [UniversalRenderer.cs](../Packages/com.unity.render-pipelines.universal/Runtime/UniversalRenderer.cs)
- [ShadowUtils.cs](../Packages/com.unity.render-pipelines.universal/Runtime/ShadowUtils.cs)
- [MainLightShadowCasterPass.cs](../Packages/com.unity.render-pipelines.universal/Runtime/Passes/MainLightShadowCasterPass.cs)
