// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    /// <summary>
    /// Changes an objects parent on trigger
    /// </summary>
    [MetaCodeSample("NorthStar")]
    public class ChangePlayerParentOnTeleport : MonoBehaviour
    {
        [SerializeField] private Transform m_player, m_parent;
        public void Trigger()
        {
            m_player.parent = m_parent;
        }
    }
}
