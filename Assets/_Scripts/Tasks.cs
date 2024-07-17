using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Tasks : MonoBehaviour
{
    public static Tasks Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        questionsAnswered = YandexGame.savesData.qAnswered;
        redAnswered = YandexGame.savesData.rAnswered;
        blueAnswered = YandexGame.savesData.bAnswered;
        questionsAnsweredCorrect_WichMore = YandexGame.savesData.qAnsweredCorrect_WichMore;
    }

    public int questionsAnswered;
    public int questionsAnsweredCorrect_WichMore;
    public int redAnswered;
    public int blueAnswered;
    public int tasksCompleted;

    public Sprite smallChest;
    public Sprite bigChest;
    public Sprite largeChest;
    
    public TextMeshProUGUI tasksText;
    
}
