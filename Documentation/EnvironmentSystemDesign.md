# Implementing Environment Transitions

## Overview

Scenes often need environment transitions to show changes in time, like moving from midnight to late morning. Each transition uses a new Environment Profile and can be either quick (0.01 seconds) or extended (5–50 seconds).

Most transitions happen during teleport warps, limiting the number of transitions per scene. We manage these transitions carefully to ensure smooth shifts between different weather and time profiles.

## Managing Transitions

Sometimes, we need duplicate Environment Profiles with slight variations, such as different cloud textures or ocean wind speeds, to ensure natural-looking transitions. This prevents clouds and oceans from appearing like an unnatural timelapse.

**Example Scenes:**

- Beat6 – Rapid time-of-day shifts within 5 teleport warps.
- Beat3and4 – Transitioning from a clear day to a stormy night.

## Environment Profile

Environment Profiles are data assets with several parameters that allow us to create various environmental, ocean, and weather changes throughout a scene. See below for a screenshot of the data asset.

![](./Images/EnvImplementation/Fig4.png)

## Beat6 Scene: Example of Environment Profile Transitions

Transitions in the Beat6 scene were challenging due to rapid time-of-day changes, which are difficult to convey in a few minutes and within 5 teleport warps. Beat6 required transitioning from the stormy dark weather of Beat5 to a late morning profile by the end of Beat6.

Beat6 uses 6 Environment Profiles, starting with one nearly identical to the stormy night profile that ends Beat5. Key changes were crucial for seamless transitions from the dark storm profile to late morning profiles in Beat6.

- The Skybox

  - To ensure seamless transitions, the skybox was updated so that cloud values matched the rest of the scene transitions. These include:

    - Cloud Offset
    - Cloud Scale
    - Cloud Speed
    - Cloud Direction

  - Drastic differences in these values can cause clouds to appear to lower or move unnaturally across the sky.

    - Different cloud textures can be used, but matching these values reduces unintended effects. Using the same cloud texture is preferable to minimize issues during longer transitions.

![](./Images/EnvImplementation/Fig6.gif)

- The Ocean

  - At the end of Beat5, the ocean is choppy with a large patch size and high wind speed. By the end of Beat6, the weather needs to be calmer and sunnier. The following were adjusted for seamless transitions:

    - Environment Profile Parameters:

      - Wind Speed
      - Directionality
      - Patch Size

        - Large changes in patch size were hidden behind instant transitions.

      - Ocean Material

        - Ocean Normal Map texture
        - Normals parameters
        - Smoothness
        - Displacement noise

Adjusting these parameters ensures consistency between Beat4 and Beat5's stormy weather while setting up Beat6 transitions smoothly as players teleport across the boat.

For nighttime and stormy profiles, we set the Sun Disk Material to the Moon material, so water reflections come from the moon instead of the sun.

- This requires instant transitions to swap the moon and sun.

  - A long transition would make the moon's position change rapidly like a timelapse.

  - We hide this in Beat6 between the second and third transitions, and this technique is also used in Beat3 and Beat4 scenes transitioning from a clear day to a stormy night.

To check transition flow, we can drag different environment profiles onto the **‘Target Profile’** on the Environment prefab in a scene, like Beat6, and adjust the Transition Time to determine which transitions need to be instant or long-form and their duration.

![](./Images/EnvImplementation/Fig5.gif)

We set up transitions using Unity Event Receivers on the Environment Profile prefab and assign them to Event Broadcasters on the Teleport Warps in the scene. Once Receivers are set up, we add the Environment Profile for the transition, its duration, and skybox updaters to ensure smooth transitions. See below for an example of Beat6 transitions on the Environment Prefab.

**Starting Environment Profile in Beat6:**

![](./Images/EnvImplementation/Fig2.png)

**Final Environment Profile in Beat6:**

![](./Images/EnvImplementation/Fig1.png)

**Example of the Beat6 Environment Transitions Setup in Scene**

![](./Images/EnvImplementation/Fig0.png)

![](./Images/EnvImplementation/Fig3.png)

# Summary of Things to Look Out For

### Sun and Moon Positions

- Higher or lower in the sky

  - Small position changes can be instant or long (5 - 30 seconds).

  - Large position changes can be instant or long (20 - 50 seconds).

    - NOTE: Large changes need significant time; otherwise, the sun/moon will move quickly and look unnatural.

- Swapping Moon and Sun

  - Must be done via an instant transition.

- Keep sun and moon changes on the X-axis only, moving up or down, not horizontally (Y-axis changes between scenes are acceptable if necessary).

### Ocean Patch Sizes

- Longer transitions require the same patch size, as it limits max wave size. Different patch sizes can cause visual inconsistencies during transitions, like noticeable shifting/scrolling as the ocean patch size changes.

### Same Skybox Cloud Values for Longer Transitions

- Cloud Offset should be consistent to avoid clouds moving up and down.

- Cloud Speed should be consistent to prevent noticeable scrolling as clouds shift.

- Cloud Scaling, like Cloud Speed, can cause noticeable scrolling if different between profiles.

  - NOTE: Consistent cloud values are necessary for extended transitions.

### Colours

- Managing colour changes between profiles can be challenging, especially with rapid time-of-day changes.

- Smaller changes in instant or short transitions (3 - 5 seconds) can help achieve smoother colour transitions over longer periods.

- In a Beat6 profile, when the moon was in the ‘sun’ position, I edited the Skybox ‘Cloud Back Color’ to appear more orange, typically a darker version of the ‘Cloud Sun Color’, to simulate the sun about to crest the ocean.

  - Tricks like this can help create transitions between very different profile setups.

- Experimenting with Post Processing profiles can also aid in smooth colour transitions between profiles.

### Relevant Files
- [EnvironmentSystem.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Environment/EnvironmentSystem.cs)
- [EnvironmentProfile.cs](https://github.com/meta-quest/Unity-UtilityPackages/tree/main/com.meta.utilities.environment/Runtime/Scripts/Water/EnvironmentProfile.cs)
