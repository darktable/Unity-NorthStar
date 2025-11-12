// Copyright (c) Meta Platforms, Inc. and affiliates.
using System.Collections;
using Meta.XR.Samples;
using UnityEngine;
using UnityEngine.Events;

namespace NorthStar
{
    /// <summary>
    /// Trigger an event on a delay
    /// </summary>
    [MetaCodeSample("NorthStar")]
    public class DelayedEvent : MonoBehaviour
    {
        [SerializeField] private bool m_playOnStart;
        [SerializeField] private float m_time;

        [Space(10)]
        public UnityEvent Event;

        private void Start()
        {
            if (m_playOnStart)
            {
                Trigger();
            }
        }

        public void Trigger()
        {
            _ = StartCoroutine(DelayTrigger());
        }

        private IEnumerator DelayTrigger()
        {
            yield return new WaitForSeconds(m_time);

            Event.Invoke();
        }
    }
}
