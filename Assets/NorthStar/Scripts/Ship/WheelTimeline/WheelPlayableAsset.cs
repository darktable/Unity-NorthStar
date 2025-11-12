// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;
using UnityEngine.Playables;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    public class WheelPlayableAsset : PlayableAsset
    {
        public WheelPlayableBehaviour Template;
        //public float OverrideValue;
        //public bool Override;
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<WheelPlayableBehaviour>.Create(graph, Template);
            //var behaviour = playable.GetBehaviour();
            //behaviour.OverrideValue = OverrideValue;
            //behaviour.Override = Override;
            return playable;
        }

    }
}
