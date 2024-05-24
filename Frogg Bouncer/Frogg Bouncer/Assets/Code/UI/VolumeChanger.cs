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

        public void MusicMuteToggle(bool muted)
        {
            if (muted)
            {
                //AudioListener.volume = 0;
                audioMixer.SetFloat("MusicVolume", -80);
            }
            else
            {
            audioMixer.SetFloat("MusicVolume", 0);
            //AudioListener.volume = 1;
            }

        }

    public void SFXMuteToggle(bool muted)
    {
        if (muted)
        {
            //AudioListener.volume = 0;
            audioMixer.SetFloat("SFXVolume", -80);
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", 0);
            //AudioListener.volume = 1;
        }

    }
}