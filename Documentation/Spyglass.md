## Spyglass

In NorthStar, a telescope helps players see farther through dense fog. It uses an adjusted projection matrix to magnify objects and reduce fog density, allowing players to view distant objects. The telescope opens by pulling on the eyeglass, which extends its length.

![](./Images/Eyeglass/Fig1.png)

### Interactions

The telescope consists of two parts: a Front and a Back, each with a Rigidbody and multiple grab points. A joint connects these parts. A script monitors the separation between the Rigidbodies and updates the telescope's state (open or closed) based on this separation. When the state changes, the joint's target position adjusts to keep the telescope open or closed, and eyeglass rendering is enabled or disabled.

When the telescope is near the user’s eye, any jitter becomes noticeable and can make it difficult or uncomfortable to use. To stabilize, the telescope attaches to the nearest eye using another joint.

![](./Images/Eyeglass/Fig2.png)

### Rendering

Initially, the system rendered the world to a RenderTexture and composited it over the eyeglass. However, this method was too slow for real-time rendering. To reduce processing costs, a ScriptableRenderPass renders the visible world directly to the screen with adjusted projection matrices and viewport, stenciled to render only inside the eyeglass. It uses the same culling as the main scene.

First, the lens is drawn, writing a stencil bit to mark visible eyeglass pixels. This ensures the magnified world is only visible through the eyeglass, and any objects obscuring the eyeglass will also obscure the magnified world.

A screen rect is calculated for each eye based on the lens mesh bounds. Due to eye separation, the screen-space bounds of the lens can differ significantly for each eye. These rects combine to create a screen-space viewport containing the lens for both eyes, used to adjust each eye's projection matrix. The projection matrix is skewed by the relative telescope orientation multiplied by a “trackingFactor.”

Fog density is adjusted by overwriting the FogParams vector, and control passes to the base DrawObjectsPass to render the game scene before restoring everything to its initial state.

Finally, the lens is drawn again with a smooth transparent material to give it a glassy look.

### Relevant Files
- [Telescope.cs](../Assets/NorthStar/Scripts/Items/Telescope.cs)
- [ViewportRenderer.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.viewport-renderer/Scripts/ViewportRenderer.cs)
- [EyeStencilWrite.shader](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.viewport-renderer/Materials/EyeStencilWrite.shader)
