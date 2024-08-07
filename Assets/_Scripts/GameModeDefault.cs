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
    bool loaded = false;

    public Button cardA;
    public Button cardB;

    public Sprite chestSmall;
    public Sprite chestBig;
    public Sprite chestLarge;

    public TextMeshProUGUI questionsCount;
    public int questionsCountInt;

    public GameObject awardWindow;
    public GameObject gameplayWindow;
    public Image chestToGiveImage;
    public Button openReward;

    public Chest.ChestType chestType;

    private void Start()
    {
        cardA.onClick.AddListener(delegate
        {
            notAnswered = false;
            cardA.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            cardA.interactable = false;
            cardB.interactable = false;
            Tasks.Instance.blueAnswered++;
            YandexGame.savesData.bAnswered = Tasks.Instance.blueAnswered;
            YandexGame.SaveProgress();
            SoundsBaseCollection.Instance.cardClickSound.Play();

        });
        cardB.onClick.AddListener(delegate
        {
            notAnswered = false;
            cardB.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            cardA.interactable = false;
            cardB.interactable = false;
            Tasks.Instance.redAnswered++;
            YandexGame.savesData.rAnswered = Tasks.Instance.redAnswered;
            YandexGame.SaveProgress();
            SoundsBaseCollection.Instance.cardClickSound.Play();
            
        });
        openReward.onClick.AddListener(delegate
        {
            OpenWinChest(chestType);
            GameManager.Instance.Menu.SetActive(true);
            awardWindow.SetActive(false);
            gameplayWindow.SetActive(true);
        });
    }

    public void OpenWinChest(Chest.ChestType type)
    {
        GameManager.Instance.Chest.SetActive(true);
        GameManager.Instance.Menu.SetActive(false);
        GameManager.Instance.Chest.GetComponent<ChestOpener>().StartOpen(type);
        GameManager.Instance.GameMode_Default.SetActive(false);
    }

    public void StartGameVoid(List<QuestionScriptable> questions)
    {
        print("STarted game");
        StartCoroutine(StartGame(questions));
    }

    public IEnumerator StartGame(List<QuestionScriptable> questions)
    {
        gameplayWindow.SetActive(true);
        awardWindow.SetActive(false);
        cardA.animator.SetTrigger("In");
        cardB.animator.SetTrigger("In");
        questionsCountInt = 0;
        foreach (var question in questions)
        {
            SoundsBaseCollection.Instance.cardOn.Play();

            questionsCountInt++;
            cardA.interactable = true;
            cardB.interactable = true;
            questionsCount.text = questionsCountInt.ToString() + "/" + questions.Count;
            cardA.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = question.p1.ToString() + "%";
            cardB.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = question.p2.ToString() + "%";

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

            GetComponent<Image>().sprite = Resources.Load<Sprite>("SO/Sprites/" + question.Group.ToString());

            while (notAnswered)
            {
                yield return new WaitForFixedUpdate();
                print("Return");
            }

            Tasks.Instance.questionsAnswered++;
            YandexGame.savesData.qAnswered++;
            YandexGame.SaveProgress();
            
            YandexGame.NewLeaderboardScores("Questions", Tasks.Instance.questionsAnswered);

            cardA.transform.GetChild(1).gameObject.SetActive(true);
            cardB.transform.GetChild(1).gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            cardA.GetComponent<Animator>().SetTrigger("Out");
            cardB.GetComponent<Animator>().SetTrigger("Out");
            SoundsBaseCollection.Instance.cardAway.Play();

            yield return new WaitForSeconds(1f);

            notAnswered = true;
        }

        gameplayWindow.SetActive(false);
        awardWindow.SetActive(true);
        SoundsBaseCollection.Instance.cardOpenEndSound.Play();
        
        if (questions.Count <= 10)
        {
            chestToGiveImage.sprite = chestSmall;
            chestType = Chest.ChestType.Small;
        }
        else if (questions.Count > 10 && questions.Count <= 25)
        {
            chestToGiveImage.sprite = chestBig;
            chestType = Chest.ChestType.Big;
        }
        else
        {
            chestToGiveImage.sprite = chestLarge;
            chestType = Chest.ChestType.Large;
        }
    }
}