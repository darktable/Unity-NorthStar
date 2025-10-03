# Visibility Set System

## Overview

![](./Images/VisibilitySet/Fig0.png)

During development, we noticed that only a small part of the boat and its surroundings was fully visible at any time. Elements like the boat’s interior and the island in Beat 2 were often only partially visible or needed lower detail levels.

Typically, LOD (Level of Detail) groups handle this, but in VR, LOD transitions were too noticeable due to frequent head movements. We couldn't set precise LOD transition points to eliminate this effect, so we created a custom solution. This system dynamically manages object visibility and LOD activation, enhancing performance throughout the experience.

This solution is the Visibility Set System. It allows designers to define and manage named object groups within a scene, enabling and disabling them as needed.

## How the Visibility Set System Works

1. **Named Visibility Sets**

    - Designers can define multiple "sets" in a scene, grouping related objects.
    - These sets can be enabled or disabled dynamically, optimizing rendering and performance.

2. **Multi-Level Detail Management**

    - Each set supports multiple LOD levels, including a fully disabled state.
    - Sets can be linked, allowing objects to adjust visibility based on narrative events or scene interactions.

3. **Example: Boat Cabin System**

    - The boat is divided into multiple visibility sets, including one for the cabin interior.
    - The cabin door has two visibility states:
        - Closed: Activates the cabin_interior_door_closed set, applying a high LOD factor (9999) to disable unnecessary objects.
        - Open: A narrative trigger switches to the cabin_interior_door_open set, revealing parts of the island visible through the door.

4. **Seamless Transitions**

    - Visibility transitions occur during teleports, masking performance spikes from activating objects.
    - This approach was crucial in Beat 2, where rendering the cabin interior, boat exterior, port, and island simultaneously was too costly.

5. **Performance Optimizations**

    - Entire level sections, including physics, logic, and scripts, are disabled when not needed, improving CPU and GPU performance.
    - On scene load, all objects are enabled for the first frame to ensure Awake() / Start() methods execute properly, preventing lag spikes when enabling large scene sections later.

### Relevant Files

- [ActiveVisibilitySetLevelData.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/ActiveVisibilitySetLevelData.cs)
- [VisibilitySet.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/VisibilitySet.cs)
- [VisibilitySetData.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/VisibilitySetData.cs)

## Editor Support

Managing multiple overlapping sets was challenging. To assist designers and artists:

- We implemented a context menu tool to convert LOD groups into Visibility Set items automatically.
- The tool could rearrange object hierarchies if the original LOD group was misconfigured.

This allowed artists to use familiar workflows while benefiting from the system’s optimizations.

## Further Improvements

Opportunities exist to improve the Visibility Set System, especially in editor usability:

- Custom inspectors for better visualization of active/culled objects.
- In-editor previews to show which objects are disabled under specific set conditions.

These enhancements would make the system more intuitive and reduce iteration time in complex scenes.

## Conclusion

The Visibility Set System significantly optimized performance and rendering efficiency in NorthStar, especially for VR gameplay. By dynamically managing visibility and LOD levels, we eliminated LOD popping, improved scene transitions, and enabled large-scale optimizations without sacrificing visual quality.

With further editor enhancements, the system could become even more powerful, offering better visual debugging tools and workflow improvements for designers.
