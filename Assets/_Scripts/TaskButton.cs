using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton : MonoBehaviour
{
    public Chest.ChestType rewardType;

    public string taskType;

    public bool collected;
    
    private Button _button;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        switch (rewardType)
        {
            case Chest.ChestType.Small:
                transform.GetChild(0).GetComponent<Image>().sprite = Tasks.Instance.smallChest;
                break;
            
            case Chest.ChestType.Big:
                transform.GetChild(0).GetComponent<Image>().sprite = Tasks.Instance.bigChest;
                break;
            
            case Chest.ChestType.Large:
                transform.GetChild(0).GetComponent<Image>().sprite = Tasks.Instance.largeChest;
                break;
        }

        
        _button.onClick.AddListener(delegate
        {
            GameManager.Instance.Chest.SetActive(true);
            GameManager.Instance.Chest.GetComponent<ChestOpener>().StartOpen(rewardType);
            collected = true;
            _button.interactable = false;
            _button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"<s>{_button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text}</s>";
            GetComponent<Animator>().SetBool("earned", true);
            PlayerPrefs.SetInt("E_"+gameObject.name, 1);
            Tasks.Instance.tasksCompleted++;
            Tasks.Instance.tasksText.text = Tasks.Instance.tasksCompleted + "/10";
            _button.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.3f);
        });

        if ((PlayerPrefs.HasKey("C_" + gameObject.name)) || (PlayerPrefs.HasKey("E_" + gameObject.name)))
        {
            Tasks.Instance.tasksCompleted++;
            Tasks.Instance.tasksText.text = Tasks.Instance.tasksCompleted + "/10";
            print("ASASFASFSAFSAFSAFSAF");
        }
        
        if (PlayerPrefs.HasKey("E_" + gameObject.name))
        {
            if (PlayerPrefs.GetInt("E_" + gameObject.name) != 0)
            {
                collected = true;
                _button.interactable = false;
                _button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    $"<s>{_button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text}</s>";
                GetComponent<Animator>().SetBool("earned", true);
                _button.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
                GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
                transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.3f);
            }
        }
        else if (PlayerPrefs.HasKey("C_"+gameObject.name))
        {
            if (PlayerPrefs.GetInt("C_" + gameObject.name) != 0)
            {
                GetComponent<Animator>().SetBool("completed", true);
                PlayerPrefs.SetInt("C_" + gameObject.name, 1);
                _button.interactable = true;
            }
        }
    }

    private void OnEnable()
    {
        CheckCompletion();
    }

    public void UpdateButtons()
    {
        TaskButton[] tasks = GameObject.FindObjectsByType<TaskButton>(FindObjectsSortMode.None);
        foreach (var vTask in tasks)
        {
            vTask.CheckCompletion();
        }
    }
    
    public void CheckCompletion()
    {
        print("CheckCompletion()");
        _button = GetComponent<Button>();
        if (collected)
        {
            print("CheckCompletion().collected");
            Tasks.Instance.tasksCompleted++;
            _button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"<s>{_button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text}</s>";
            Tasks.Instance.tasksText.text = Tasks.Instance.tasksCompleted + "/10";
            return;
        }
        switch (taskType)
        {
            case "q25":
                if (Tasks.Instance.questionsAnswered >= 25)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "q50":
                if (Tasks.Instance.questionsAnswered >= 50)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "q100":
                if (Tasks.Instance.questionsAnswered >= 50)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "c10":
                if (GameManager.Instance.QuestionsHave.Count >= 10)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "c30":
                if (GameManager.Instance.QuestionsHave.Count >= 30)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "c90":
                if (GameManager.Instance.QuestionsHave.Count >= 90)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "r25":
                if (Tasks.Instance.redAnswered >= 25)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "b25":
                if (Tasks.Instance.blueAnswered >= 25)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "correct40":
                if (Tasks.Instance.questionsAnsweredCorrect_WichMore >= 40)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
            
            case "correct100":
                if (Tasks.Instance.questionsAnsweredCorrect_WichMore >= 100)
                {
                    GetComponent<Animator>().SetBool("completed", true);
                    PlayerPrefs.SetInt("C_"+gameObject.name, 1);
                    _button.interactable = true;
                }
                break;
        }
    }
    
    private void OnDisable()
    {
        Tasks.Instance.tasksCompleted = 0;
        print("Tasks back");
    }
}
