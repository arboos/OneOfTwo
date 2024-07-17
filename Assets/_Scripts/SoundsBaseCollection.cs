using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsBaseCollection : MonoBehaviour
{
    public static SoundsBaseCollection Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource menuSound;
    public AudioSource gameplaySound;
    public AudioSource buttonClickSound;
    public AudioSource cardClickSound;
    public AudioSource cardOpenSound;
    public AudioSource cardOpenEndSound;
    
}
