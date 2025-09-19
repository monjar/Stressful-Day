using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "AudioSO", menuName = "ScriptableObjects/AudioSO", order = 1)]
    public class AudioSO : ScriptableObject
    {
        public List<AudioEntry> audioEntries;

        public AudioClip GetClip(AudioName audioName)
        {
            return audioEntries.First(audio => audio.name == audioName).clip;
        }
    }

    [Serializable]
    public struct AudioEntry
    {
        public AudioClip clip;
        public AudioName name;
    }
}