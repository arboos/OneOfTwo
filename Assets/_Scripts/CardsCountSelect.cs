using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardsCountSelect : MonoBehaviour
{
    public int maxValue;

    [SerializeField] private Button startGameButton;
    
    public int valueCurrent;

    [SerializeField] private Slider slider;

    private TextMeshProUGUI textCount;
    private int selfMode;
    public float valueSlider;
    public void SetCurrentMode(int mode)
    {
        selfMode = mode; // 0 - default; 1 - wich more
    }

    private void Start()
    {
        startGameButton.onClick.AddListener(delegate{StartGame();});
    }

    private void OnEnable()
    {
        textCount = startGameButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SliderValueChanged()
    {
        maxValue = GameManager.Instance.QuestionsHave.Count;
        valueSlider = slider.value;
        valueCurrent = 5 + (int)((maxValue-5) * valueSlider);
        textCount.text = valueCurrent.ToString();
        

    }

    public void StartGame()
    {
        if(selfMode == 0)
        {
            GameManager.Instance.StartDefaultMode(valueCurrent);
        }
    }
}
