using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Chest : MonoBehaviour
{
    public bool canOpen;

    public ChestType type;
    
    public float timeToOpen;
    public float currentTimeToOpen;

    private TextMeshProUGUI timerText;
    

    private Button _button;
    private Animator _animator;
    private void Start()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        timerText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _button.onClick.AddListener(delegate{OpenChest(); });
    }
    

    private void FixedUpdate()
    {
        if (currentTimeToOpen <= 0f && !canOpen)
        {
            _button.interactable = true;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
            _animator.SetBool("CanOpen", true);
            GetComponent<Image>().color = new Color(87f / 256f, 184f / 256f, 241f / 256f, 1f);
        }
        else if(currentTimeToOpen > 0f)
        {
            _button.interactable = false;
            currentTimeToOpen -= Time.deltaTime;
            int min;
            int sec;
            int.TryParse(((int)currentTimeToOpen / 60).ToString(), out min);
            int.TryParse(((int)currentTimeToOpen % 60).ToString(), out sec);
            timerText.text = min.ToString() + ":" + sec.ToString();
            if (sec < 10)
            {
                timerText.text = min.ToString() + ":0" + sec.ToString();
            }
        }
    }

    public void OpenChest()
    {
        currentTimeToOpen = timeToOpen;
        canOpen = false;
        _button.interactable = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        _animator.SetBool("CanOpen", false);
        GetComponent<Image>().color = new Color(87f / 256f, 184f / 256f, 241f / 256f, 0.3f);
        YandexGame.SaveProgress();
        GameManager.Instance.Chest.SetActive(true);
        GameManager.Instance.Menu.SetActive(false);
        GameManager.Instance.Chest.GetComponent<ChestOpener>().StartOpen(type);

        //GameManager.Instance.Menu.transform.GetChild(0).gameObject.SetActive(false);
        //GameManager.Instance.Chest.SetActive(true);
    }

    private void OnEnable()
    {
        if(YandexGame.savesData.exitTime == "") return;
        
        TimeSpan ts = new TimeSpan();
        ts = DateTime.Now - DateTime.Parse(YandexGame.savesData.exitTime);
        
        switch (type)
        {
            case ChestType.Small:
                currentTimeToOpen = YandexGame.savesData.timeSmallChest;
                break;
            
            case ChestType.Big:
                currentTimeToOpen = YandexGame.savesData.timeBigChest;
                break;
            
            case ChestType.Large:
                currentTimeToOpen = YandexGame.savesData.timeLargeChest;
                break;
        }
        int seconds = (ts.Days * 24 * 60 * 60) + (ts.Hours * 60 * 60) + (ts.Minutes * 60) + ts.Seconds;
        currentTimeToOpen -= seconds;
        
    }

    private void OnDisable()
    {
        switch (type)
        {
            case ChestType.Small:
                YandexGame.savesData.timeSmallChest = currentTimeToOpen;
                print(YandexGame.savesData.timeSmallChest);
                break;
            
            case ChestType.Big:
                YandexGame.savesData.timeBigChest = currentTimeToOpen;
                break;
            
            case ChestType.Large:
                YandexGame.savesData.timeLargeChest = currentTimeToOpen;
                break;
        }

        YandexGame.savesData.exitTime = DateTime.Now.ToString();
        YandexGame.SaveProgress();
    }

    public enum ChestType
    {
        Small,
        Big,
        Large
    }
    

}
