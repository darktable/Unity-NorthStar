// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class PoolTest : MonoBehaviour
    {
        public EffectAsset EffectAsset;
        private void Update()
        {
            EffectAsset.Play(transform.position, transform.rotation);
        }
    }
}
