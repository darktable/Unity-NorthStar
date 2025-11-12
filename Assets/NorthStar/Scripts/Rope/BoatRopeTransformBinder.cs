// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.Utilities.Ropes;
using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class BoatRopeTransformBinder : RopeTransformBinder
    {
        public bool UseBoatSpace;

        protected override Vector3 GetPosition() =>
            UseBoatSpace ? BoatController.WorldToBoatSpace(base.GetPosition()) : base.GetPosition();
    }
}
