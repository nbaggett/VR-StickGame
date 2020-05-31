using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poof : MonoBehaviour
{
    public GameObject poof = null;
    private bool poofed = false;
    private bool introPlayed = false;
    public bool outroPlayed = false;

    private AudioSource audioSource = null;
    public AudioClip poofSound = null;

    public AudioSource stumpMusicSource = null;

    public AudioClip stumpMusic = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (introPlayed && !audioSource.isPlaying && !outroPlayed)
        {
            outroPlayed = true;
            StartCoroutine(AudioFade.FadeOut(3f, Mathf.SmoothStep));
        }
    }

    public void OnPuff()
    {
        if (!poofed)
        {
            Instantiate(poof, this.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(poofSound, 0.5f);
            poofed = true;
            stumpMusicSource.clip = stumpMusic;
            stumpMusicSource.Play();
            Invoke("PlayIntro", 0.5f);
        }
    }

    private void PlayIntro()
    {
        audioSource.Play();
        introPlayed = true;
    }
}
