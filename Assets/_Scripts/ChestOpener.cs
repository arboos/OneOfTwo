using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class ChestOpener : MonoBehaviour
{
    [SerializeField] private Transform lootLayout;
    [SerializeField] private GameObject CardPrefab;
    [SerializeField] private Button AcceptButton;
    [SerializeField] private GameObject AllCardsUnlocked_text;

    public List<QuestionScriptable> questionsToUnlock;

    
    public List<QuestionScriptable> unsortedList;


    private void Start()
    {
        AcceptButton.GetComponent<Button>().onClick.AddListener(delegate { Accept(); });
    }

    public void StartOpen(Chest.ChestType type)
    {
        questionsToUnlock = new List<QuestionScriptable>();
        int iMin = 0;
        int iMax = 0;
        switch (type)
        {
                
            case Chest.ChestType.Small:
                iMin = 3;
                iMax = 6;
                break;
            
            case Chest.ChestType.Big:
                iMin = 12;
                iMax = 16;
                break;
            
            case Chest.ChestType.Large:
                iMin = 22;
                iMax = 26;
                break;
        }
        
        for (int i = Random.Range(iMin, iMax); i > 0; i--)
        {
            print(1);
            if (GameManager.Instance.QuestionsNotHave.Count == 0) break;
            questionsToUnlock.Add(GetRandomQuestionToUnlock());
            GameManager.Instance.QuestionsNotHave.Remove(questionsToUnlock[questionsToUnlock.Count-1]);
        }
        
        //Sorting questionsToUnlock for count of similar type of cards 
        for (int i = 0; i < questionsToUnlock.Count; i++)
        {
            print("Sort" + i);
            for (int j = i+1; j < questionsToUnlock.Count; j++)
            {
                if (questionsToUnlock[i].Group == questionsToUnlock[j].Group)
                {
                    print("Sort_J" + i);
                    QuestionScriptable questionTemp = questionsToUnlock[i + 1];
                    questionsToUnlock[i + 1] = questionsToUnlock[j];
                    questionsToUnlock[j] = questionTemp;
                }
            }
        }
        
        StartCoroutine(ShowCards(questionsToUnlock));
    }

    
    public IEnumerator ShowCards(List<QuestionScriptable> cards)
    {
        int counter = 1;
        for(int i = 0; i < cards.Count; i++)
        {
            if (i < cards.Count - 1 && cards[i + 1].Group == cards[i].Group)
            {
                counter++;
                continue;
            }
            
            GameObject cardGO = Instantiate(CardPrefab);
            cardGO.transform.parent = lootLayout;
            cardGO.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("SO/Sprites/"+cards[i].Group.ToString());
            cardGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+"+counter;

            counter = 1;
            yield return new WaitForSeconds(0.5f);
        }

        if (cards.Count == 0) AllCardsUnlocked_text.SetActive(true);
        AcceptButton.gameObject.SetActive(true);
    }

    public QuestionScriptable GetRandomQuestionToUnlock()
    {
        int index = Random.Range(0, GameManager.Instance.QuestionsNotHave.Count);
        QuestionScriptable questionToReturn = GameManager.Instance.QuestionsNotHave[index];
        return questionToReturn;
    }

    public void Accept()
    {
        AllCardsUnlocked_text.SetActive(false);
        foreach (var question in questionsToUnlock)
        {
            GameManager.Instance.QuestionsHave.Add(question);
        }
        
        questionsToUnlock = new List<QuestionScriptable>();
        for (int i = 0; i < lootLayout.transform.childCount; i++)
        {
            Destroy(lootLayout.GetChild(i).gameObject);
        }

        YandexGame.savesData.questionsHave = GameManager.Instance.QuestionsHave;
        YandexGame.savesData.questionsNotHave = GameManager.Instance.QuestionsNotHave;
        YandexGame.SaveProgress();
    }
}
