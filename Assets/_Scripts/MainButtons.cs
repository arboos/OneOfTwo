using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Button = UnityEngine.UI.Button;

public class MainButtons : MonoBehaviour
{

    private Button _button;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        if (YandexGame.savesData.questionsHave.Count >= 5) _button.interactable = true;
        else _button.interactable = false;
    }

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        if (YandexGame.savesData.questionsHave.Count >= 5) _button.interactable = true;
        else _button.interactable = false;
    }
}
