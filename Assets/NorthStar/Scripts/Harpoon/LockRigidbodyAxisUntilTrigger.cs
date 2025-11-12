// Copyright (c) Meta Platforms, Inc. and affiliates.
using Meta.Utilities;
using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    /// <summary>
    /// Prevents horizontal movement of a rigidbody until its released
    /// </summary>
    [MetaCodeSample("NorthStar")]
    public class LockRigidbodyAxisUntilTrigger : MonoBehaviour
    {
        [SerializeField, AutoSet] private Rigidbody m_body;
        private void Start()
        {
            m_body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        [ContextMenu("Test")]
        public void Release()
        {
            m_body.constraints = RigidbodyConstraints.None;
        }
    }
}
