# Boat Movement

## Overview

In North Star, the boat is essential. It must move smoothly, turn, and interact with the ocean while allowing free player movement onboard. We explored several methods to achieve this.

## Movement Approaches

### True Movement

Initially, the boat was a kinematic rigid body controlled by scripting, with objects physically on it. This caused hand tracking and physics issues, so we abandoned this method.

### Move the World

We considered moving the environment around the boat instead of the boat itself. However, this complicated persistent objects outside the boat, so we did not pursue it.

### Fake Movement

We adopted a "fake movement" system. The boat's visual position updates before rendering and resets afterward. This prevents physics issues, avoids dragging objects with the boat, and eliminates the need to move the world (ocean, islands, sky, reflections, etc.). We developed helper functions for transforming between world space and "boat space."

#### Relevant File
- [Fake Movement](../Assets/NorthStar/Scripts/Ship/FakeMovement.cs)

## Rocking and Bobbing

To simulate ocean movement, we implemented a procedural noise system to make the boat rock and bob. The effect scales with boat speed, influenced by wind direction and sail angle. We tested a more realistic wave-height and momentum-based system but replaced it for better player comfort and direct control.

## Reaction Movement

We scripted specific boat movements for special events, like waves hitting the boat or creature attacks, using the timeline.

### Relevant Files
- [BoatMovementAsset](../Assets/NorthStar/Scripts/Wave/BoatMovementAsset.cs)
- [BoatMovementBehaviour](../Assets/NorthStar/Scripts/Wave/BoatMovementBehaviour.cs)
- [BoatMovementMixerBehaviour](../Assets/NorthStar/Scripts/Wave/BoatMovementMixerBehaviour.cs)
- [BoatMovementTrack](../Assets/NorthStar/Scripts/Wave/BoatMovementTrack.cs)
- [WaveControlAsset](../Assets/NorthStar/Scripts/Wave/WaveControlAsset.cs)
- [WaveControlBehaviour](../Assets/NorthStar/Scripts/Wave/WaveControlBehaviour.cs)
- [WaveControlTrack](../Assets/NorthStar/Scripts/Wave/WaveControlTrack.cs)

![](./Images/BoatMovement/Fig0.png)

## Comfort Settings

To ensure player comfort, procedural motion is directly controlled, allowing magnitude adjustments. A comfort option locks the horizon angle, keeping the player upright as if they had perfect "sea legs." This stabilizes the horizon and prevents motion sickness.

```cs
var boatRotation = BoatController.Instance.MovementSource.CurrentRotation;
CameraRig.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Inverse(boatRotation), GlobalSettings.PlayerSettings.ReorientStrength) * CameraRig.parent.localRotation;
```
*(from [BodyPositions.cs](../Assets/NorthStar/Scripts/Player/BodyPositions.cs#L241-L242))*

![](./Images/BoatMovement/Fig1.png)
