using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Singleton;
    public Sound[] sounds;

    void Awake()
    {
        Singleton = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string _name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        s.source.PlayOneShot(s.clip);
    }

    public void PlayLoop(string _name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        s.source.Play();
    }

    public void StopLoop(string _name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        s.source.Stop();
    }

    public void PauseLoop(string _name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        s.source.Pause();
    }

    public void ResumeLoop(string _name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        s.source.UnPause();
    }
}
