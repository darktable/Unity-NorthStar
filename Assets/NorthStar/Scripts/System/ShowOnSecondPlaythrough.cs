// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class ShowOnSecondPlaythrough : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(GameFlowController.Instance.GameCompleteOnce);
        }
    }
}
