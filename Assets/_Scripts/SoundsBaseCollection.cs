using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioSource cardUnlockSound;
    public AudioSource cardOpenEndSound;
    public AudioSource cardAway;
    public AudioSource cardOn;

    private void Start()
    {
        Button[] buttons = GameObject.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var button in buttons)
        {
            if (button.CompareTag("Card"))
            {
                button.onClick.AddListener(delegate { cardClickSound.Play(); });
            }
            else
            {
                button.onClick.AddListener(delegate { buttonClickSound.Play(); });
            }
           
        }
    }
}
