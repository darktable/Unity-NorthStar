# Rope System

NorthStar's development prioritized creating a realistic rope simulation system. We achieved this by combining Verlet integration for simulation with anchoring constraints for interactive rope behavior.

## Verlet Simulation

![](./Images/RopeSystem/Fig3.png)

Verlet integration simplifies rope physics by focusing on point mass calculations. Each point mass stores only its current and previous positions, assuming a unit mass. Constraints are enforced by adjusting point positions to maintain the rope's rest length.

This approach simulates how ropes visually react to player interactions. However, forces only travel from the player to the rope, not bidirectionally.

To enhance stability, we implemented a binding system. This system allows specific nodes in the rope simulation to act as kinematic points, unaffected by forces or constraints. We optimized the Verlet simulation using Burst Jobs to support many nodes and high iteration counts for constraint solving.

### Relevant Files
- [BurstRope.cs](https://github.com/meta-quest/Unity-UtilityPackages/blob/main/com.meta.utilities.ropes/Runtime/Rope/VerletRope/BurstRope.cs)

## Anchoring Constraints

We developed an anchoring system to let players interact with and be constrained by ropes. This system enables ropes to wrap around static objects, dynamically creating bends that constrain the Verlet simulation.

**How Anchors Work:**

- When the rope encounters an obstacle, a new anchor is created based on normal direction, bend angle, and bend direction.
- If the player holds the rope, the system calculates the rope length between two anchors and applies a configurable joint with a linear limit to prevent overstretching.
- Slack and spooled rope are managed, allowing:
  - Loose rope to be pulled through when tightening.
  - Extra rope to be spooled out, such as for sail controls.
- If the player exerts enough force, the rope can slip, allowing hands to slide along it like a real rope.
- The number of bends, slack amount, and spooled rope length can trigger events when the rope is pulled tight or tied.

### Relevant Files
- [RopeSystem.cs](https://github.com/meta-quest/Unity-UtilityPackages/blob/main/com.meta.utilities.ropes/Runtime/Rope/RopeSystem.cs)

## Tube Rendering

![](./Images/RopeSystem/Fig2.png)

To render ropes visually, we developed a tube renderer that:

- Uses rope nodes to generate a lofted mesh along a spline.
- Supports subdivisions for added detail.
- Adds indentation and twisting for realism.
- Utilizes normal mapping for enhanced depth and texture.
- Is optimized using Burst Jobs for efficient performance.

### Relevant Files
- [TubeRenderer.cs](https://github.com/meta-quest/Unity-UtilityPackages/blob/main/com.meta.utilities.ropes/Runtime/Rope/VerletRope/TubeRenderer.cs)

## Collision Detection

![](./Images/RopeSystem/Fig1.png)

Efficient rope collision detection was challenging. We used `Physics.ComputePenetration()` to detect interactions between Verlet nodes and nearby level geometry. However, two key issues arose:
1. `ComputePenetration` is incompatible with Jobs or Burst, so collision detection had to occur on the main thread after Verlet simulation.
2. Single collision checks per frame caused phasing issues, as ropes passed through objects when nodes were forced apart.

**Optimizations for Better Collision Detection:**

To address these issues, we:
- Split the rope simulation into multiple sub-steps, running a collision check after each sub-step.
- Forced the first job to complete immediately, allowing for early collision checks in the frame.
- Performed the second sub-step during the frame, resolving it in `LateUpdate()` for increased stability.
- Used `SphereOverlapCommand` in a Job to efficiently gather potential collisions without stalling the main thread.

### Relevant Files
- [BurstRope.cs](https://github.com/meta-quest/Unity-UtilityPackages/blob/main/com.meta.utilities.ropes/Runtime/Rope/VerletRope/BurstRope.cs)

## Editor Workflow

![](./Images/RopeSystem/Fig4.png)

We streamlined the process of adding and configuring ropes in scenes with an intuitive editor workflow:
- Start with the `RopeSystem` prefab.
- Edit the included spline to define the desired rope shape.
- Use context menu options in the `RopeSystem` component to:
  - Set up nodes.
  - Define the rope's total length.
- Run the simulation in a test scene and allow the rope to settle naturally.
- Copy the anchor points and `BurstRope` nodes from the simulation back into the editor.
- Finalize the rope setup for use in live gameplay.

![](./Images/RopeSystem/Fig0.png)

### Relevant Files
- [RopeSystem.prefab](../Assets/NorthStar/Prefabs/RopeSystem.prefab)

## Conclusion

By combining Verlet simulation with dynamic anchoring constraints, we created a realistic and performant rope system for NorthStar. The use of Burst Jobs, tube rendering, and multi-step collision detection balanced realism, interactivity, and performance. The editor workflow streamlined development, enabling efficient iteration and fine-tuning of rope behaviors.
