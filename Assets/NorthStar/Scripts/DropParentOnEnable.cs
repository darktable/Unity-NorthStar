// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    /// <summary>
    /// Removes an objects parent when its enabled after the boat syncs its movement
    /// </summary>
    [MetaCodeSample("NorthStar")]
    public class DropParentOnEnable : MonoBehaviour
    {
        private void Drop()
        {
            transform.parent = null;
        }
        private void OnEnable()
        {
            BoatController.Instance.MovementSource.OnSync += Drop;
        }

        private void OnDisable()
        {
            BoatController.Instance.MovementSource.OnSync -= Drop;
        }
    }
}
