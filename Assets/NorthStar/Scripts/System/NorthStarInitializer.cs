// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.Utilities.Environment;
using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class NorthStarInitializer : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            ProfilingSystem.AddBooleanCommand("sphere_mask_enabled", enabled =>
            {
                SphereMaskRenderer.Enabled = enabled;
                Debug.Log($"sphere_mask_enabled = {SphereMaskRenderer.Enabled}");
            });
        }
    }
}
