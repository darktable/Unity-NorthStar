# Vertex Shader

The ocean shader starts by sampling the displacement map in the vertex shader. UVs are calculated from the world position of the vertices, divided by the ocean's total scale.

A Displacement Noise texture is sampled with a scale independent of the ocean's scale. This noise controls the displacement strength, varying the effect across the ocean's surface to reduce noticeable tiling. Choosing the right noise texture and scale is crucial to avoid excessive displacement reduction across the ocean.

The horizontal displacement length, multiplied by the displacement noise strength, is passed to the fragment shader for fake subsurface scattering.

Additionally, a "Giant Wave" feature is used in some cutscenes to create a large wave moving towards the boat. It uses a single Gerstner wave with a masking function to limit the effect to one wave of a specified width. Due to time constraints, this was implemented in HLSL via a custom function node instead of Shadergraph. The wave gradually rises from the ocean and quickly fades after hitting the boat. Parameters from C# code ensure the wave always moves towards the boat. The output is an additional displacement and partial derivatives, combined with existing ocean displacement/normals.

![](./Images/OceanShader/Fig1.png)

The giant wave includes fudge factors to align and hit the ship, then fade. However, achieving this in all situations was challenging, so some parameters are controlled through Timeline curves for extra control. Some equations could be improved to reduce manual tweaking.

# Fragment Shader

The tiling normal map is sampled using the same UVs as the displacement map. It contains foam and smoothness values in the blue/alpha channels, discussed shortly. The normal value includes the X and Z components, with the Y component reconstructed using a `NormalReconstructZ` function. Since the ocean is a world-space grid, a regular tangent-to-world transform isn't needed; we can swizzle the Y and Z channels.

A detail normal map is sampled with a different scale/offset than the main normal map, usually smaller than the primary ocean normal map. The normal maps are combined using partial derivative blending, accurately combining different normals from heightmaps. This normal map can be scaled, panned, and rotated over time. When panned similarly to the ocean, with a slightly offset rotation and speed, it adds convincing detail to the lower-resolution ocean simulation.

When the giant wave is enabled, partial derivatives combine with the ocean normal and detail normal partial derivatives to produce the final world space normal. The giant wave is re-evaluated in the pixel shader to obtain normals, as per-vertex passing often lacked accuracy, causing visual artifacts.

# Foam

![](./Images/OceanShader/Fig4.gif)

A value for foam coverage is packed into the normal map's blue channel. It's converted into a foam mask with a configurable threshold using `saturate(foamStrength * (threshold – normalMap.b))`. This allows foam variation across ocean profiles. It's multiplied with a detail foam texture. The result is used to lerp between the ocean’s blue albedo color and a configurable foam color. While often left white, a slight tint can be used for specific effects.

Shore foam functionality was implemented by sampling the depth buffer and using the depth difference to produce foam along object edges intersecting the water. However, for optimization, it's not used, as it depends on the scene depth texture, and copying it can impact performance.

The total foam amount masks out underwater effects like subsurface scattering, as heavy foam layers block strong underwater light.

# Subsurface Scattering

![](./Images/OceanShader/Fig0.png)

The subsurface scattering effect adds tinting/highlights inside steep/tall waves. It's implemented using the horizontal displacement length from the vertex shader, as tall waves appear where there's significant horizontal displacement, making subsurface scattering noticeable. The effect uses a two-lobe phase function, allowing configurable forward and backward scattering. The result is multiplied by a configurable color. While not physically based, it works well in practice, adding detail to larger waves and breaking the ocean's uniform look.

# Refraction and Underwater Tint (Not used)

A fake refraction effect was included to sample the scene color behind the water surface, but due to performance costs from copying the scene texture, it's not used. The effect is noticeable in shallow waters like the docks scene, so it was an acceptable compromise to maintain performance goals. The effect involves multiplying the world space normal XZ components by a small factor to offset a screenspace UV, used to sample the scene color texture.

The depth texture is sampled at this location, and the scene color is tinted using a simple exponential fog formula: `sceneColor *= exp(-depthDifference * extinction)`. Extinction is calculated using the “Color at Distance to Extinction” function, which calculates the extinction coefficient for a specific tint color at a specific distance, more intuitive than working with an extinction coefficient directly.

The water albedo also needs modification by the inverse of this amount, so shallow water areas don't have more scattering/fog than they should due to their shallow depth.

# Underwater Surface

![](./Images/OceanShader/Fig3.png)

In the game's final part, where the player is underwater, a duplicate water shader is used with double-sided rendering enabled and extra logic for the water's backfaces. A custom fog function tints the underwater surface as it gets further from the player.

Additionally, the environment reflection lookup is modified to use a refracted direction instead of reflected. This produces the “Snell's window” effect observed when looking up at an underwater surface, where parts of the sky are visible in a circle above the viewer. Using the existing environment cubemap means this doesn't require a scene texture copy, so the performance overhead is minimal, as it replaces the sky reflection lookup in a regular PBR shader. The downside is that above-water objects like the ship aren't visible, but a similar effect could be achieved with alpha blending if desired.

# Parameter Overview

- **Albedo:** Represents the general color of the water. It functions like albedo in any regular PBR shader but can be modified by the foam amount if enabled.

**Foam**

