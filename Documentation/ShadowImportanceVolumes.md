## Shadow Importance Volumes

Unity ensures the full viewport, up to the configured maximum shadow distance, is covered in the shadowmap. For many games and platforms, this balances texel density with full scene coverage. However, in this game, set on a ship's surface with the Meta Quest's impressive FOV, it's inefficient to assign shadow coverage to areas like the sky and ocean. Shadow Importance Volumes solve this by letting designers specify which surfaces need shadowmap coverage, allowing other surfaces to remain outside the shadow volume.

![](./Images/ShadowImportance/Fig1.png)

ShadowImportanceVolumes require changes to URP to adjust the shadow projection matrix and shadow distance before shadow casting.

### Relevant Files
- [ShadowImportanceVolume.cs](../Assets/NorthStar/Scripts/Utils/ShadowImportanceVolume.cs)
- [ShadowUtils.cs](../Packages/com.unity.render-pipelines.universal/Runtime/ShadowUtils.cs)
- [MainLightShadowCasterPass.cs](../Packages/com.unity.render-pipelines.universal/Runtime/Passes/MainLightShadowCasterPass.cs)

## Using Shadow Importance Volumes

Shadow Importance Volumes activate automatically when any enabled volume intersects the camera. To create a new volume, make an empty Game Object, select Add Component, and choose ShadowImportanceVolume. Reposition and scale the volume as needed. Only the surface that can receive shadows needs to be inside a volume, not the entire object or mesh.

![](./Images/ShadowImportance/Fig2.png)

If all volumes are disabled or don't intersect the camera, shadows revert to Unity's default implementation.

## How They Work

First, all volumes in the game world intersect with the active camera's frustum. Any volume or part outside the camera frustum is ignored. The intersection of volumes and frustum forms a hull for shadowcasting. This hull is transformed into shadow space, and an adjustment matrix is generated to fit the shadowmap more tightly around active areas.

Unity camera frustum with a far distance of 20:

![](./Images/ShadowImportance/Fig3.png)

Comparison of shadowmap between default and with volumes on the ship deck. The very wide FOV forces Unity to size the shadowmap inefficiently:

![](./Images/ShadowImportance/Fig4.png)
