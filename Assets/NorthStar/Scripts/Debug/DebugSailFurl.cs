// Copyright (c) Meta Platforms, Inc. and affiliates.
using Meta.Utilities.Environment;
using Meta.XR.Samples;
using Oculus.Interaction;
using UnityEngine;

namespace NorthStar.DebugUtilities
{
    [MetaCodeSample("NorthStar")]
    public class DebugSailFurl : ButtonGroup
    {
        [SerializeField]
        private ClothAnimator m_sail;
        private const float FURL_INPUT_SPEED = 1;

        protected override void DecrementOnStateChange(InteractableStateChangeArgs args)
        {
            if (m_sail != null)
            {
                m_sail.FurlInput = args.NewState == InteractableState.Select ? -1 : 0;
            }
        }

        protected override void IncrementOnStateChange(InteractableStateChangeArgs args)
        {
            if (m_sail != null)
            {
                m_sail.FurlInput = args.NewState == InteractableState.Select ? 1 : 0;
            }
        }
    }
}