using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    
    public void Show()
    {
        gameObject.SetActive(true);
        source.Play();
    }

    public void Hide()
    {
        source.Stop();
        gameObject.SetActive(false);
    }
}