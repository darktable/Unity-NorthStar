# Full Body Tracking

NorthStar supports full-body tracking using the [Meta Movement SDK](https://developers.meta.com/horizon/documentation/unity/move-overview/). The SDK provides a solid foundation, and we have implemented additional logic to meet the project's needs.

![](./Images/FullBodyTracking/Fig1.png)

## Meta Movement SDK Setup

To set up body tracking, follow these steps:
- Right-click the player rig GameObject.
- Select the body tracking setup option.

![](./Images/FullBodyTracking/Fig0.png)

This process configures the RetargetingLayer component and the FullBodyDeformationConstraint, which work effectively out of the box.

## Calibration

**Avatar Scaling**

After the player calibrates their height in the menu, we scale the player avatar to match. We adjust the player rig based on the initial height versus the player's height, accounting for any height offset.

#### Relevant Files
- [PlayerCalibration.cs](../Assets/NorthStar/Scripts/Player/PlayerCalibration.cs)

**Seated vs Standing**

NorthStar supports both seated and standing play. We added an offset to the player rig to ensure the player appears standing in the game, even when seated. This offset is calculated from the difference between the player's calibrated standing and seated height, adjustable in the menu.

The full-body tracking system does not support manual offsets, causing the player rig to crouch regardless of skeleton adjustments. To address this, we use only upper body tracking and manage leg placement ourselves.

**Custom Leg Solution**

![](./Images/FullBodyTracking/Fig2.png)

Initially, we used the built-in full-body tracking leg solution from the Movement SDK, which worked for standing play but not for seated play. We developed an alternative leg-IK solution using Unity's rigging package, providing similar functionality for foot placement, stepping, and crouching.

#### Relevant Files
- [BodyPositions.cs](../Assets/NorthStar/Scripts/Player/BodyPositions.cs)

## Postprocessors

**Physics Hands Retargeting**

Using physically simulated hands required a RetargetingProcessor to retarget the hands with IK. The CustomRetargetingProcessorCorrectHand is a modified version of the original from Meta, supporting hand rotation and position overrides.

#### Relevant Files
- [CustomRetargetingProcessorCorrectHand.cs](../Assets/NorthStar/Scripts/Player/CustomRetargetingProcessorCorrectHand.cs)

**Hands, Elbow, and Twist Bone Correction**

After retargeting the hand and arm bones, the elbow and twist bones may misalign. We introduced a processor to correct their orientation, keeping the elbow locked to one axis relative to the shoulder and distributing wrist rotation along the twist and wrist bones. This processor also aligns the tracked finger bones with the player's tracked hands.

#### Relevant Files
- [SyntheticHandRetargetingProcessor.cs](../Assets/NorthStar/Scripts/Player/SyntheticHandRetargetingProcessor.cs)

## Tracking Failure

Full-body tracking can fail in several cases, such as failing to initialize, especially during Oculus Link testing. When tracking failure is detected, we switch off the full-body rig and revert to standard floating ghost hands. We also attempt to restart body tracking when the headset wakes from sleep or is put back on.

A persistent bug remains where the player's body is squished into the floor without any SDK-reported tracking failure. The cause is currently unknown.

## Future Improvements

The full-body tracking implementation has issues that need improvement, such as slight alignment problems with the avatar's head and feet floating off the ground in seated mode.
