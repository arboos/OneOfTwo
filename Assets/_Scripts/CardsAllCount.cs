using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsAllCount : MonoBehaviour
{
    private void OnEnable()
    {
        int have = 0;
        int max = 0;

        have = GameManager.Instance.QuestionsHave.Count;

        max = GameManager.Instance.QuestionsAllList.Count;
        
        GetComponent<TextMeshProUGUI>().text = have + "/" + max;
    }
}
