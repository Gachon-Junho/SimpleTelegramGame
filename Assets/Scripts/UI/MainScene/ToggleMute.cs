using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMute : MonoBehaviour
{
    [SerializeField] 
    private Sprite speaker;
    
    [SerializeField] 
    private Sprite mute;

    [SerializeField] 
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        updateSprite();
    }

    public void Toggle()
    {
        GameSettingsCache.Mute = !GameSettingsCache.Mute;
        updateSprite();
    }

    private void updateSprite()
    {
        if (GameSettingsCache.Mute)
            image.sprite = speaker;
        else
            image.sprite = mute;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
