using System;
using System.Collections;
using UnityEngine;

public class DangerLine : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer line;
    
    [SerializeField]
    private BackgroundMusic bgm;

    [SerializeField] 
    private AudioClip gameoverClip;

    private bool finishRequested;
    private bool canceled;

    private IEnumerator OnTriggerStay2D(Collider2D other)
    {
        var doughnut = other.GetComponent<Doughnut>();

        if (doughnut == null)
            yield break;

        if (doughnut.State == DoughnutState.Completed && !finishRequested)
        {
            finishRequested = true;
            
            StartCoroutine(flashColor());
            this.StartDelayedCoroutine(finish(), 1.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canceled = true;
        finishRequested = false;
    }

    private IEnumerator flashColor()
    {
        if (canceled)
        {
            canceled = false;
            yield break;
        }
        
        var origin = line.color;
        
        line.color = new Color(1, 1, 1, line.color.a);
        
        StartCoroutine(transformLoop(origin, Time.time, Time.time + 0.5f, (_) => StartCoroutine(flashColor())));

        yield return null;
    }
    
    private IEnumerator transformLoop(Color to, double startTime, double endTime, Action<float> after = null)
    {
        if (canceled)
        {
            canceled = false;

            line.color = to;
            
            yield break;
        }
        
        var origin = line.color;

        while (Time.time < endTime)
        {
            Color newCol = new Color(
                Interpolation.ValueAt(Time.time, origin.r, to.r, startTime, endTime, new EasingFunction(Easing.OutQuint)),
                Interpolation.ValueAt(Time.time, origin.g, to.g, startTime, endTime, new EasingFunction(Easing.OutQuint)),
                Interpolation.ValueAt(Time.time, origin.b, to.b, startTime, endTime, new EasingFunction(Easing.OutQuint)),
                  origin.a);

            line.color = newCol;

            yield return null;
        }
        
        after?.Invoke(Time.time);
    }

    private IEnumerator finish()
    {
        if (canceled)
        {
            canceled = false;
            yield break;
        }

        StartCoroutine(flashColor());
        
        GameplayManager.Current.FinishGame();
        bgm.SwitchBackgroundMusic(gameoverClip);

        yield return null;
    }
}