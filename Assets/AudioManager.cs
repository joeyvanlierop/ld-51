using System;
using System.Collections;
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
      s.nextSound = "ThemeLoop";
    }

    Play("ThemeStart");
  }

  IEnumerator WaitForSongEnd(Sound s)
  {
    Debug.Log("Test1");
    yield return new WaitUntil(() => !s.source.isPlaying && s.source.time == 0);
    Debug.Log("Test2");
    Play(s.nextSound);
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
    if (s.nextSound != null)
      StartCoroutine(WaitForSongEnd(s));
  }

  //this addition to the code was made by me, the rest was from Brackeys tutorial
  public void Stop(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);

    s.source.Stop();
  }
}