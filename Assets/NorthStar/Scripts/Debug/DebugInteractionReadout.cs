// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using TMPro;
using UnityEngine;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class DebugInteractionReadout : MonoBehaviour
    {
        public BaseJointInteractable<float> Interactable;
        public TMP_Text Text;

        public void Update()
        {
            Text.text = Interactable.Value.ToString("0.00");
        }
    }
}
