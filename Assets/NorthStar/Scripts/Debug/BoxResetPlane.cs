// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class BoxResetPlane : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {

            other.attachedRigidbody?.GetComponent<BoxReset>()?.ResetPos();
        }
    }
}
