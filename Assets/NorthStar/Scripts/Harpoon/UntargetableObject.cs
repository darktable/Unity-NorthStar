// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    /// <summary>
    /// Prevents an object from being hit by the harpoon
    /// </summary>
    [MetaCodeSample("NorthStar")]
    [RequireComponent(typeof(CapsuleCollider))]
    public class UntargetableObject : MonoBehaviour
    {
        public float Radius;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}
