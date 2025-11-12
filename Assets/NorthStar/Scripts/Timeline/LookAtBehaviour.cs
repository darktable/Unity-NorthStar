// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;
using UnityEngine.Playables;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class LookAtBehaviour : PlayableBehaviour
    {
        public NpcRigController NpcRigController;
        [Range(0, 1)]
        public float Weight = 1;
        public NpcRigController.IKRig Rig = NpcRigController.IKRig.Spine;
    }
}
