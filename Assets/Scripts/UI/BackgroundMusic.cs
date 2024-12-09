using System;
using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] 
    private AudioSource source;

    private bool lastMute;

    private void Start()
    {
        lastMute = GameSettingsCache.Mute;
        source.volume = GameSettingsCache.Mute ? 0 : 1;
        source.Play();
    }

    private void Update()
    {
        if (lastMute != GameSettingsCache.Mute)
        {
            // StopAllCoroutines();
            // StartCoroutine(transformLoop(GameSettingsCache.Mute ? 0 : 1, Time.time, Time.time + 0.5f));
            source.volume = GameSettingsCache.Mute ? 0 : 1;
        }

        lastMute = GameSettingsCache.Mute;
    }

    public void SwitchBackgroundMusic(AudioClip clip)
    {
        StartCoroutine(transformLoop(0, Time.time, Time.time + 0.5f, t =>
        {
            source.Stop();
            source.clip = clip;
            source.Play();

            StartCoroutine(transformLoop(GameSettingsCache.Mute ? 0 : 1, t, t + 0.5f));
        }));
    }

    IEnumerator transformLoop(float to, double startTime, double endTime, Action<float> after = null)
    {
        var start = source.volume;

        while (Time.time < endTime)
        {
                source.volume = Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(Easing.OutPow10));

            yield return null;
        }
        
        after?.Invoke(Time.time);
    }
}