- **Enable Shore Foam:** Activates the shore foam effect, creating a foam line where objects intersect the water plane. Requires the depth texture.
- **Foam Texture:** The texture used for foam. Only the red channel is used to save bandwidth. A full-color texture with an alpha can be used if needed, but minor shader changes are required.
- **Foam Scale:** Determines the world space tiling of the foam. Larger numbers make the foam smaller and more detailed but more repetitive.
- **Foam Scroll:** Applies a world space panning effect to the foam for added visual interest. Often unnoticeable due to ocean movement, so it may not be necessary.
- **Foam Color:** A color tint applied to the foam texture. Used as a lerp parameter instead of a multiply to avoid blue-white foam that can't reach fully white.
- **Foam Strength:** Multiplies the foam texture strength. Higher values produce more foam but may reduce soft gradients at edges.
- **Foam Threshold:** Higher values create foam in more areas, while lower values restrict foam to the peaks of high waves.
- **Foam Aeration Color:** Adds detail to foam calculations using additional texture channels, but currently disabled for performance reasons.
- **Foam Aeration Threshold:** Controls secondary foam texture details, currently not enabled.

**Scatter**

- **Enable Subsurface:** Toggles the effect that produces extra light scattering from tall waves. Can be disabled in gentle seas or certain lighting to improve performance.
- **Scattering Color:** The color and intensity applied to the effect. Best values depend on the environment and ocean, often blue-green or green, as red light is absorbed and blue is scattered.
- **Forward Scatter:** Controls the sharpness of scattering along the light direction. High values give a small, bright highlight; smaller values spread the effect for a uniform look.
- **Back Scatter:** Produces extra scattering when looking away from the light source. Smaller values like 0.3 work better as back-scattering is usually more diffused.
- **Scatter Blend:** Balances the contribution of forward and back scatter. A value of 0.5 provides an equal combination.
- **Scatter Displacement Power:** Affects the strength of the scatter effect based on horizontal displacement. Larger displacements provide stronger scattering. A power function varies the effect from linear to dramatic.
- **Scatter Displacement Factor:** A multiplier for the scatter amount compared to ocean displacement, combined with the power function to control the overall effect.

**Normals**

- **Normal Map:** The secondary normal map combined with base ocean normals from the simulation. Any normal map can be used, but one baked from the ocean simulation is recommended for high quality.
- **Normal Map Size:** The area the normal map covers in world space. Should be smaller than the ocean profile’s patch size to provide detail at different scales and reduce tiling.
- **Normal Map Scroll:** Time-based offset applied to the normal map, should align with wind direction but not match exactly for interesting interactions.
- **Normal Map Strength:** Scales the strength of the detail normal map.
- **Normal Rotation:** Rotates the normal map UVs.

**Displacement Noise**

- **Noise Scroll:** Scrolls the displacement noise texture, similar to normals and foam. Often, minimal or no scrolling is sufficient.
- **Noise Texture:** The noise texture should tile seamlessly and contain a mix of light and dark areas to break up ocean simulation repetition without reducing displacement.
- **Noise Strength:** Controls how strongly ocean displacement is attenuated by the noise texture. A strength of 0 disables the effect.
- **Noise Scale:** Controls the size of the noise texture in world space. Should be larger than the ocean patch size to avoid noticeable repetition.

**Smoothness**

- **Smoothness Close:** The smoothness value for the ocean. Previously, a “Smoothness Far” slider was used for distance, but this is now baked into mip maps of the normal/foam/smoothness texture.

**Shore**

- **Enable Refraction:** Activates the underwater refraction effect, requiring the Camera Opaque Texture, which has a performance cost.
- **Refraction Offset:** Controls how much the refraction effect is offset based on the normal map.
- **Shore Foam Strength:** Controls the intensity of the shore foam effect.
- **Shore Foam Threshold:** Controls how quickly shore foam fades in at the edge of objects intersecting the water.
- **Shore Foam Fade:** Controls how quickly shore foam fades out with distance.
- **Depth Threshold:** Controls the distance at which an underwater object is fully tinted by the depth color.
- **Depth Color:** The color used to tint an underwater object at the “depth threshold” value.

**Giant Wave**

Settings generally set from C# code but can be used for control or debugging:

- **Giant Wave Height:** The peak height of the giant wave.
- **Giant Wave Width:** The length along the wave for effect calculation.
- **Giant Wave Length:** The wavelength of the giant wave, controlling width and speed.
- **Giant Wave Angle:** Controls the angle of wave movement towards the center.
- **Center:** The location the wave moves towards.
- **Falloff:** Controls the slope along the wave sides, fading it in.
- **Phase:** A debug option showing the wave's progress towards the target.
- **Giant Wave Steepness:** Controls the steepness of the Gerstner wave used for the giant wave.
- **Giant Wave Curve Strength:** Controls how strongly the wave peak curves towards its target for a dramatic effect.
- **Giant Wave Enabled:** Toggles the giant wave, saving calculations when disabled.

### Relevant Files

- [Water Realistic.shadergraph](https://github.com/meta-quest/Unity-UtilityPackages/blob/main/com.meta.utilities.environment/Runtime/Shaders/Water/Water%20Realistic.shadergraph)
- [UnderwaterShader.shader](../Assets/NorthStar/Shaders/Water/UnderwaterShader.shader)
