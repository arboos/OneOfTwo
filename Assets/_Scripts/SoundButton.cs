using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite SoundOnSprite;
    [SerializeField] private Sprite SoundOffSprite;
    [SerializeField] private AudioMixer mixer;

    public bool enabled;
    
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeSoundState()
    {
        if (enabled)
        {
            mixer.SetFloat("Volume", -80f);
            _image.sprite = SoundOffSprite;
            enabled = false;
        }
        else
        {
            mixer.SetFloat("Volume", 0f);
            _image.sprite = SoundOnSprite;
            enabled = true;
        }
    }
}
