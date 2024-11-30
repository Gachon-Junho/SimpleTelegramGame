using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MonoBehaviorExtensions
{
    public static void LoadSceneAsync(this MonoBehaviour mono, string sceneName)
    {
        mono.StartCoroutine(loadScene());

        IEnumerator loadScene()
        {
            var loadingTask = SceneManager.LoadSceneAsync(sceneName);

            while (!loadingTask.isDone)
                yield return null;
        }
    }

    public static void ScaleTo(this MonoBehaviour mono, Vector3 scale, double duration = 0, Easing easing = Easing.None, Action then = null)
    {
        mono.StartCoroutine(transformLoop(scale, Time.time, Time.time + duration));

        IEnumerator transformLoop(Vector3 to, double startTime, double endTime)
        {
            var start = mono.transform.localScale;
                
            while (Time.time < endTime)
            {
                mono.transform.localScale = Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing));

                yield return null;
            }
            
            then?.Invoke();
        }
    }

    public static Coroutine StartDelayedCoroutine(this MonoBehaviour mono, IEnumerator routine, float timeUntilStart)
    {
        var cor = mono.StartCoroutine(startDelayed());
        
        IEnumerator startDelayed()
        {
            yield return new WaitForSeconds(timeUntilStart);

            cor = mono.StartCoroutine(routine);
        }

        return cor;
    }

    public static bool IsVisibleInCamera(this MonoBehaviour mono, Camera camera, float boundOffset = 0)
    {
        var cameraPos = camera.WorldToViewportPoint(mono.transform.position);

        if (cameraPos.y > 1 + boundOffset || cameraPos.y < 0 - boundOffset || cameraPos.x > 1 + boundOffset || cameraPos.x < 0 - boundOffset)
            return false;

        return true;
    }

    public static IEnumerator CheckVisibility(this MonoBehaviour mono, Camera camera, float boundOffset = 0, Action onBecameVisible = null, Action onBecameInvisible = null)
    {
        var isVisible = mono.IsVisibleInCamera(camera, boundOffset);
        var lastVisibility = isVisible;

        while (true)
        {
            isVisible = mono.IsVisibleInCamera(camera, boundOffset);

            if (lastVisibility != isVisible)
            {
                if (isVisible) onBecameVisible?.Invoke();
                else onBecameInvisible?.Invoke();
            }

            lastVisibility = isVisible;
            
            yield return null;
        }
    }
}