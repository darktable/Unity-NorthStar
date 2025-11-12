// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine.Playables;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    [System.Serializable]
    public class WheelPlayableBehaviour : PlayableBehaviour
    {
        public float OverrideValue = 0;
        public bool Override = false;
    }
}
