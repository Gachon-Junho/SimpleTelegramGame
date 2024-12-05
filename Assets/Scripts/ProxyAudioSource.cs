using System.Collections;
using UnityEngine;

public class ProxyAudioSource : MonoBehaviour
{
    [SerializeField] 
    private static GameObject Prefab;
    
    [SerializeField] 
    private AudioSource audio;

    public void Play(AudioClip clip = null)
    {
        if (audio == null)
            audio = GetComponent<AudioSource>();
        
        if (clip != null)
            audio.clip = clip;
        else
            return;

        audio.volume = GameSettingsCache.EffectVolume;
        audio.Play();
        
        StartCoroutine(scheduleDestroy(clip.length));
    }

    public static void CreateProxyAndPlay(AudioClip clip)
    {
        var obj = Instantiate(Prefab).GetComponent<ProxyAudioSource>();
        obj.Play(clip);
    }

    private IEnumerator scheduleDestroy(float timeUntilPerform)
    {
        yield return new WaitForSeconds(timeUntilPerform);
        
        Destroy(gameObject);
    }
}