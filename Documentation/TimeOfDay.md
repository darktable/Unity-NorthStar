# Time of Day System

![](./Images/TimeOfDay/Fig0.gif)

The Time of Day System in NorthStar is managed by an environment profile, centralizing settings for efficient scene management and real-time adjustments. It supports real-time previews and transitions, even in scene mode, for faster iteration and development.

Smooth transitions are achieved by lerping between floats, vectors, and colors. Precomputed values minimize GPU overhead. The system directly controls environment parameters, avoiding costly techniques like full atmospheric scattering and complex cloud lighting.

## Environment System

![](./Images/TimeOfDay/Fig1.png)

The Environment System is the core of the Time of Day System. It:
- Manages scene object references.
- Stores global and default values.
- Handles transitions between environment profiles.
- Integrates rendering logic with the current render pipeline via RenderPipelineManager callbacks.

**Custom Rendering Logic**

This system manages key rendering tasks, including:
- Updating ocean simulations.
- Rendering the ocean quadtree.
- Rendering the sun and moon discs.
- Setting global shader properties.

## Skybox Updater

The Skybox Updater manages environmental lighting by adjusting:
- Skybox reflection probes.
- Ambient lighting.
- Fog settings.
- Directional light angles and colors.

These updates ensure accurate lighting and atmospheric transitions throughout the day.

## Sun Disk Renderer

The Sun Disk Renderer renders the sun and moon as simple quads with a custom shader, rather than embedding logic within the skybox shader.

**Key Features:**
- Basic color and texture functionality.
- Simulates distant light source illumination (e.g., moon shading using a normal map).
- Cloud, fog, and atmospheric occlusion, ensuring natural integration with skybox colors.

These materials inherit from the base skybox materials, maintaining proper cloud occlusion and sky color blending.

# Environment Profiles

The Time of Day System uses scriptable objects to define environment profiles. Each profile contains nested classes grouping relevant settings. Below is an overview of the main configurable elements:

![](./Images/TimeOfDay/Fig2.png)

**Post Process Profile**

- Allows custom post-processing settings per environment.
- Supports smooth transitions between profiles.
- Uses a default profile if none is assigned.

**Ocean Settings**

- Defines the current ocean state.
- Uses a specific ocean material with the Ocean Shader (refer to the Ocean System documentation for details).

**Skybox Material**

- Defines the skybox shader used to render the sky.

## Sun and Celestial Object Settings

**Sun Settings**

- Intensity – Controls directional light strength.
- Filter – Adjusts directional light color.
- Rotation – Controls sun orientation (X and Y components have a noticeable effect).
- Sun Disk Material – Must use a skybox shader with Is Celestial Object enabled.
- Angular Diameter – Controls the sun's apparent size (real-world value ~0.52, but adjustable for visuals).

**Moon/Secondary Celestial Object**

- Render Secondary Celestial Object – Enables rendering of a secondary disk (e.g., the moon).
- Secondary Object Rotation – Similar to the sun, but controls the secondary celestial body's orientation.
- Secondary Object Material – Configured similarly to the sun.
- Angular Diameter – Adjusts the moon’s apparent size (real-world value ~0.5 degrees, but tweakable for aesthetics).

## Fog and Wind Settings

**Fog Settings**

- Fog Color – Defines the full-density fog color.
- Density – Controls fog thickness via an exponential density function.
- Underwater Fog Color – Specific to underwater environments (used in Beat 7).
- Underwater Tint & Distance – Controls underwater color blending and fade distance.

**Wind Settings**

- Wind Yaw (Horizontal Angle) – Affects ocean movement and sail interactions.
- Wind Pitch (Vertical Angle) – Influences sail behavior and ocean wind speed calculations.

## Gradient Ambient & Precomputed Lighting

**Gradient Ambient Settings**

- Used with the Environment System to apply ambient lighting based on a gradient rather than the skybox.

**Environment Data Optimization**

- Stores precomputed ambient lighting from the skybox to reduce runtime calculations, optimizing performance.

## Conclusion

The Time of Day System in NorthStar is a highly optimized tool for dynamic environment control in mobile VR. By focusing on direct parameter manipulation, precomputed values, and scriptable environment profiles, it achieves realistic time-based transitions while minimizing GPU load.

This structured approach ensures high customization, allowing seamless environmental shifts and efficient real-time iteration during development.

### Relevant Files
- [EnvironmentSystem.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/EnvironmentSystem.cs)
- [SkyboxUpdater.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/SkyboxUpdater.cs)
- [SunDiskRenderer.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/SunDiskRenderer.cs)
- [EnvironmentProfile.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Water/EnvironmentProfile.cs)
- [RainController.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/RainSystem/RainController.cs)
- [RainData.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/RainSystem/RainData.cs)
- [UnderwaterEnvironmentController.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/UnderwaterSystem/UnderwaterEnvironmentController.cs)
- [UnderwaterEnvironmentData.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/UnderwaterSystem/UnderwaterEnvironmentData.cs)
- [WindController.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/WindSystem/WindController.cs)
- [WindData.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/WindSystem/WindData.cs)
