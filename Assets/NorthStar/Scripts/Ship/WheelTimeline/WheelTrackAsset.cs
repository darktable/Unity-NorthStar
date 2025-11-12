// Copyright (c) Meta Platforms, Inc. and affiliates.

using Meta.XR.Samples;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace NorthStar
{
    [MetaCodeSample("NorthStar")]
    [TrackClipType(typeof(WheelPlayableAsset))]
    [TrackBindingType(typeof(WheelController))]
    public class WheelTrackAsset : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<WheelMixerBehaviour>.Create(graph, inputCount);
        }
    }
}
