using UnityEngine;
using System.Collections.Generic;

public class VictoryChecker : MonoBehaviour
{
    public List<GameObject> currentCollisions = new List<GameObject>();
    public ParticleSystem fireworks = null;

    public bool hasVictory = false;
    public int stickCount = 0;

    public AudioSource musicSource = null;
    public AudioSource victoryMusicSource = null;


    public AudioSource fireworksSource = null;

    private void OnTriggerEnter(Collider other)
    {
        currentCollisions.Add(other.gameObject);
        Debug.Log("LOG");
        if (!hasVictory)
            CheckVictory();
    }

    private void OnTriggerExit(Collider other)
    {
        currentCollisions.Remove(other.gameObject);
    }
    void CheckVictory()
    {
        foreach (GameObject gObject in currentCollisions)
        {
            if (gObject.tag == "Interactable")
                stickCount++;
        }
        if (stickCount >= 4 && !hasVictory)
        {
            HandleVictory();
            hasVictory = true;
        }
    }

    void HandleVictory()
    {
        fireworks.Play();
        StartCoroutine(AudioFade.FadeOut(1f, Mathf.SmoothStep));
        victoryMusicSource.Play();
        fireworksSource.Play();
        Debug.Log("VICTORY");

    }
}