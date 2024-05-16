using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

    public class VolumeChanger : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public void SetMainVolume(float volume)
        {
            audioMixer.SetFloat("MainVolume", volume);
            Debug.Log(volume);
        }
        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", volume);
        }
        public void SetSFXVolume(float volume)
        {
            audioMixer.SetFloat("SFXVolume", volume);
        }

        public void MuteToggle(bool muted)
        {
            if (muted)
            {
                AudioListener.volume = 0;
            }
            else
            {
                AudioListener.volume = 1;
            }

        }
    }