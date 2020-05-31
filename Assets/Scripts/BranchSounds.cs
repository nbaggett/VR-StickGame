using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSounds : MonoBehaviour
{
    AudioSource audioSource = null;
    public AudioClip bonk = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            audioSource.PlayOneShot(bonk, 0.2f);
        }
        else
            audioSource.PlayOneShot(bonk);
    }
}
