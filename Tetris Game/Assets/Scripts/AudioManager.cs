using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] _sounds;

    private void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
            sound.source.pitch = sound.pitch;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PauseSound(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.source.Pause();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
