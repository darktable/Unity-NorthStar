// Copyright (c) Meta Platforms, Inc. and affiliates.
using Meta.Utilities.Environment;
using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    /// <summary>
    /// Sets up a camera as a profiling camera, used in profiling mode for benchmark and performance tuning
    /// </summary>
    [MetaCodeSample("NorthStar")]
    public class ProfilingCamera : MonoBehaviour
    {
        [field: SerializeField] public string Name { get; protected set; }
        [field: SerializeField] public VisibilitySetData VisibilitySet { get; protected set; }
        [field: SerializeField] public Camera Camera { get; protected set; }
    }
}
