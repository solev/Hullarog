using UnityEngine;
using System.Collections;
using System;

public class IntroSpeak : MonoBehaviour
{

    public AudioClip[] audios;
    int idx = 0;

    AudioSource audio;
    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        PlayAudio();
    }

    private void PlayAudio()
    {
        audio.clip = audios[idx];
        audio.Play();
        StartCoroutine(AudioLenght(audio.clip.length));
    }

    IEnumerator AudioLenght(float sec)
    {
        yield return new WaitForSeconds(sec);
        idx++;
        if (idx < audios.Length)
            PlayAudio();
    }
}
