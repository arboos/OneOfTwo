using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsCount : MonoBehaviour
{
    [SerializeField] private GameManager.QuestionGroup Group;
    private void OnEnable()
    {
        int have = 0;
        int max = 0;

        foreach (var question in GameManager.Instance.QuestionsHave)
        {
            if (question.Group == Group) have++;
        }
        
        foreach (var question in GameManager.Instance.QuestionsAllList)
        {
            if (question.Group == Group) max++;
        }

        GetComponent<TextMeshProUGUI>().text = have + "/" + max;
    }
}
