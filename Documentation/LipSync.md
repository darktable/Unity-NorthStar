# North Star’s Implementation of ULipSync

To support fully voiced NPC dialogue, NorthStar needed an efficient lip-syncing solution that minimized reliance on animators. The team chose [ULipSync](https://github.com/hecomi/uLipSync) for its familiarity, strong narrative control, customization, and ease of use.

## ULipSync Setup

ULipSync processes lip-sync data in three ways:

1. **Runtime processing** – Analyzes audio dynamically.
2. **Baking into scriptable objects** – Stores data for reuse.
3. **Baking into animation clips** – Prepares animations for timeline use.

Due to CPU constraints and narrative timelines, baking data into animation clips was the best approach.

![](./Images/LipSync/Fig2.png)

## Phoneme Sampling & Viseme Groups

ULipSync maps phonemes (smallest speech components) to viseme groups (facial animation controls).

- English has **44 phonemes**, but not all are needed for lip-syncing.
- **Plosive sounds** (e.g., "P" or "K") are hard to calibrate and may not impact the final animation significantly.
- Stylized models need fewer viseme groups than realistic ones, sometimes only vowels.

To ensure flexibility, we recorded all 44 phonemes for each voice actor, allowing system refinement later.

![](./Images/LipSync/Fig0.png)

## Challenges in Phoneme Sampling

Not all phonemes were sampled perfectly. Issues included:

- Regression effects, where certain phonemes worsened results.
- Lack of matching viseme groups, making some phonemes irrelevant.
- Volume inconsistencies, causing some sounds to be too quiet for accurate sampling.

To improve accuracy, we documented problematic phonemes for future improvements and considered additional recordings.

## Ensuring Realistic Lip-Sync

Automated lip-syncing often results in excessive mouth openness. Realistic speech involves frequent mouth closures. To address this:

- We referenced real-life speech patterns.
- Animators provided feedback to refine mouth movement accuracy.

## Final Implementation

Each voice line was baked with a pre-calibrated sample array, storing blend shape weights per NPC. This per-character approach worked due to a limited NPC count, but a more generalized system is needed for larger projects.

![](./Images/LipSync/Fig1.png)

### Relevant Files

- [NpcController.cs](../Assets/NorthStar/Scripts/NPC/NpcController.cs)
- [NpcRigController.cs](../Assets/NorthStar/Scripts/NPC/NpcRigController.cs)
