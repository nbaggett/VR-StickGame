using System.Collections;
using System;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    public static AudioSource source = null;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public static IEnumerator FadeIn(float fadingTime, Func<float, float, float, float> Interpolate)
    {

        source.Play();
        source.volume = 0;

        float resultVolume = 0.212f;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            source.volume = Interpolate(0, resultVolume, t);
            yield return null;
        }

        source.volume = resultVolume;
    }

    public static IEnumerator FadeOut(float fadingTime, Func<float, float, float, float> Interpolate)
    {
        float startVolume = source.volume;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            source.volume = Interpolate(startVolume, 0, t);
            yield return null;
        }

        source.volume = 0;
        source.Pause();
    }
}
