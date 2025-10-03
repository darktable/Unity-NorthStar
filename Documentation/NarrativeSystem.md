# Narrative Sequencing

## Introduction

NorthStar’s Narrative Sequencing System is a custom, lightweight framework for efficiently scripting game events. It primarily supports linear structures but also accommodates branching narratives and multiple task options.

## Structure

The system uses Task Sequences, which function as game chapters. Each Task Sequence includes one or more Task Definitions. A sequence completes when all its tasks are marked as done. The Task Manager oversees progression, starting the next sequence once the previous one finishes.

Task sequences are managed via the **Task Manager object**: ..Assets/Resources/NarrativeSequence/Task Manager.asset

## Tasks

NorthStar typically assigns a new Task Sequence to each teleport point, with actions at that location divided into Task Definitions.

For example, in the B2_S4 Task Sequence, where the player helps Audrey and Bessie move barrels, tasks trigger sequentially based on prerequisites. However, multiple tasks can be active simultaneously, allowing player-driven events.

**Task Handler Component**

A Task Handler must be present in the scene for each potential active task. It includes the following properties:
- **Task ID** – Manages the specific task.
- **Player Transform** – Tracks player position (typically PlayerV2).
- **Player Gaze Camera** – Used for look direction checks.
- **Complete When** – Determines if the task completes when all or any conditions are met.

**Completion Conditions**

A task completes when at least one condition is met. If no condition is set, it completes instantly. Conditions include:
- **Hit Targets** – Requires hitting listed harpoon targets (e.g., Kraken fight).
- **Reeled Targets** – Used for retrieving harpoon-hooked objects.
- **Wait for Event Broadcast** – Triggers upon receiving a specified event.
- **Rope Is Spooled** – Checks rope length between Min/Max values.
- **Rope Is Tied** – Completes when rope is secured.
- **Proximity** – Player reaches a set distance from the target.
- **Look at Target** – Player faces the target within a set angle.
- **Time Delay** – Completes after a specified duration.
- **Wait for Event** – Triggered by a Unity Event on a referenced object.
- **Wait for Animation** – Completes when an animation finishes.
- **Wait for Playable** – Completes when a timeline ends.

**Event Triggers**

Tasks can trigger events at different stages:
- **On Task Started** – Fires when the task begins.
- **On Task Completed** – Fires upon completion.
- **On Reminder** – Fires at intervals while active.

**Reminder Interval:** If set, triggers On Reminder actions every X seconds. (Set to 0 to disable.)

**Scripted Narrative Sequences**

Most narrative events in NorthStar are driven by Timelines using Playable Directors and Timeline Signals. Each Task Manager typically has:
- **Playable Director** – Plays Timeline assets.
- **Dialogue Player** – Controls voice-over sequences.

When a task starts, its Dialogue Player begins playback, triggering the Playable Director. Even non-dialogue sequences follow this structure for consistency.

**Best Practice:** Set Playable Director Update Method to DSP Clock to prevent audio desynchronization.

## Creating New Tasks

**To add a new Task Sequence:**
1. Duplicate an existing sequence (..Assets/NorthStar/Data/Task Sequences) or create one via Create > Data > Narrative Sequencing > Task Sequence.
2. Add Task Definitions inside the sequence.
3. Register the sequence in the Task Manager:
   - Open the Task Manager object.
   - Add the new Task Sequence to the Sequences list (in order).
   - Refresh the Task Manager from the context menu.

**Setting Task Prerequisites**
- Use the dropdown menu to set a prerequisite or click Add Preceding Task ID for automatic ordering.
- Leave at least one task with no prerequisite to prevent game progression from stalling.
- If you see an Undefined ID error, ensure the Task Sequence is registered in the Task Manager.

**Adding Tasks to a Scene**
1. Place a GameObject with a Task Handler component.
2. Assign the Task ID and required settings.
3. Ensure a Game Flow Controller is in the scene (see Game Flow prefab).
4. If using multi-scene sequences, set the First Task to the earliest sequence in that scene.

**Testing & Debugging**

For quick narrative testing:
- **Before Play Mode**: Set First Task to the desired sequence.
- **During Play Mode**: Select a Task Handler and click "Start narrative from this sequence" to skip forward.

**Note:** Some sequences rely on prior events. Skipping ahead may cause characters/objects to remain incorrectly active or disabled.
