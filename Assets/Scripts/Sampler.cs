using System;
using UnityEngine;

namespace Improv
{
    /// <summary>
    /// The most basic sampler imaginable
    /// </summary>
    [Serializable]
    public class Sampler
    {
        public AudioClip sound;

        private AudioSource audioSource_;
        private GameObject parent_;

        public void Init(GameObject parent)
        {
            parent_ = parent;
            audioSource_ = parent_.AddComponent<AudioSource>();
            audioSource_.clip = sound;
        }

        public void Play(int noteNumber, double dspTime)
        {
            audioSource_.pitch = Mathf.Pow(2, (noteNumber - 60) / 12.0f);
            audioSource_.PlayScheduled(dspTime);
        }
    }
}