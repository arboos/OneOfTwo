using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameModeDefault : MonoBehaviour
{
    bool notAnswered = true;

    public Button cardA;
    public Button cardB;
    
    public Button OpenChest;

    public TextMeshProUGUI questionsCount;

    private Image spriteIcon;

    private void Start()
    {
        spriteIcon = GetComponent<Image>();

        cardA.onClick.AddListener(delegate { notAnswered = false;
            cardA.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);cardB.interactable = false;
        });
        cardB.onClick.AddListener(delegate { notAnswered = false; cardB.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            cardA.interactable = false;
        });
    }

    public void OpenWinChest(Chest.ChestType type)
    {
        GameManager.Instance.Chest.SetActive(true);
        GameManager.Instance.Menu.SetActive(false);
        GameManager.Instance.Chest.GetComponent<ChestOpener>().StartOpen(type);
    }

    public void StartGameVoid(List<QuestionScriptable> questions)
    {
        StartCoroutine(StartGame(questions));
    }
    
    public IEnumerator StartGame(List<QuestionScriptable> questions)
    {
        foreach (var question in questions)
        {
            

            cardA.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = question.p1.ToString();
            cardB.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = question.p2.ToString();
            
            cardA.transform.GetChild(1).gameObject.SetActive(false);
            cardB.transform.GetChild(1).gameObject.SetActive(false);

            cardA.transform.localScale = Vector3.one;
            cardB.transform.localScale = Vector3.one;

            cardA.interactable = true;
            cardB.interactable = true;
            
            switch (YandexGame.lang)
            {
                case "ru":
                    cardA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.ru_a;
                    cardB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.ru_b;
                    break;
                
                case "en":
                    cardA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.en_a;
                    cardB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.en_b;
                    break;
                
                case "tr":
                    cardA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.tr_a;
                    cardB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.tr_b;
                    break;
            }
            
            cardA.GetComponent<Animator>().SetTrigger("In");
            cardB.GetComponent<Animator>().SetTrigger("In");
            
            while (notAnswered)
            {
                yield return new WaitForEndOfFrame();
                print("Return");
            }
            
            
            cardA.transform.GetChild(1).gameObject.SetActive(true);
            cardB.transform.GetChild(1).gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);
        
            cardA.GetComponent<Animator>().SetTrigger("Out");
            cardB.GetComponent<Animator>().SetTrigger("Out");

            yield return new WaitForSeconds(1f);

            notAnswered = true;
        }
        
        GameManager.Instance.Menu.SetActive(true);
        OpenWinChest(Chest.ChestType.Small);

    }
    
}
