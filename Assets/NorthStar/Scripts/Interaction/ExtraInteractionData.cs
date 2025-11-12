// Copyright (c) Meta Platforms, Inc. and affiliates.
using Meta.Utilities;
using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    /// <summary>
    /// Provides more information about an interaction if neccecary
    /// </summary>
    [MetaCodeSample("NorthStar")]
    public class ExtraInteractionData : MonoBehaviour
    {
        public LineSegment LineSegment;
        public bool FreeRotation = false;
    }
}
