using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [System.NonSerialized]public static AudioManager aM;
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
    void Start()
    {
        Play("MenuMusic");
        Play("Noise");
    }
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
            return;
        sound.source.Play();
    }
}
