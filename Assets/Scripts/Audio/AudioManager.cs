using System;
using Core;
using ScriptableObjects;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoImmortal<AudioManager>
    {
        public AudioSO audioSo;
        public AudioSource musicAudioSource;
        public AudioSource oneShotsAudioSource;

        public void PlayOneShot(AudioName audioName)
        {
            try
            {
                var clip = audioSo.GetClip(audioName);
                oneShotsAudioSource.PlayOneShot(clip, 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void StopMusic()
        {
            musicAudioSource.Stop();
        }
        public void PlayMusic(AudioName audioName)
        {
            var clip = audioSo.GetClip(audioName);
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }
    }
}