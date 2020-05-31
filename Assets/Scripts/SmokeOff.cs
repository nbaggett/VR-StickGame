using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeOff : MonoBehaviour
{
    bool triggered = false;
    ParticleSystem ps = null;
    public AudioSource music = null;
    public AudioClip intenseJambo = null;
    public Poof poof = null;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!triggered && poof.outroPlayed)
        {
            ps.Stop();
            music.clip = intenseJambo;
            StartCoroutine(AudioFade.FadeIn(3f, Mathf.SmoothStep));
            triggered = true;
            Invoke("KillMe", 10f);
        }
    }

    private void KillMe()
    {
        Destroy(this.gameObject);
    }
}
