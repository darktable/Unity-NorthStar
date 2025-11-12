// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;
using UnityEngine.Playables;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class BlendshapeBehaviour : PlayableBehaviour
    {
        public SkinnedMeshRenderer SkinnedMeshRenderer;
        public int Index = 0;
        [Range(0, 100)]
        public float Intensity = 100.0f;
    }
}
