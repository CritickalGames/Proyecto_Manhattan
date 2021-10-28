using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [System.NonSerialized]public static AudioManager aM;
    private List<Sound> toResume = new List<Sound>();
    public Sound[] sounds;

    void Awake()
    {
        if (aM != null)
            Destroy(this.gameObject);
        else
            aM = this;
        DontDestroyOnLoad(this);

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.group;
        }
    }
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null || sound.source.isPlaying == true)
            return;
        foreach (Sound resume in toResume)
            if (sound == resume)
            {
                sound.source.UnPause();
                toResume.Remove(resume);
                return;
            }
        sound.source.Play();

    }
    public void Pause(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null || sound.source.isPlaying == false)
            return;
        toResume.Add(sound);
        sound.source.Pause();
    }
}
