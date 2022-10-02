using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;

  void Awake()
  {
    foreach (Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;

      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
      s.source.loop = s.loop;
    }
  }

  void Start()
  {
    Play("Theme");
  }

  public void Play(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s == null)
    {
      Debug.LogWarning("Sound: " + name + " not found");
      return;
    }

    s.source.Play();
  }

  //this addition to the code was made by me, the rest was from Brackeys tutorial
  public void Stop(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);

    s.source.Stop();
  }
}