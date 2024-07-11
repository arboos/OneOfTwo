using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class ChestOpener : MonoBehaviour
{
    [SerializeField] private Transform lootLayout;
    [SerializeField] private GameObject CardPrefab;
    [SerializeField] private Button AcceptButton;

    public List<QuestionScriptable> questionsToUnlock;

    private void Start()
    {
        AcceptButton.GetComponent<Button>().onClick.AddListener(delegate { Accept(); });
    }

    public void StartOpen(Chest.ChestType type)
    {
        questionsToUnlock = new List<QuestionScriptable>();
        switch (type)
        {
            case Chest.ChestType.Small:
                //int i = Random.Range(3, 6)
                for (int i = Random.Range(3, 6); i > 0; i--)
                {
                    print(1);
                    if (GameManager.Instance.QuestionsNotHave.Count == 0) break;
                    questionsToUnlock.Add(GetRandomQuestionToUnlock());
                    GameManager.Instance.QuestionsNotHave.Remove(questionsToUnlock[questionsToUnlock.Count-1]);
                }
                break;
        }

        StartCoroutine(ShowCards(questionsToUnlock));
    }

    public IEnumerator ShowCards(List<QuestionScriptable> cards)
    {
        foreach (var card in cards)
        {
            GameObject cardGO = Instantiate(CardPrefab);
            cardGO.transform.parent = lootLayout;
            cardGO.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("SO/Sprites/"+card.Group.ToString());
            yield return new WaitForSeconds(0.5f);
        }
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
